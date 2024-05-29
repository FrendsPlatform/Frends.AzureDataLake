using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;
using Frends.AzureDataLake.DownloadFiles.Exceptions;
using Frends.AzureDataLake.UploadFiles.Tests.asserts;
using static Frends.AzureDataLake.DownloadFiles.Definitions.Constants;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class DownloadingTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task ThrowIfDestinationDirectoryDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Source { ConnectionString = connectionString, ContainerName = containerName },
            new Destination { Directory = "C:/NonExistingDir/ForSure/test" },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task DownloadSingleFileWithConnectionString()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                FilePattern = file1a
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, SingleFileResult.DownladedFiles);
        Assert.That.FileExists(Path.Combine(testDirectory, file1a));
    }

    [TestMethod]
    public async Task DownloadSingleFileWithOAuth2()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                ContainerName = containerName,
                StorageAccountName = storageAccount,
                ApplicationID = appID,
                TenantID = tenantID,
                ClientSecret = clientSecret,
                FilePattern = file1a
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, SingleFileResult.DownladedFiles);
        Assert.That.FileExists(Path.Combine(testDirectory, file1a));
    }

    [TestMethod]
    public async Task DownloadNothingIfPatternFileNotFound()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                FilePattern = "nonExisting.txt"
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, new Dictionary<string, string>());
    }

    [TestMethod]
    public async Task DownloadMultipleFiles()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                FilePattern = multiFilePatten
            },
            new Destination { Directory = testDirectory },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.IsSuccess);

        var orderedResultFiles = result
            .DownladedFiles.OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
        var orderedExpectedResultFiles = MultiFileResult
            .DownladedFiles.OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        CollectionAssert.AreEqual(orderedResultFiles, orderedExpectedResultFiles);
        Assert.That.FileExists(Path.Combine(testDirectory, file1a));
        Assert.That.FileExists(Path.Combine(testDirectory, file1b));
        Assert.That.FileExists(Path.Combine(testDirectory, file1c));
        Assert.That.FileDoesNotExist(Path.Combine(testDirectory, file2));
    }

    [TestMethod]
    public async Task OverwriteExistingFile()
    {
        //arrange
        var startContent = "test line";
        using (var streamWriter = File.CreateText(Path.Combine(testDirectory, file1a)))
        {
            streamWriter.Write(startContent);
        }

        //act
        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                FilePattern = file1a
            },
            new Destination { Directory = testDirectory, Overwrite = true },
            new Options(),
            CancellationToken.None
        );
        //assert
        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, SingleFileResult.DownladedFiles);
        Assert.That.FileContainsText(Path.Combine(testDirectory, file1a), string.Empty);
    }

    [TestMethod]
    public async Task ReturnMessageIfFileAlreadyExists()
    {
        //arrange
        var startContent = "test line";
        using (var streamWriter = File.CreateText(Path.Combine(testDirectory, file2)))
        {
            streamWriter.Write(startContent);
        }

        var result = await AzureDataLake.DownloadFiles(
            new Source
            {
                ConnectionString = connectionString,
                ContainerName = containerName,
                FilePattern = file2
            },
            new Destination { Directory = testDirectory, Overwrite = false },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, FileAlreadyExistsResult.DownladedFiles);
        Assert.That.FileContainsText(Path.Combine(testDirectory, file2), startContent);
    }
}
