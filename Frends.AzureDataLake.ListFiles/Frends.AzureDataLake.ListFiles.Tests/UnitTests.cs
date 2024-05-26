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
    private readonly string _connectionString = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CONNSTRING"
    );
    private readonly string _appID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_APPID"
    );
    private readonly string _tenantID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_TENANTID"
    );
    private readonly string _clientSecret = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CLIENTSECRET"
    );
    private readonly string _storageAccount = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_STORAGEACCOUNT"
    );
    private readonly string _containerName = $"test-container{DateTime.Now.ToString("mmssffffff", CultureInfo.InvariantCulture)}";

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
        await Helper.CreateContainerAndTestFiles(false, _connectionString, _containerName);
    }

    [TestCleanup]
    public async Task CleanUp()
    {
        await Helper.CreateContainerAndTestFiles(true, _connectionString, _containerName);
    }

    [TestMethod]
    public async Task LisFiles_ConnectionString_ListingStructures()
    {
        var listing = new List<ListingStructure>() { ListingStructure.Flat, ListingStructure.Hierarchical };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.ConnectionString,
            ConnectionString = _connectionString,
            ContainerName = _containerName
        };

        foreach (var structure in listing)
        {
            var options = new Options
            {
                DictionaryName = null,
                ListingStructure = structure
            };

            var result = await AzureDataLake.ListFilesInContainer(source, options, default);

            if (structure is ListingStructure.Flat)
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
        var listing = new List<ListingStructure>() { ListingStructure.Flat, ListingStructure.Hierarchical };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.ConnectionString,
            ConnectionString = _connectionString,
            ContainerName = _containerName
        };

        foreach (var structure in listing)
        {
            var options = new Options
            {
                DictionaryName = "Temp",
                ListingStructure = structure
            };

            var result = await AzureDataLake.ListFilesInContainer(source, options, default);

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
            ConnectionString = "",
            ContainerName = _containerName,
        };

        var options = new Options
        {
            DictionaryName = "/tes",
            ListingStructure = ListingStructure.Hierarchical
        };

        var ex = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await AzureDataLake.ListFilesInContainer(source, options, default));
        Assert.AreEqual("Value cannot be null. (Parameter 'connectionString')", ex.Message);
    }

    [TestMethod]
    public async Task ListFiles_OAuth_ListingStructures()
    {
        var listing = new List<ListingStructure>() { ListingStructure.Flat, ListingStructure.Hierarchical };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ApplicationID = _appID,
            TenantID = _tenantID,
            ClientSecret = _clientSecret,
            StorageAccountName = _storageAccount,
            ContainerName = _containerName,
        };

        foreach (var structure in listing)
        {
            var options = new Options
            {
                DictionaryName = null,
                ListingStructure = structure
            };

            var result = await AzureDataLake.ListFilesInContainer(source, options, default);

            if (structure is ListingStructure.Flat)
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
        var listing = new List<ListingStructure>() { ListingStructure.Flat, ListingStructure.Hierarchical };

        var source = new Source
        {
            ConnectionMethod = ConnectionMethod.OAuth2,
            ApplicationID = _appID,
            TenantID = _tenantID,
            ClientSecret = _clientSecret,
            StorageAccountName = _storageAccount,
            ContainerName = _containerName,
        };

        foreach (var structure in listing)
        {
            var options = new Options
            {
                DictionaryName = "Temp",
                ListingStructure = structure
            };

            var result = await AzureDataLake.ListFilesInContainer(source, options, default);

            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile"));
            Assert.IsTrue(result.FileList.Any(x => x.Name == "Temp/SubFolderFile2"));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile")));
            Assert.IsTrue(result.FileList.Any(x => x.URL.Contains("/Temp/SubFolderFile2")));

            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile.txt"));
            Assert.IsFalse(result.FileList.Any(x => x.Name == "TestFile2.txt"));
        }
    }
}
