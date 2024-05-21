using Azure.Storage.Files.DataLake;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Asserts;

static class DataLakeAsserts
{
    public static void FileExistsInContainer(
        this Assert assert,
        string connString,
        string containerName,
        string fileName
    )
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        var fileClient = client.GetFileClient(fileName);
        Assert.IsTrue(fileClient.Exists());
    }

    public static void FileDoesNotExistInContainer(
        this Assert assert,
        string connString,
        string containerName,
        string fileName
    )
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        var fileClient = client.GetFileClient(fileName);
        Assert.IsFalse(fileClient.Exists());
    }
}
