using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteFiles.Definitions;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Tests;

[TestClass]
public class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput =>
        new() { ConnectionString = "Not a valid connection string", ContainerName = "validname" };

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        await AzureDataLake.DeleteFiles(InvalidInput, new Options(), CancellationToken.None);
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await AzureDataLake.DeleteFiles(InvalidInput, options, CancellationToken.None);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsFalse(string.IsNullOrEmpty(result.Error.Message));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = CustomErrorMessage
        };

        try
        {
            await AzureDataLake.DeleteFiles(InvalidInput, options, CancellationToken.None);
        }
        catch (Exception ex)
        {
            StringAssert.Contains(ex.Message, CustomErrorMessage);
            throw;
        }
    }
}
