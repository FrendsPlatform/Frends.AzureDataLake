using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.UploadFiles.Definitions;
using Frends.AzureDataLake.UploadFiles.Exceptions;
using Frends.AzureDataLake.UploadFiles.Tests.asserts;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class OptionsTests : TestsBase
{
    [TestMethod]
    public async Task ReturnsErrorWhenErrorOccurs()
    {
        var result = await AzureDataLake.UploadFiles(
            new Input(),
            new Connection(),
            new Options { ThrowErrorOnFailure = false },
            new CancellationToken()
        );
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsNotNull(result.Error.Message);
    }

    [TestMethod]
    public async Task ReturnsCustomErrorMessageOnFailure()
    {
        var result = await AzureDataLake.UploadFiles(
            new Input(),
            new Connection(),
            new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error" },
            new CancellationToken()
        );
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Custom error", result.Error.Message);
        Assert.IsNotNull(result.Error.AdditionalInfo);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfErrorOccurs()
    {
        await AzureDataLake.UploadFiles(
            new Input(),
            new Connection(),
            new Options { ThrowErrorOnFailure = true },
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task OverwriteFile()
    {
        await CreateContainer();
        AddFileToContainer("foobar1.txt");
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName,
                ActionOnExistingFile = HandleExistingFile.Overwrite
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    [ExpectedException(typeof(FileAlreadyExistsException))]
    public async Task ThrowIfFileAlreadyExists()
    {
        await CreateContainer();
        AddFileToContainer("foobar1.txt");
        await AzureDataLake.UploadFiles(
            new Input
            {
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName,
                ActionOnExistingFile = HandleExistingFile.Error
            },
            new Connection { ConnectionString = connectionString },
            new Options { ThrowErrorOnFailure = true },
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task SkipFileIfAlreadyExists_WhenThrowErrorOnFailureIsFalse()
    {
        await CreateContainer();
        AddFileToContainer("foobar1.txt");
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                SourceDirectory = testDirectory,
                FilePattern = "foobar1.txt",
                ContainerName = containerName,
                ActionOnExistingFile = HandleExistingFile.Error
            },
            new Connection { ConnectionString = connectionString },
            new Options { ThrowErrorOnFailure = false },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, result.Files.Count);
    }
}
