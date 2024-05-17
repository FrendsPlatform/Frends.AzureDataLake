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
    public string SourceDirectory { get; set; }

    /// <summary>
    /// The search string is used to match against the names of files in the path.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions
    /// </summary>
    /// <example>*.*, Search*.*, *.xml, foobar.txt</example>
    public string SourceFilePattern { get; set; }
}
