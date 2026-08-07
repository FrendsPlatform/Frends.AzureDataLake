using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteContainer.Definitions;

namespace Frends.AzureDataLake.DeleteContainer.Tests;

[TestClass]
public class ErrorHandlerTests
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    [TestMethod]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = string.Empty,
        };

        // Trigger a runtime error by using a malformed connection string
        await Assert.ThrowsExceptionAsync<Exception>(async () =>
            await AzureDataLake.DeleteContainer(
                new Input { ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fake;AccountKey=ZmFrZWtleQ==;EndpointSuffix=core.windows.net", ContainerName = "test" },
                options,
                CancellationToken.None
            )
        );
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = false,
            ErrorMessageOnFailure = string.Empty,
        };

        var result = await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fake;AccountKey=ZmFrZWtleQ==;EndpointSuffix=core.windows.net", ContainerName = "test" },
            options,
            CancellationToken.None
        );

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = CustomErrorMessage,
        };

        var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            await AzureDataLake.DeleteContainer(
                new Input { ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fake;AccountKey=ZmFrZWtleQ==;EndpointSuffix=core.windows.net", ContainerName = "test" },
                options,
                CancellationToken.None
            )
        );

        Assert.IsTrue(ex.Message.Contains(CustomErrorMessage));
    }
}
