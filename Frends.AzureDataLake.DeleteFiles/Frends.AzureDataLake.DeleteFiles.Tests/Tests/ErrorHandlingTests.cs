using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteFiles.Definitions;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Tests;

[TestClass]
public class ErrorHandlingTests : TestsBase
{
    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task ThrowIfAnyExceptionOccured()
    {
        await AzureDataLake.DeleteFiles(new Input(), new Options(), CancellationToken.None);
    }

    [TestMethod]
    public async Task ReturnErrorIfAnyExceptionOccured()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input(),
            new Options { ThrowErrorOnFailure = false },
            CancellationToken.None
        );
        Assert.IsFalse(result.IsSuccess);
        CollectionAssert.AreEqual(result.DeletedFiles, new List<string>());
        Assert.AreNotEqual(result.ErrorMessage, string.Empty);
    }
}
