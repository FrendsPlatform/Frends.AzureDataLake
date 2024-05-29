﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Frends.AzureDataLake.DeleteFiles.Definitions;
using Frends.AzureDataLake.DeleteFiles.Exceptions;
using Microsoft.Extensions.FileSystemGlobbing;
using static Frends.AzureDataLake.DeleteFiles.Definitions.Constants;

namespace Frends.AzureDataLake.DeleteFiles;

/// <summary>
/// Azure Data Lake Task.
/// </summary>
public static class AzureDataLake
{
    /// <summary>
    /// Delete files from Azure Data Lake.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.AzureDataLake.DeleteFiles)
    /// </summary>
    /// <param name="input">Input parameters</param>
    /// <param name="options">Options parameters</param>
    /// <param name="token">Token generated by Frends to stop this task.</param>
    /// <returns>Object { bool IsSuccess, List&lt;string&gt; DeletedFiles, string ErrorMessage }</returns>
    public static async Task<Result> DeleteFiles(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken token
    )
    {
        try
        {
            ValidateInputParameters(input);

            var container = await GetDataLakeContainer(input, token);
            var parallelResults = new ConcurrentBag<string>();
            var allContainerFiles = container
                .GetPaths(recursive: true, cancellationToken: token)
                .Where(x => (bool)!x.IsDirectory)
                .Select(x => x.Name);
            var matches = new Matcher()
                .AddInclude($"**/{input.DeleteFilePattern}")
                .Match(allContainerFiles);

            await Parallel.ForEachAsync(
                matches.Files,
                async (match, token) =>
                {
                    var deletedFilePath = await DeleteFile(match.Path, container, token);
                    parallelResults.Add(deletedFilePath);
                }
            );

            return new Result { IsSuccess = true, DeletedFiles = parallelResults.ToList() };
        }
        catch (Exception ex)
        {
            if (options.ThrowErrorOnFailure)
                throw;
            else
                return new Result { ErrorMessage = ex.Message };
        }
    }

    private static void ValidateInputParameters(Input input)
    {
        if (
            input.ConnectionMethod is ConnectionMethod.OAuth2
            && (
                input.ApplicationID is null
                || input.ClientSecret is null
                || input.TenantID is null
                || input.StorageAccountName is null
            )
        )
            throw new InvalidInputException(
                "Input.StorageAccountName, Input.ClientSecret, Input.ApplicationID and Input.TenantID parameters can't be empty when Input.ConnectionMethod = OAuth."
            );
        if (
            input.ConnectionMethod is ConnectionMethod.ConnectionString
            && string.IsNullOrWhiteSpace(input.ConnectionString)
        )
            throw new InvalidInputException(
                "ConnectionString parameter can't be empty when Input.ConnectionMethod = ConnectionString."
            );
        if (string.IsNullOrWhiteSpace(input.ContainerName))
            throw new InvalidInputException("ContainerName parameter can't be empty.");
        if (input.ContainerName.Any(char.IsUpper))
            throw new InvalidInputException("ContainerName can't contain upper letters.");
    }

    private static async Task<DataLakeFileSystemClient> GetDataLakeContainer(
        Input input,
        CancellationToken token
    )
    {
        DataLakeServiceClient client = input.ConnectionMethod switch
        {
            ConnectionMethod.ConnectionString => new DataLakeServiceClient(input.ConnectionString),
            ConnectionMethod.OAuth2
                => new DataLakeServiceClient(
                    new Uri($"https://{input.StorageAccountName}.blob.core.windows.net"),
                    new ClientSecretCredential(
                        input.TenantID,
                        input.ApplicationID,
                        input.ClientSecret,
                        new ClientSecretCredentialOptions()
                    )
                ),
            _ => throw new InvalidEnumArgumentException(),
        };

        var container = client.GetFileSystemClient(input.ContainerName);

        if (!await container.ExistsAsync(token))
            throw new ContainerNotFoundException(input.ContainerName);
        return container;
    }

    private static async Task<string> DeleteFile(
        string path,
        DataLakeFileSystemClient container,
        CancellationToken token
    )
    {
        var fileClient = container.GetFileClient(path);
        await fileClient.DeleteAsync(cancellationToken: token);
        return fileClient.Uri.AbsoluteUri;
    }
}