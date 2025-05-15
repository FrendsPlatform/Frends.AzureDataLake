namespace Frends.AzureDataLake.ListFiles.Tests;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net;
using Frends.AzureDataLake.ListFiles.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTests
{
    private readonly string connectionString = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CONNSTRING");

    private readonly string appID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_APPID");

    private readonly string tenantID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_TENANTID");

    private readonly string clientSecret = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CLIENTSECRET");

    private readonly string storageAccount = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_STORAGEACCOUNT");

    private readonly string containerName = $"test-container{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { $"{projDir}/.env.local" }));
    }

    [TestInitialize]
    public async Task Init()
    {
        await Helper.CreateContainerAndTestFiles(false, connectionString, containerName);
    }

    [TestCleanup]
    public async Task CleanUp()
    {
        await Helper.CreateContainerAndTestFiles(true, connectionString, containerName);
    }

    [TestMethod]
    public async Task LisFiles_ConnectionString_ListingStructures()
    {
        var recursiveOptions = new List<bool>() { true, false };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.ConnectionString,
            ConnectionString = connectionString,
            ContainerName = containerName,
        };

        foreach (var isRecursive in recursiveOptions)
        {
            var options = new Options
            {
                DictionaryName = null,
                Recursive = isRecursive,
            };

            var result = await AzureDataLake.ListFiles(source, options, default);

            if (isRecursive)
            {
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile"));
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile2"));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile")));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile2")));
            }
            else
            {
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp"));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp")));
            }

            Assert.IsTrue(result.FileList.Any(x => x.Name == "TestFile.txt"));
            Assert.IsTrue(result.FileList.Any(x => x.Name == "TestFile2.txt"));
        }
    }

    [TestMethod]
    public async Task ListFiles_ConnectionString_Prefix()
    {
        var recursiveOptions = new List<bool>() { true, false };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.ConnectionString,
            ConnectionString = connectionString,
            ContainerName = containerName,
        };

        foreach (var isRecursive in recursiveOptions)
        {
            var options = new Options
            {
                DictionaryName = "Temp",
                Recursive = isRecursive,
            };

            var result = await AzureDataLake.ListFiles(source, options, default);

            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile"));
            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile2"));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile")));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile2")));

            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile.txt"));
            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile2.txt"));
        }
    }

    [TestMethod]
    public async Task ListFiles_ConnectionString_ConnectionStringMissing()
    {
        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.ConnectionString,
            ConnectionString = string.Empty,
            ContainerName = containerName,
        };

        var options = new Options
        {
            DictionaryName = "/tes",
            Recursive = false,
        };

        var ex = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await AzureDataLake.ListFiles(source, options, default));
        Assert.AreEqual("Value cannot be null. (Parameter 'connectionString')", ex.Message);
    }

    [TestMethod]
    public async Task ListFiles_OAuth_ListingStructures()
    {
        var recursiveOptions = new List<bool>() { true, false };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ApplicationID = appID,
            TenantID = tenantID,
            ClientSecret = clientSecret,
            StorageAccountName = storageAccount,
            ContainerName = containerName,
        };

        foreach (var isRecursive in recursiveOptions)
        {
            var options = new Options
            {
                DictionaryName = null,
                Recursive = isRecursive,
            };

            var result = await AzureDataLake.ListFiles(source, options, default);

            if (isRecursive)
            {
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile"));
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile2"));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile")));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile2")));
            }
            else
            {
                Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp"));
                Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp")));
            }

            Assert.IsTrue(result.FileList.Any(x => x.Name == "TestFile.txt"));
            Assert.IsTrue(result.FileList.Any(x => x.Name == "TestFile2.txt"));
        }
    }

    [TestMethod]
    public async Task ListFiles_OAuth_Prefix()
    {
        var recursiveOptions = new List<bool>() { true, false };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ApplicationID = appID,
            TenantID = tenantID,
            ClientSecret = clientSecret,
            StorageAccountName = storageAccount,
            ContainerName = containerName,
        };

        foreach (var isRecursive in recursiveOptions)
        {
            var options = new Options
            {
                DictionaryName = "Temp",
                Recursive = isRecursive,
            };

            var result = await AzureDataLake.ListFiles(source, options, default);

            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile"));
            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile2"));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile")));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile2")));

            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile.txt"));
            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile2.txt"));
        }
    }
}
