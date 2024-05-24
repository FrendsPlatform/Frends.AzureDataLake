using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Files.DataLake;
using dotenv.net;
using Frends.AzureDataLake.DeleteFiles.Definitions;

namespace Frends.AzureDataLake.DeleteFiles.Tests;

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
    protected string testDirectory;

    protected static readonly string file1a = "foobar1a.txt";
    protected static readonly string file1b = "foobar1b.txt";
    protected static readonly string file1c = "innerDir/foobar1c.txt";
    protected static readonly string file2 = "foobar2.txt";

    protected static readonly string multiFilePattern = "*bar1*";
    protected string AzDataLakeUrlPrefix => $"https://{storageAccount}.blob.core.windows.net/";

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
    public async Task TestSetup()
    {
        testDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"test-{Guid.NewGuid()}");
        Directory.CreateDirectory(testDirectory);
        containerName = $"download-files-test{Guid.NewGuid()}";
        await CreateContainer();
        AddFileToContainer(file1a);
        AddFileToContainer(file1b);
        AddFileToContainer(file1c);
        AddFileToContainer(file2);
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DeleteContainer();
        Directory.Delete(testDirectory, true);
    }

    private async Task CreateContainer()
    {
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        await container.CreateAsync();
    }

    private async Task DeleteContainer()
    {
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        await container.DeleteIfExistsAsync();
    }

    protected void AddFileToContainer(string fileName)
    {
        var client = new DataLakeServiceClient(connectionString);
        var container = client.GetFileSystemClient(containerName);
        container.CreateFile(fileName);
    }

    protected Result SingleFileDeleted =>
        new()
        {
            IsSuccess = true,
            DeletedFiles = new List<string> { $"{AzDataLakeUrlPrefix}{containerName}/{file1a}", },
        };

    protected Result MultiFileDeleted =>
        new()
        {
            IsSuccess = true,
            DeletedFiles = new List<string>
            {
                $"{AzDataLakeUrlPrefix}{containerName}/{file1a}",
                $"{AzDataLakeUrlPrefix}{containerName}/{file1b}",
                $"{AzDataLakeUrlPrefix}{containerName}/{file1c}"
            },
        };
}
