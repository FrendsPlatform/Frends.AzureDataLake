using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class ErrorHandlingTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfAnyExceptionOccured()
    {
        await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task ReturnErrorIfAnyExceptionOccured()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Source(),
            new Destination(),
            new Options { ThrowErrorOnFailure = false },
            CancellationToken.None
        );
        Assert.IsFalse(result.IsSuccess);
        CollectionAssert.AreEqual(result.DownladedFiles, new Dictionary<string, string>());
        Assert.AreNotEqual(result.ErrorMessage, string.Empty);
    }
}
