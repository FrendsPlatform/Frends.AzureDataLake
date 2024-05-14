using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Source parameters.
/// </summary>
public class Source
{
    /// <summary>
    /// Upload all files from the given directory.
    /// </summary>
    /// <example>c:\temp</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string SourceDirectory { get; set; }

    /// <summary>
    /// The search string is used to match against the names of files in the path.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions
    /// </summary>
    /// <example>*.*, Search*.*, *.xml, foobar.txt</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string SourceFilePattern { get; set; }

    /// <summary>
    /// Name of the blob folder. If left empty, file will be uploaded directly to container.
    /// </summary>
    /// <example>ExampleDir</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string DestinationFolderName { get; set; }
}