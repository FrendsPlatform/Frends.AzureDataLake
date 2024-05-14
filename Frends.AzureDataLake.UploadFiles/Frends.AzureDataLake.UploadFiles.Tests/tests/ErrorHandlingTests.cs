namespace Frends.AzureDataLake.UploadFiles.Tests.tests;

[TestClass]
public class ErrorHandlingTests : TestsBase
{
    [TestMethod]
    public void ReturnsMessageWhenErrorOccures() { }

    [TestMethod]
    public void ThrowIfFileAlreadyExists() { }

    [TestMethod]
    public void ThrowIfContainerDoesNotExist() { }

    [TestMethod]
    public void ThrowIfAuthenticationFails() { }

    [TestMethod]
    public void ThrowIfWrongConnectionString() { }

    [TestMethod]
    public void ThrowIfParameterNotValid() { }
}
