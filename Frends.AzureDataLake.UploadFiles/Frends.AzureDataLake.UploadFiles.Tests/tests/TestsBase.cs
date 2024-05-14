using System;
using System.IO;
using dotenv.net;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class TestsBase
{
    protected readonly string _connectionString = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CONNSTRING"
    );
    protected readonly string _appID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_APPID"
    );
    protected readonly string _tenantID = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_TENANTID"
    );
    protected readonly string _clientSecret = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_CLIENTSECRET"
    );
    protected readonly string _storageAccount = Environment.GetEnvironmentVariable(
        "FRENDS_AZUREDATALAKE_STORAGEACCOUNT"
    );
    protected string _containerName;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { $"{projDir}/.env.local" }));
    }
}
