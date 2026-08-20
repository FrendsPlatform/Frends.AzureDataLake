using System;
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
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfDestinationDirectoryDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                DestinationDirectory = "C:/NonExistingDir/ForSure/test"
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task DownloadSingleFileWithConnectionString()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                FilePattern = file1a,
                DestinationDirectory = testDirectory
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);
        CollectionAssert.AreEqual(result.DownladedFiles, SingleFileResult.DownladedFiles);
        Assert.That.FileExists(Path.Combine(testDirectory, file1a));
    }

    [TestMethod]
    public async Task DownloadSingleFileWithOAuth2()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                FilePattern = file1a,
                DestinationDirectory = testDirectory
            },
            new Connection
            {
                ConnectionMethod = ConnectionMethod.OAuth2,
                StorageAccountName = storageAccount,
                ApplicationID = appID,
                TenantID = tenantID,
                ClientSecret = clientSecret
            },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);
        CollectionAssert.AreEqual(result.DownladedFiles, SingleFileResult.DownladedFiles);
        Assert.That.FileExists(Path.Combine(testDirectory, file1a));
    }

    [TestMethod]
    public async Task DownloadNothingIfPatternFileNotFound()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                FilePattern = "nonExisting.txt",
                DestinationDirectory = testDirectory
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);
        CollectionAssert.AreEqual(result.DownladedFiles, new Dictionary<string, string>());
    }

    [TestMethod]
    public async Task DownloadMultipleFiles()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Input
            {
                ContainerName = containerName,
                FilePattern = multiFilePatten,
                DestinationDirectory = testDirectory
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);

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
            new Input
            {
                ContainerName = containerName,
                FilePattern = file1a,
                DestinationDirectory = testDirectory,
                Overwrite = true
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        //assert
        Assert.IsTrue(result.Success);
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
            new Input
            {
                ContainerName = containerName,
                FilePattern = file2,
                DestinationDirectory = testDirectory,
                Overwrite = false
            },
            new Connection { ConnectionString = connectionString },
            new Options(),
            CancellationToken.None
        );
        Assert.IsTrue(result.Success);
        CollectionAssert.AreEqual(result.DownladedFiles, FileAlreadyExistsResult.DownladedFiles);
        Assert.That.FileContainsText(Path.Combine(testDirectory, file2), startContent);
    }
}
