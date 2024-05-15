namespace Frends.AzureDataLake.DeleteDirectory.Tests;

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Files.DataLake;
using dotenv.net;
using Frends.AzureDataLake.DeleteDirectory.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTests
{
    Input input;

    private readonly string _connectionString = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CONNSTRING"
    );
    private readonly string _appID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_APPID"
    );
    private readonly string _tenantID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_TENANTID"
    );
    private readonly string _clientSecret = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CLIENTSECRET"
    );
    private readonly string _storageAccount = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_STORAGEACCOUNT"
    );
    private string _containerName;
    private string _directoryName;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { $"{projDir}/.env.local" }));
    }

    [TestInitialize]
    public void TestSetup()
    {
        // Generate unique container adn directory names to avoid conflicts when running multiple tests
        _containerName = $"test-container{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";
        _directoryName = $"test-directory{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";
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
    public async Task TestDeleteDirectory()
    {
        input = new Input { ConnectionString = _connectionString, ContainerName = _containerName, DirectoryName = _directoryName };
        DataLakeFileSystemClient container = new DataLakeServiceClient(input.ConnectionString).GetFileSystemClient(input.ContainerName);
        await container.CreateIfNotExistsAsync(null, new CancellationToken());

        var directoryClient = AzureDataLake.GetDataLakeDirectory(input);
        await directoryClient.CreateIfNotExistsAsync(null, new CancellationToken());

        var deleted = await AzureDataLake.DeleteDirectory(input, new Options { ThrowErrorIfDirectoryDoesNotExists = true }, new CancellationToken());
        Assert.IsTrue(deleted.DirectoryWasDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task TestDeleteDirectory_throws_ContainerNotFound()
    {
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = _connectionString, ContainerName = _containerName, DirectoryName = _directoryName }, new Options { ThrowErrorIfDirectoryDoesNotExists = true }, new CancellationToken());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task TestDeleteDirectory_throws_ParameterEmpty()
    {
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = _connectionString, ContainerName = null, DirectoryName = _directoryName }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = null, ContainerName = _containerName, DirectoryName = _directoryName }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = _connectionString, ContainerName = _containerName, DirectoryName = null }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task TestDeleteDirectory_throws_ParameterNotValid()
    {
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = "Not valid parameter", ContainerName = "Valid name", DirectoryName = "Valid name" }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = "name=value", ContainerName = "Valid name", DirectoryName = "Valid name" }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task TestDeleteDirectory_throws_ClientNotFound()
    {
        await AzureDataLake.DeleteDirectory(new Input { ConnectionString = "DefaultEndpointsProtocol=https;AccountName=unitTestStorage;AccountKey=abcdefghijklmnopqrstuyxz123456789;EndpointSuffix=core.windows.net", ContainerName = _containerName, DirectoryName = _directoryName }, new Options { ThrowErrorIfDirectoryDoesNotExists = false }, new CancellationToken());
    }

    [TestMethod]
    public async Task AccessTokenAuthenticationTest()
    {
        var containerName = $"test{Guid.NewGuid()}";
        var directoryName = $"test{Guid.NewGuid()}";

        input = new Input
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ContainerName = containerName,
            DirectoryName = directoryName,
            StorageAccountName = _storageAccount,
            ApplicationID = _appID,
            TenantID = _tenantID,
            ClientSecret = _clientSecret
        };

        var options = new Options
        {
            ThrowErrorIfDirectoryDoesNotExists = false
        };

        DataLakeFileSystemClient container = new DataLakeServiceClient(_connectionString).GetFileSystemClient(input.ContainerName);
        await container.CreateIfNotExistsAsync(null, new CancellationToken());

        var directoryClient = AzureDataLake.GetDataLakeDirectory(input);
        await directoryClient.CreateIfNotExistsAsync(null, new CancellationToken());

        var result = await AzureDataLake.DeleteDirectory(input, options, default);
        Assert.IsTrue(result.DirectoryWasDeleted);
    }
}
