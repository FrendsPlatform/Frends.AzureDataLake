using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Frends.AzureDataLake.DeleteFiles.Definitions;
using Frends.AzureDataLake.DeleteFiles.Exceptions;
using static Frends.AzureDataLake.DeleteFiles.Definitions.Constants;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Tests;

[TestClass]
public class ConnectionTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public async Task ThrowIfConnectionStringIsInvalid()
    {
        var wrongConnStr = $"xxx{connectionString}";
        await AzureDataLake.DeleteFiles(
            new Input { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(RequestFailedException))]
    public async Task ThrowIfConnectionStringKeyIsInvalid()
    {
        var wrongConnStr =
            "DefaultEndpointsProtocol=https;AccountName=frendstemplates;AccountKey=000000000wrongKey00000000000000000000000000000000000000000000000000000000000000000000000;EndpointSuffix=core.windows.net";

        await AzureDataLake.DeleteFiles(
            new Input { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationFailedException))]
    public async Task ThrowIfOauthParametersAreInvalid()
    {
        await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = containerName,
                StorageAccountName = storageAccount,
                ApplicationID = appID,
                TenantID = tenantID,
                ClientSecret = "wrongSecret"
            },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(ContainerNotFoundException))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = "not-existing-container"
            },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task ThrowIfInvalidSourceParameters()
    {
        await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = "InvalidContainerName"
            },
            new Options(),
            CancellationToken.None
        );
    }
}
