using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Files.DataLake;
using dotenv.net;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public abstract class TestsBase
{
    protected readonly string connectionString = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CONNSTRING"
    );
    protected readonly string appID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_APPID"
    );
    protected readonly string tenantID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_TENANTID"
    );
    protected readonly string clientSecret = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CLIENTSECRET"
    );
    protected readonly string storageAccount = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_STORAGEACCOUNT"
    );
    protected string containerName;
    protected static readonly string testDirectory = Path.Combine(
        Directory.GetCurrentDirectory(),
        "test"
    );

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        //load envs
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(
            options: new DotEnvOptions(
                envFilePaths: new[] { $"{projDir}{Path.DirectorySeparatorChar}.env.local" }
            )
        );
    }

    [TestInitialize]
    public async void TestSetup()
    {
        containerName = $"DownloadFilesTest{Guid.NewGuid()}";
        await CreateContainer();
        AddFileToContainer("foobar1.txt");
        AddFileToContainer("foobar2.txt");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        // delete whole container after running tests
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        await container.DeleteIfExistsAsync();
    }

    private async Task CreateContainer()
    {
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        await container.CreateAsync();
    }

    protected void AddFileToContainer(string fileName)
    {
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        container.CreateFile(fileName);
    }
}
