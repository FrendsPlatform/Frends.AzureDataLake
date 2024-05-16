namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Specify source files parameters
    /// </summary>
    public Source Source { get; set; }

    /// <summary>
    /// Specify destination Data Lake parameters
    /// </summary>
    public Destination Destination { get; set; }

    /// <summary>
    /// Specify options how we should handle data and errors
    /// </summary>
    public Options Options { get; set; }
}
