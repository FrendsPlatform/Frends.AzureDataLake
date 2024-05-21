﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Frends.AzureDataLake.DeleteDirectory.Definitions;

namespace Frends.AzureDataLake.DeleteDirectory;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class AzureDataLake
{
    /// <summary>
    /// Frends Task for deleteing a directory in Azure Data Lake container.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.AzureDataLake.DeleteDirectory)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this task.</param>
    /// <returns>Object { string DirectoryWasDeleted, string Message }</returns>
    public static async Task<Result> DeleteDirectory([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        if (input.ConnectionMethod is ConnectionMethod.OAuth2 && (input.ApplicationID is null || input.ClientSecret is null || input.TenantID is null || input.StorageAccountName is null))
            throw new ArgumentNullException("Input.StorageAccountName, Input.ClientSecret, Input.ApplicationID and Input.TenantID parameters can't be empty when Input.ConnectionMethod = OAuth.");
        if (string.IsNullOrWhiteSpace(input.ConnectionString) && input.ConnectionMethod is ConnectionMethod.ConnectionString)
            throw new ArgumentNullException("ConnectionString parameter can't be empty when Input.ConnectionMethod = ConnectionString.");
        if (string.IsNullOrWhiteSpace(input.ContainerName))
            throw new ArgumentNullException("ContainerName parameter can't be empty.");
        if (string.IsNullOrWhiteSpace(input.DirectoryName))
            throw new ArgumentNullException("DirectoryName parameter can't be empty.");

        try
        {
            var directory = GetDataLakeDirectory(input);

            var directoryExists = await directory.ExistsAsync(cancellationToken);

            if (!directoryExists)
            {
                if (options.ThrowErrorIfDirectoryDoesNotExist)
                {
                    throw new Exception("DeleteDirectory error: Directory not found.");
                }
                else
                {
                    return new Result(false, "Directory not found.");
                }
            }

            var result = await directory.DeleteIfExistsAsync(null, cancellationToken);
            return new Result(result, "Directory deleted successfully.");
        }
        catch (Exception ex)
        {
            throw new Exception("DeleteDirectory: Error occured while trying to delete directory.", ex);
        }
    }

    internal static DataLakeDirectoryClient GetDataLakeDirectory(Input input)
    {
        try
        {
            DataLakeServiceClient dataLakeServiceClient;
            DataLakeFileSystemClient fileSystemClient;

            switch (input.ConnectionMethod)
            {
                case ConnectionMethod.ConnectionString:
                    dataLakeServiceClient = new DataLakeServiceClient(input.ConnectionString);
                    fileSystemClient = dataLakeServiceClient.GetFileSystemClient(input.ContainerName);
                    return fileSystemClient.GetDirectoryClient(input.DirectoryName);
                case ConnectionMethod.OAuth2:
                    var credentials = new ClientSecretCredential(input.TenantID, input.ApplicationID, input.ClientSecret, new ClientSecretCredentialOptions());
                    dataLakeServiceClient = new DataLakeServiceClient(new Uri($"https://{input.StorageAccountName}.dfs.core.windows.net"), credentials);
                    fileSystemClient = dataLakeServiceClient.GetFileSystemClient(input.ContainerName);
                    return fileSystemClient.GetDirectoryClient(input.DirectoryName);
                default: throw new NotSupportedException();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"GetDataLakeDirectory error: {ex}");
        }
    }
}
