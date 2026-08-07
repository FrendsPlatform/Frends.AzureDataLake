namespace Frends.AzureDataLake.DeleteDirectory.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteDirectory.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ErrorHandlerTests
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput => new Input
    {
        ConnectionString = "Not a valid connection string",
        ContainerName = "test-container",
        DirectoryName = "test-directory",
    };

    [TestMethod]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var options = new Options { ThrowErrorOnFailure = true };
        await Assert.ThrowsExceptionAsync<Exception>(() =>
            AzureDataLake.DeleteDirectory(InvalidInput, options, CancellationToken.None));
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await AzureDataLake.DeleteDirectory(InvalidInput, options, CancellationToken.None);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = CustomErrorMessage };
        var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
            AzureDataLake.DeleteDirectory(InvalidInput, options, CancellationToken.None));
        Assert.IsNotNull(ex);
        StringAssert.Contains(ex.Message, CustomErrorMessage);
    }
}
