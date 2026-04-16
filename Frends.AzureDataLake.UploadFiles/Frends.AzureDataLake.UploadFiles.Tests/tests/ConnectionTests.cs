using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Frends.AzureDataLake.UploadFiles.Definitions;
using Frends.AzureDataLake.UploadFiles.Exceptions;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class ConnectionTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(ContainerNotFoundException))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.UploadFiles(
            new Input { ContainerName = containerName },
            new Connection { ConnectionString = connectionString },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(RequestFailedException))]
    public async Task ThrowIfWrongConnectionStringCredentials()
    {
        var wrongConnString =
            "DefaultEndpointsProtocol=https;AccountName=frendstemplates;AccountKey=000000000wrongKey00000000000000000000000000000000000000000000000000000000000000000000000;EndpointSuffix=core.windows.net";
        await AzureDataLake.UploadFiles(
            new Input { ContainerName = containerName },
            new Connection { ConnectionString = wrongConnString },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationFailedException))]
    public async Task ThrowIfWrongOauthCredentials()
    {
        await AzureDataLake.UploadFiles(
            new Input { ContainerName = containerName },
            new Connection
            {
                AuthenticationMethod = AuthenticationMethod.OAuth2,
                StorageAccountName = storageAccount,
                ApplicationId = appID,
                TenantId = tenantID,
                ClientSecret = "wrongSecret"
            },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public async Task ThrowIfWrongConnectionString()
    {
        var wrongConnectionString = $"xxx{connectionString}";
        await AzureDataLake.UploadFiles(
            new Input { ContainerName = containerName },
            new Connection { ConnectionString = wrongConnectionString },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task ThrowIfParametersNotValid()
    {
        await AzureDataLake.UploadFiles(
            new Input { ContainerName = "" },
            new Connection { ConnectionString = connectionString },
            new Options(),
            new CancellationToken()
        );
    }
}
