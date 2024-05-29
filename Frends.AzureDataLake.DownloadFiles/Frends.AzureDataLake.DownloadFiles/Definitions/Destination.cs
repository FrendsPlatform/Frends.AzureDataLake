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
    public string Directory { get; init; }

    /// <summary>
    /// How the existing file will be handled.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool Overwrite { get; init; } = false;
}
