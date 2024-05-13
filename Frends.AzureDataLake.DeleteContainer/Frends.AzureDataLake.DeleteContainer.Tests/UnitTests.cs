using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using dotenv.net;
using Frends.AzureDataLake.DeleteContainer.Definitions;
using Frends.AzureDataLake.DeleteContainer.Exceptions;

namespace Frends.AzureDataLake.DeleteContainer.Tests;

[TestClass]
public class UnitTests
{
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
    public async Task DeleteContainerWhenExists()
    {
        await CreateContainer();
        var result = await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = _connectionString, ContainerName = _containerName },
            new Options(),
            new CancellationToken()
        );
        Assert.That.ContainerDoesNotExist(_connectionString, _containerName);
        Assert.IsTrue(result.ContainerWasDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(ContainerNotFoundException))]
    public async Task DeleteContainerWhenDoesNotExistWithExceptionOptionEnabled()
    {
        Assert.That.ContainerDoesNotExist(_connectionString, _containerName);
        await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = _connectionString, ContainerName = _containerName },
            new Options { ThrowErrorIfContainerDoesNotExist = true },
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task DeleteContainerWhenDoesNotExistWithExceptionOptionDisabled()
    {
        Assert.That.ContainerDoesNotExist(_connectionString, _containerName);
        var result = await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = _connectionString, ContainerName = _containerName },
            new Options { ThrowErrorIfContainerDoesNotExist = false },
            new CancellationToken()
        );
        Assert.IsFalse(result.ContainerWasDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task DeleteContainerThrowsInvalidOAuthParameters()
    {
        await AzureDataLake.DeleteContainer(
            new Input
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = _containerName,
                StorageAccountName = _storageAccount,
                ApplicationID = _appID,
                TenantID = null,
                ClientSecret = ""
            },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task DeleteContainerThrowsInvalidConnectionString()
    {
        await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = "", ContainerName = _containerName },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task DeleteContainerThrowsInvalidContainerName()
    {
        await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = _connectionString, ContainerName = "" },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationFailedException))]
    public async Task DeleteContainerThrowsAuthenticationFailedException()
    {
        await AzureDataLake.DeleteContainer(
            new Input
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = _containerName,
                StorageAccountName = _storageAccount,
                ApplicationID = _appID,
                TenantID = _tenantID,
                ClientSecret = "wrongSecret"
            },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public async Task DeleteContainerThrowsFormatException()
    {
        var wrongConnectionString = $"xxx{_connectionString}";
        await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = wrongConnectionString, ContainerName = _containerName },
            new Options(),
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task DeleteContainerWithOAuth()
    {
        await CreateContainer();
        var result = await AzureDataLake.DeleteContainer(
            new Input
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = _containerName,
                StorageAccountName = _storageAccount,
                ApplicationID = _appID,
                TenantID = _tenantID,
                ClientSecret = _clientSecret
            },
            new Options(),
            new CancellationToken()
        );
        Assert.That.ContainerDoesNotExist(_connectionString, _containerName);
        Assert.IsTrue(result.ContainerWasDeleted);
    }

    private async Task CreateContainer()
    {
        var dataLakeServiceClient = new DataLakeServiceClient(_connectionString);
        var container = dataLakeServiceClient.GetFileSystemClient(_containerName);
        await container.CreateAsync();
        Assert.That.ContainerExists(_connectionString, _containerName);
    }
}
