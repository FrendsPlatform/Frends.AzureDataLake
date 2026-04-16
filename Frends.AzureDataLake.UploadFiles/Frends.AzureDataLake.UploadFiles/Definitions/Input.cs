using System.ComponentModel;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Upload all files from the given directory.
    /// </summary>
    /// <example>c:\temp</example>
    public string SourceDirectory { get; set; }

    /// <summary>
    /// The search string used to match against the names of files in the path.
    /// Supports wildcard characters (* and ?), but not regular expressions.
    /// </summary>
    /// <example>*.*, Search*.*, *.xml, foobar.txt</example>
    public string FilePattern { get; set; }

    /// <summary>
    /// Name of the Azure Data Lake container.
    /// Task will convert all letters to lowercase.
    /// </summary>
    /// <example>examplecontainer</example>
    public string ContainerName { get; set; }

    /// <summary>
    /// Name of the target folder in the container. If left empty, files will be uploaded directly to the container.
    /// </summary>
    /// <example>ExampleDir</example>
    public string TargetDirectory { get; set; }

    /// <summary>
    /// Specifies how an existing file in the destination should be handled.
    /// </summary>
    /// <example>HandleExistingFile.Error</example>
    [DefaultValue(HandleExistingFile.Error)]
    public HandleExistingFile ActionOnExistingFile { get; set; } = HandleExistingFile.Error;

    /// <summary>
    /// True: Upload all files matching pattern, even in nested directories.
    /// False: Upload files matching pattern only directly in Source Directory.
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
