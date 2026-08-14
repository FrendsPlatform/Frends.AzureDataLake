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
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = containerName
                },
            },
            new Options(),

            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfWrongConnectionStringCredentials()
    {
        var wrongConnString =
            "DefaultEndpointsProtocol=https;AccountName=frendstemplates;AccountKey=000000000wrongKey00000000000000000000000000000000000000000000000000000000000000000000000;EndpointSuffix=core.windows.net";
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination
                {
                    ConnectionString = wrongConnString,
                    ContainerName = containerName
                },
            },
            new Options(),

            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfWrongOauthCredentials()
    {
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination
                {
                    ConnectionMethod = ConnectionMethod.OAuth2,
                    ContainerName = containerName,
                    StorageAccountName = storageAccount,
                    ApplicationID = appID,
                    TenantID = tenantID,
                    ClientSecret = "wrongSecret"
                },
            },
            new Options(),

            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfWrongConnectionString()
    {
        var wrongConnectionString = $"xxx{connectionString}";
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination
                {
                    ConnectionString = wrongConnectionString,
                    ContainerName = containerName
                },
            },
            new Options(),

            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfParametersNotValid()
    {
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),

                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = ""
                },
            },
            new Options(),

            new CancellationToken()
        );
    }
}
