using System;
using System.Linq;
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
    public async Task ReturnsMessageWhenErrorOccures()
    {
        var result = await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination(),
                Options = new Options { ThrowErrorOnFailure = false },
            },
            new CancellationToken()
        );
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.ErrorMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfErrorOccures()
    {
        await AzureDataLake.UploadFiles(
            new Input
            {
                Source = new Source(),
                Destination = new Destination(),
                Options = new Options { ThrowErrorOnFailure = true },
            },
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

                Options = new Options { Overwrite = true },
            },
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

                Options = new Options { Overwrite = false, ThrowErrorOnFailure = true },
            },
            new CancellationToken()
        );
    }

    [TestMethod]
    public async Task ReturnMessageIfFileAlreadyExists()
    {
        await CreateContainer();
        AddFileToContainer("foobar1.txt");
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

                Options = new Options { Overwrite = false, ThrowErrorOnFailure = false },
            },
            new CancellationToken()
        );
        Assert.That.FileExistsInContainer(connectionString, containerName, "foobar1.txt");
        Assert.IsTrue(result.Success);
        Assert.AreEqual("File already exists", result.Data.Values.First());
    }
}
