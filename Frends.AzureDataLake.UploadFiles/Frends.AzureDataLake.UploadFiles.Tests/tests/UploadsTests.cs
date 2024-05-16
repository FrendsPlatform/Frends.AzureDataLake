using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.UploadFiles.Definitions;
using Frends.AzureDataLake.UploadFiles.Tests.asserts;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class UploadsTests : TestsBase
{
    [TestMethod]
    public async Task UploadSingleFileWithConnectionString()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source
                {
                    SourceDirectory = testDirectory,
                    SourceFilePattern = "foobar1.txt"
                },

                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = containerName
                },

                Options = new Options(),
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UploadSingleFileWithOAuth2()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source
                {
                    SourceDirectory = testDirectory,
                    SourceFilePattern = "foobar1.txt"
                },

                Destination = new Destination
                {
                    ConnectionMethod = ConnectionMethod.OAuth2,
                    ContainerName = containerName,
                    StorageAccountName = storageAccount,
                    ApplicationID = appID,
                    TenantID = tenantID,
                    ClientSecret = clientSecret
                },
                Options = new Options(),
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UploadFilesRecursively()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source { SourceDirectory = testDirectory, SourceFilePattern = "*" },

                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = containerName
                },

                Options = new Options { UploadFilesRecursively = true },
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar2.txt");
        Assert.That.FileExistsInContainer(connectionString, containerName, "nestedDir/foobar3.txt");
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UploadFilesNonRecursively()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source { SourceDirectory = testDirectory, SourceFilePattern = "*" },

                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = containerName
                },

                Options = new Options { UploadFilesRecursively = false },
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar2.txt");
        Assert.That.FileDoesNotExistInContainer(
            connectionString,
            containerName,
            "nestedDir/foobar3.txt"
        );
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UploadFileToSpecifiedFolder()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source
                {
                    SourceDirectory = testDirectory,
                    SourceFilePattern = "foobar1.txt"
                },

                Destination = new Destination
                {
                    ConnectionString = connectionString,
                    ContainerName = containerName,
                    DestinationFolderName = "SpecialFolder"
                },
                Options = new Options(),
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(
            connectionString,
            containerName,
            "SpecialFolder/foobar1.txt"
        );
        Assert.IsTrue(result.Success);
    }
}
