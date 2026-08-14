using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteContainer.Definitions;
using NUnit.Framework;
using NAssert = NUnit.Framework.Assert;

namespace Frends.AzureDataLake.DeleteContainer.Tests;

[TestFixture]
public class ErrorHandlerTests
{
    private const string CustomErrorMessage = "CustomErrorMessage";
    private const string FakeConnectionString =
        "DefaultEndpointsProtocol=https;AccountName=fake;AccountKey=ZmFrZWtleQ==;EndpointSuffix=core.windows.net";

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = string.Empty,
        };

        NAssert.ThrowsAsync<Exception>(async () =>
            await AzureDataLake.DeleteContainer(
                new Input { ConnectionString = FakeConnectionString, ContainerName = "test" },
                options,
                CancellationToken.None
            )
        );
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = false,
            ErrorMessageOnFailure = string.Empty,
        };

        var result = await AzureDataLake.DeleteContainer(
            new Input { ConnectionString = FakeConnectionString, ContainerName = "test" },
            options,
            CancellationToken.None
        );

        NAssert.That(result.Success, Is.False);
        NAssert.That(result.Error, Is.Not.Null);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = CustomErrorMessage,
        };

        var ex = NAssert.ThrowsAsync<Exception>(async () =>
            await AzureDataLake.DeleteContainer(
                new Input { ConnectionString = FakeConnectionString, ContainerName = "test" },
                options,
                CancellationToken.None
            )
        );

        NAssert.That(ex, Is.Not.Null);
        NAssert.That(ex.Message, Does.Contain(CustomErrorMessage));
    }
}
