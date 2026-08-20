using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Name of the Azure Data Lake container where the data will be downloaded from.
    /// Naming: lowercase
    /// Valid chars: alphanumeric and dash, but cannot start or end with dash.
    /// </summary>
    /// <example>test-container</example>
    [DefaultValue("test-container")]
    public string ContainerName { get; init; }

    /// <summary>
    /// Full path pattern of file(s) you want to download from source DataLake.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
    /// </summary>
    /// <example>*.txt</example>
    [DefaultValue("*.txt")]
    public string SourceFilePattern { get; init; }

    /// <summary>
    /// Destination directory on the local file system.
    /// </summary>
    /// <example>c:\temp</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string DestinationDirectory { get; init; }

    /// <summary>
    /// How the existing file will be handled.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool Overwrite { get; init; } = false;
}
