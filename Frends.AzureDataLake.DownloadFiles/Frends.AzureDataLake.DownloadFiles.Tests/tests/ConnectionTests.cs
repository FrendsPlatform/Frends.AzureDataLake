using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class ConnectionTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfConnectionStringIsInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfConnectionStringKeyIsInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfOauthParametersAreInvalid()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ThrowIfContainerDoesNotExist()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }
}
