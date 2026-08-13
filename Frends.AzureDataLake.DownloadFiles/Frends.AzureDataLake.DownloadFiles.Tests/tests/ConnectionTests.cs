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
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfConnectionStringIsInvalid()
    {
        var wrongConnStr = $"xxx{connectionString}";
        await AzureDataLake.DownloadFiles(
            new Input { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Connection { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfConnectionStringKeyIsInvalid()
    {
        var wrongConnStr =
            "DefaultEndpointsProtocol=https;AccountName=frendstemplates;AccountKey=000000000wrongKey00000000000000000000000000000000000000000000000000000000000000000000000;EndpointSuffix=core.windows.net";

        await AzureDataLake.DownloadFiles(
            new Input { ConnectionString = wrongConnStr, ContainerName = containerName },
            new Connection { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfOauthParametersAreInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = containerName,
                StorageAccountName = storageAccount,
                ApplicationID = appID,
                TenantID = tenantID,
                ClientSecret = "wrongSecret"
            },
            new Connection { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = "not-existing-container"
            },
            new Connection { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfInvalidSourceParameters()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = "InvalidContainerName"
            },
            new Connection { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
    }
}
