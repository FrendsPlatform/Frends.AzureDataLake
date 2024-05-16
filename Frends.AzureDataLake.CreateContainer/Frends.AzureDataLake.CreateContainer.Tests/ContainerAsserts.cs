using Azure.Storage.Files.DataLake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.AzureDataLake.CreateContainer.Tests;
static class ContainerAsserts
{
    public static void ContainerExists(this Assert assert, string connString, string containerName)
    {
        var dataLakeServiceClient = new DataLakeServiceClient(connString);
        var client = dataLakeServiceClient.GetFileSystemClient(containerName);
        if (client.Exists())
        {
            return;
        }
        else
        {
            throw new AssertFailedException("Created container does not exists");
        }
    }
}