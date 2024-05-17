using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Destination location parameters.
/// </summary>
public class Destination
{
    /// <summary>
    /// Destination directory.
    /// </summary>
    /// <example>c:\temp</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string Directory { get; set; }

    /// <summary>
    /// Error: Throws exception if destination file exists.
    /// Rename: Adds '(1)' at the end of file name. Incerements the number if (1) already exists.
    /// Overwrite: Overwrites existing file.
    /// </summary>
    /// <example>Error</example>
    [DefaultValue(FileExistsAction.Error)]
    public FileExistsAction HandleExistingFile { get; set; }
}
