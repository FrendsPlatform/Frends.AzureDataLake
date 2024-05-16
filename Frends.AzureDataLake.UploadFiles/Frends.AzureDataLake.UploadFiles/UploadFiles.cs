﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Frends.AzureDataLake.UploadFiles.Definitions;
using Frends.AzureDataLake.UploadFiles.Exceptions;

namespace Frends.AzureDataLake.UploadFiles;

/// <summary>
/// Azure Data Lake Task.
/// </summary>
public static class AzureDataLake
{
    /// <summary>
    /// Upload files to Azure Data Lake.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.AzureDataLake.UploadFiles)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this Task.</param>
    /// <returns>Object { bool Success, Dictionary&lt;string, string&gt; Data }</returns>
    public static async Task<Result> UploadFiles(
        [PropertyTab] Input input,
        CancellationToken cancellationToken
    )
    {
        var resultData = new Dictionary<string, string>();
        try
        {
            ValidateDestinationParameters(input.Destination);

            var container = await GetDataLakeContainer(input.Destination, cancellationToken);
            var paralelResults = new ConcurrentDictionary<string, string>();
            var paths = Directory.GetFiles(
                input.Source.SourceDirectory,
                input.Source.SourceFilePattern,
                input.Options.UploadFilesRecursively
                    ? SearchOption.AllDirectories
                    : SearchOption.TopDirectoryOnly
            );

            await Parallel.ForEachAsync(
                paths,
                async (sourcePath, cancellationToken) =>
                    await UploadFile(
                        sourcePath,
                        paralelResults,
                        input,
                        container,
                        cancellationToken
                    )
            );

            return new Result(true, new Dictionary<string, string>(paralelResults));
        }
        catch (Exception ex)
        {
            if (input.Options.ThrowErrorOnFailure)
                throw;
            else
                return new Result(false, new Dictionary<string, string>(), ex.Message);
        }
    }

    private static async Task<DataLakeFileSystemClient> GetDataLakeContainer(
        Destination dst,
        CancellationToken token
    )
    {
        DataLakeServiceClient client = dst.ConnectionMethod switch
        {
            ConnectionMethod.ConnectionString => new DataLakeServiceClient(dst.ConnectionString),
            ConnectionMethod.OAuth2
                => new DataLakeServiceClient(
                    new Uri($"https://{dst.StorageAccountName}.dfs.core.windows.net"),
                    new ClientSecretCredential(
                        dst.TenantID,
                        dst.ApplicationID,
                        dst.ClientSecret,
                        new ClientSecretCredentialOptions()
                    )
                ),
            _ => throw new InvalidEnumArgumentException(),
        };

        var container = client.GetFileSystemClient(dst.ContainerName);

        if (!await container.ExistsAsync(token))
            throw new ContainerNotFoundException(dst.ContainerName);
        return container;
    }

    private static void ValidateDestinationParameters(Destination dst)
    {
        if (
            dst.ConnectionMethod is ConnectionMethod.OAuth2
            && (
                dst.ApplicationID is null
                || dst.ClientSecret is null
                || dst.TenantID is null
                || dst.StorageAccountName is null
            )
        )
            throw new InvalidInputException(
                "Input.StorageAccountName, Input.ClientSecret, Input.ApplicationID and Input.TenantID parameters can't be empty when Input.ConnectionMethod = OAuth."
            );
        if (
            dst.ConnectionMethod is ConnectionMethod.ConnectionString
            && string.IsNullOrWhiteSpace(dst.ConnectionString)
        )
            throw new InvalidInputException(
                "ConnectionString parameter can't be empty when Input.ConnectionMethod = ConnectionString."
            );
        if (string.IsNullOrWhiteSpace(dst.ContainerName))
            throw new InvalidInputException("ContainerName parameter can't be empty.");
    }

    private static async Task UploadFile(
        string srcPath,
        ConcurrentDictionary<string, string> paralelResults,
        Input input,
        DataLakeFileSystemClient container,
        CancellationToken token
    )
    {
        var relativePath = srcPath[input.Source.SourceDirectory.Length..]
            .TrimStart(Path.DirectorySeparatorChar);

        var destinationPath = input.Destination.DestinationFolderName is null
            ? relativePath
            : Path.Combine(input.Destination.DestinationFolderName, relativePath);

        if (container.GetFileClient(destinationPath).Exists(token) && !input.Options.Overwrite)
        {
            paralelResults.TryAdd(srcPath, "File already exists");
            if (input.Options.ThrowErrorOnFailure)
                throw new FileAlreadyExistsException(destinationPath);
        }
        else
        {
            DataLakeFileClient fileClient = await container.CreateFileAsync(
                destinationPath,
                null,
                token
            );

            if (new FileInfo(srcPath).Length > 0)
                await fileClient.UploadAsync(srcPath, true, token);

            paralelResults.TryAdd(srcPath, destinationPath);
        }
    }
}
