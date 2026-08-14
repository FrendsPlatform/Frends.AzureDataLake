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
    /// <example>{ SourceDirectory = "c:\temp", SourceFilePattern = "*.csv" }</example>
    public Source Source { get; set; }

    /// <summary>
    /// Specify destination Data Lake parameters
    /// </summary>
    /// <example>{ ConnectionMethod = ConnectionMethod.ConnectionString, ContainerName = "examplecontainer", DestinationFolderName = "uploads" }</example>
    public Destination Destination { get; set; }

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

    /// <summary>
    /// Specifies whether the uploaded file should be finalized (closed) after upload.
    /// When true, it triggers a "final update" event in Azure Storage Events.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool Close { get; set; } = true;
}
