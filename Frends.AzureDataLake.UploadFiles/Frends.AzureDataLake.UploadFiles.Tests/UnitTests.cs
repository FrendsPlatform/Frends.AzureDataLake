using System;
using System.IO;
using dotenv.net;

namespace Frends.AzureDataLake.UploadFiles.Tests;

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
    private string _containerName;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { $"{projDir}/.env.local" }));
    }

    [TestMethod]
    public void Test1() { }
}
