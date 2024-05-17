using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class DownloadingTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfDestinationDirectoryDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task DownloadSingleFileWithConnectionString()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
        Assert.AreEqual(result, SingleFileResult);
    }

    [TestMethod]
    public async Task DownloadSingleFileWithOAuth2()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
        Assert.AreEqual(result, SingleFileResult);
    }

    [TestMethod]
    public async Task DownloadMultipleFiles()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
        Assert.AreEqual(result, MultiFileResult);
    }

    [TestMethod]
    public async Task OverwriteExistingFile()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
        Assert.AreEqual(result, SingleFileResult);
    }

    [TestMethod]
    public async Task ReturnMessageIfFileAlreadyExists()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
        Assert.AreEqual(result, FileAlreadyExistsResult);
    }
}
