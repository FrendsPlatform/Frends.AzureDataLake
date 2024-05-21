using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteFiles.Definitions;
using Frends.AzureDataLake.DeleteFiles.Tests.Asserts;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Tests;

[TestClass]
public class DeletingFilesTests : TestsBase
{
    [TestMethod]
    public async Task DeleteSingleFile()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                DeleteFilePattern = file1a
            },
            new Options(),
            CancellationToken.None
        );

        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(SingleFileDeleted.DeletedFiles, result.DeletedFiles);
        Assert.That.FileDoesNotExistInContainer(connectionString, containerName, file1a);
        Assert.That.FileExistsInContainer(connectionString, containerName, file1b);
    }

    [TestMethod]
    public async Task DeleteMultipleFiles()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                DeleteFilePattern = multiFilePattern
            },
            new Options(),
            CancellationToken.None
        );

        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(MultiFileDeleted.DeletedFiles, result.DeletedFiles);
        Assert.That.FileDoesNotExistInContainer(connectionString, containerName, file1a);
        Assert.That.FileDoesNotExistInContainer(connectionString, containerName, file1b);
        Assert.That.FileDoesNotExistInContainer(connectionString, containerName, file1c);
        Assert.That.FileExistsInContainer(connectionString, containerName, file2);
    }

    [TestMethod]
    public async Task DeleteNothingIfPatternFileNotFound()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                DeleteFilePattern = "notsuchfile.xd"
            },
            new Options(),
            CancellationToken.None
        );

        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(MultiFileDeleted.DeletedFiles, result.DeletedFiles);
        Assert.That.FileDoesNotExistInContainer(connectionString, containerName, file1a);
        Assert.That.FileExistsInContainer(connectionString, containerName, file2);
    }
}
