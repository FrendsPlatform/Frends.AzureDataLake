using Azure.Storage.Files.DataLake;

namespace Frends.AzureDataLake.UploadFiles.Tests.asserts;

static class DataLakeAsserts
{
    public static void ContainerExists(this Assert assert, string connString, string containerName)
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        Assert.IsTrue(client.Exists());
    }

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
