using Azure.Storage.Files.DataLake;

namespace Frends.AzureDataLake.DeleteContainer.Tests;

static class ContainerAsserts
{
    public static void ContainerExists(this Assert assert, string connString, string containerName)
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        if (client.Exists())
            return;
        else
            throw new AssertFailedException("Container does not exists");
    }

    public static void ContainerDoesNotExist(
        this Assert assert,
        string connString,
        string containerName
    )
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        if (!client.Exists())
            return;
        else
            throw new AssertFailedException("Created exists");
    }
}
