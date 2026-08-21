using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DownloadFiles.Definitions;

namespace Frends.AzureDataLake.DownloadFiles.Tests.tests;

[TestClass]
public class ErrorHandlerTest : TestsBase
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        await AzureDataLake.DownloadFiles(
            new Input(),
            new Connection(),
            new Options { ThrowErrorOnFailure = true },
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var result = await AzureDataLake.DownloadFiles(
            new Input(),
            new Connection(),
            new Options { ThrowErrorOnFailure = false },
            CancellationToken.None
        );
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            await AzureDataLake.DownloadFiles(
                new Input(),
                new Connection(),
                new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = CustomErrorMessage },
                CancellationToken.None
            )
        );
        Assert.IsNotNull(ex);
        StringAssert.Contains(ex.Message, CustomErrorMessage);
    }
}
