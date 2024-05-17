using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Files.DataLake;
using Frends.AzureDataLake.CreateContainer.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.AzureDataLake.CreateContainer.Tests;

[TestClass]
public class UnitTests
{
    Input input;
    private readonly string _connectionString = Environment.GetEnvironmentVariable("Frends_AzureBlobStorage_ConnString");
    private readonly string _appID = Environment.GetEnvironmentVariable("Frends_AzureBlobStorage_AppID");
    private readonly string _tenantID = Environment.GetEnvironmentVariable("Frends_AzureBlobStorage_TenantID");
    private readonly string _clientSecret = Environment.GetEnvironmentVariable("Frends_AzureBlobStorage_ClientSecret");
    private readonly string _storageAccount = "frendstaskstestcontainer";
    private string _containerName;

    [TestInitialize]
    public void TestSetup()
    {
        // Generate unique container name to avoid conflicts when running multiple tests
        _containerName =
            $"test-container{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        // delete whole container after running tests
        var client = new DataLakeServiceClient(_connectionString);
        var container = client.GetFileSystemClient(_containerName);
        await container.DeleteIfExistsAsync();
    }

    [TestMethod]
    public async Task TestCreateContainer()
    {
        var result = await AzureDataLake.CreateContainer(
            new Input { ConnectionString = _connectionString, ContainerName = _containerName },
            new CancellationToken()
        );

        Assert.IsNotNull(result);
        Assert.AreEqual(
            new BlobClient(_connectionString, _containerName, "").Uri.ToString(),
            result.Uri
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task TestCreateContainer_throws_ParameterNotValid()
    {
        await AzureDataLake.CreateContainer(
            new Input { ConnectionString = "Not valid parameter", ContainerName = "Valid name" },
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task TestCreateContainer_throws_ClientNotFound()
    {
        await AzureDataLake.CreateContainer(
            new Input
            {
                ConnectionString =
                    "DefaultEndpointsProtocol=https;AccountName=unitTestStorage;AccountKey=abcdefghijklmnopqrstuyxz123456789;EndpointSuffix=core.windows.net",
                ContainerName = _containerName
            },
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task AccessTokenAuthenticationTest()
    {
        var containerName = $"test{Guid.NewGuid()}";

        input = new Input
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ContainerName = containerName,
            StorageAccountName = _storageAccount,
            ApplicationID = _appID,
            TenantID = _tenantID,
            ClientSecret = _clientSecret
        };

        var result = await AzureDataLake.CreateContainer(input, default);
        Assert.IsTrue(result.Success);
    }
}