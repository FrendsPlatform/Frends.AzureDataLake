using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.CreateContainer.Definitions;
using NUnit.Framework;

namespace Frends.AzureDataLake.CreateContainer.Tests;

[TestFixture]
public class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput =>
        new() { ConnectionString = "Not valid parameter", ContainerName = "Valid name" };

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        Assert.ThrowsAsync<Exception>(() =>
            AzureDataLake.CreateContainer(InvalidInput, new Options(), CancellationToken.None));
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await AzureDataLake.CreateContainer(InvalidInput, options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.Message, Is.Not.Empty);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options
        {
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = CustomErrorMessage
        };
        var ex = Assert.ThrowsAsync<Exception>(() =>
            AzureDataLake.CreateContainer(InvalidInput, options, CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain(CustomErrorMessage));
    }
}
