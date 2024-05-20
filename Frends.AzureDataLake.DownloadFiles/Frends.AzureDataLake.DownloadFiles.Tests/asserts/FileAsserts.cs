using System.IO;
using Azure.Storage.Files.DataLake;

namespace Frends.AzureDataLake.UploadFiles.Tests.asserts;

static class FileAsserts
{
    public static void FileExists(this Assert assert, string filePath)
    {
        Assert.IsTrue(File.Exists(filePath));
    }

    public static void FileDoesNotExist(this Assert assert, string filePath)
    {
        Assert.IsFalse(File.Exists(filePath));
    }

    public static void FileContainsText(this Assert assert, string filePath, string text)
    {
        var fileContent = File.ReadAllText(filePath);
        Assert.AreEqual(text, fileContent);
    }
}
