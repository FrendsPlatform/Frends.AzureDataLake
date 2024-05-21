using System.Threading;
using System.Threading.Tasks;
using Frends.AzureDataLake.DeleteFiles.Definitions;

namespace Frends.AzureDataLake.DeleteFiles.Tests.Tests;

[TestClass]
public class DeletingFilesTests : TestsBase
{
    [TestMethod]
    public async Task DeleteSingleFile()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task DeleteMultipleFiles()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input(),
            new Options(),
            CancellationToken.None
        );
    }

    [TestMethod]
    public async Task DeleteNothingIfPatternFileNotFound()
    {
        var result = await AzureDataLake.DeleteFiles(
            new Input(),
            new Options(),
            CancellationToken.None
        );
    }
}
