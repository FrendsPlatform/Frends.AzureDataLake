namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Source Data Lake parameters.
    /// </summary>
    public Source Source { get; set; }

    /// <summary>
    /// Destination location parameters.
    /// </summary>
    public Destination Destination { get; set; }
}
