using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Files.DataLake;
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
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
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
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName
            },
            new Connection
            {
                AuthenticationMethod = AuthenticationMethod.OAuth2,
                StorageAccountName = storageAccount,
                ApplicationId = appID,
                TenantId = tenantID,
                ClientSecret = clientSecret
            },
            new Options(),
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
                SourceDirectory = testDirectory,
                FilePattern = "*",
                ContainerName = containerName,
                UploadFilesRecursively = true
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
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
                SourceDirectory = testDirectory,
                FilePattern = "*",
                ContainerName = containerName,
                UploadFilesRecursively = false
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
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
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName,
                TargetDirectory = "SpecialFolder"
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(
            connectionString,
            containerName,
            "SpecialFolder/foobar1.txt"
        );
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UploadFile_WithCloseTrue_ShouldCompleteSuccessfully()
    {
        await CreateContainer();
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                SourceDirectory = testDirectory,
                FilePattern = "foobar4.txt",
                ContainerName = containerName,
                Close = true
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);
    }
}
