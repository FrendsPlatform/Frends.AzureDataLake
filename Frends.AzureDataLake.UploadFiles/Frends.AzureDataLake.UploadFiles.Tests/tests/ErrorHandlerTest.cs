using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.UploadFiles.Definitions;

namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput =>
        new()
        {
            Source = new Source(),
            Destination = new Destination(),
        };

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        await AzureDataLake.UploadFiles(InvalidInput, new Options { ThrowErrorOnFailure = true }, CancellationToken.None);
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await AzureDataLake.UploadFiles(InvalidInput, options, CancellationToken.None);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsFalse(string.IsNullOrEmpty(result.Error.Message));
    }

    [TestMethod]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = CustomErrorMessage
        };
        var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
            AzureDataLake.UploadFiles(InvalidInput, options, CancellationToken.None));
        Assert.IsNotNull(ex);
        Assert.IsTrue(ex.Message.Contains(CustomErrorMessage));
    }
}
