using System.ComponentModel;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Input parameters.
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

    /// <summary>
    /// How the existing file will be handled.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool Overwrite { get; set; } = false;

    /// <summary>
    /// True: Upload all files matching pattern, even in nested directories.
    /// False: Upload files matching pattern, which are only directly in Source Directory
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool UploadFilesRecursively { get; set; } = true;
}
