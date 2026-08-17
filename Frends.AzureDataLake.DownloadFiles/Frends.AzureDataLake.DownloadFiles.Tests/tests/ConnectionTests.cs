using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;
using Frends.AzureDataLake.DownloadFiles.Exceptions;
using static Frends.AzureDataLake.DownloadFiles.Definitions.Constants;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class ConnectionTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfConnectionStringIsInvalid()
    {
        var wrongConnStr = $"xxx{connectionString}";
        await AzureDataLake.DownloadFiles(
            new Input { ContainerName = containerName, Directory = testDirectory },
            new Connection { ConnectionString = wrongConnStr },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfConnectionStringKeyIsInvalid()
    {
        var wrongConnStr =
            "DefaultEndpointsProtocol=https;AccountName=frendstemplates;AccountKey=000000000wrongKey00000000000000000000000000000000000000000000000000000000000000000000000;EndpointSuffix=core.windows.net";

        await AzureDataLake.DownloadFiles(
            new Input { ContainerName = containerName, Directory = testDirectory },
            new Connection { ConnectionString = wrongConnStr },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfOauthParametersAreInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                Directory = testDirectory
            },
            new Connection
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
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
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = "not-existing-container",
                Directory = testDirectory
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfInvalidSourceParameters()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = "InvalidContainerName",
                Directory = testDirectory
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
    }
}
