using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Files.DataLake;
using dotenv.net;
using Frends.AzureDataLake.UploadFiles.Tests.asserts;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class TestsBase
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

        //create source test folder and files
        Directory.CreateDirectory(testDirectory);
        File.Create(Path.Combine(testDirectory, "foobar1.txt"));
        File.Create(Path.Combine(testDirectory, "foobar2.txt"));
        Directory.CreateDirectory(Path.Combine(testDirectory, "nestedDir"));
        File.Create(Path.Combine(testDirectory, "nestedDir", "foobar3.txt"));
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        // Directory.Delete(testDirectory, true);
    }

    [TestInitialize]
    public void TestSetup()
    {
        // Generate unique container name to avoid conflicts when running multiple tests
        containerName =
            $"test-container{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        // delete whole container after running tests
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        await container.DeleteIfExistsAsync();
    }

    protected async Task CreateContainer()
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connectionString);
        var container = dataLakeServiceClient.GetFileSystemClient(containerName);
        await container.CreateAsync();
        Assert.That.ContainerExists(connectionString, containerName);
    }

    protected void AddFileToContainer(string fileName)
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connectionString);
        var container = dataLakeServiceClient.GetFileSystemClient(containerName);
        container.CreateFile(fileName);
    }
}
