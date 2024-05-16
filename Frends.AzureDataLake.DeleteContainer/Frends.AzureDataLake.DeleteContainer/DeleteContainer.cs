﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Frends.AzureDataLake.DeleteContainer.Definitions;
using Frends.AzureDataLake.DeleteContainer.Exceptions;

namespace Frends.AzureDataLake.DeleteContainer;

/// <summary>
/// Azure Data Lake Task.
/// </summary>
public static class AzureDataLake
{
    /// <summary>
    /// Deletes a container from Azure Data Lake.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.AzureDataLake.DeleteContainer)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options regarding the error handling.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this task.</param>
    /// <returns>Object { string ContainerWasDeleted, string Message }</returns>
    public static async Task<Result> DeleteContainer(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken
    )
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

        var container = GetDataLakeContainer(input);

        var result = await container.DeleteIfExistsAsync(null, cancellationToken);

        if (!result)
        {
            if (options.ThrowErrorIfContainerDoesNotExist)
                throw new ContainerNotFoundException();
            else
                return new Result(false, "Container not found.");
        }
        return new Result(result, "Container deleted successfully.");
    }

    private static DataLakeFileSystemClient GetDataLakeContainer(Input input)
    {
        DataLakeServiceClient client;

        if (input.ConnectionMethod is ConnectionMethod.ConnectionString)
            client = new DataLakeServiceClient(input.ConnectionString);
        else
        {
            var credentials = new ClientSecretCredential(
                input.TenantID,
                input.ApplicationID,
                input.ClientSecret,
                new ClientSecretCredentialOptions()
            );
            client = new DataLakeServiceClient(
                new Uri($"https://{input.StorageAccountName}.dfs.core.windows.net"),
                credentials
            );
        }

        return client.GetFileSystemClient(input.ContainerName);
    }
}
