using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Frends.AzureDataLake.DownloadFiles.Definitions;
using Frends.AzureDataLake.UploadFiles.Exceptions;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class ConnectionTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public async Task ThrowIfConnectionStringIsInvalid()
    {
        var wrongConnStr = $"xxx{connectionString}";
        await AzureDataLake.DownloadFiles(
            new Source { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Destination { Directory = testDirectory },
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

        await AzureDataLake.DownloadFiles(
            new Source { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationFailedException))]
    public async Task ThrowIfOauthParametersAreInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = containerName,
                StorageAccountName = storageAccount,
                ApplicationID = appID,
                TenantID = tenantID,
                ClientSecret = "wrongSecret"
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(ContainerNotFoundException))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = "not-existing-container"
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task ThrowIfInvalidSourceParameters()
    {
        await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = "InvalidContainerName"
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task ThrowIfInvalidDestinationParameters()
    {
        await AzureDataLake.DownloadFiles(
            new Source { ConnectionString = connectionString, ContainerName = containerName },
            new Destination { Directory = "C:/NonExistingDir/ForSure/test" },
            new Options(),
            CancellationToken.None
        );
    }
}
