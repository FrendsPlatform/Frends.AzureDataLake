namespace Frends.AzureDataLake.ListFiles.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Options class usually contains parameters that are required.
/// </summary>
public class Options
{
    /// <summary>
    /// If true, list files and directories with files in those subdirectories such as directoryname/file.txt. 
    /// If false, list only top-level files and directories.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool Recursive { get; set; }

    /// <summary>
    /// Specify a dictionary name to return files and dictionaries within directory. 
    /// </summary>
    /// <example>test</example>
    [UIHint(nameof(Recursive), "", false)]
    public string DictionaryName { get; set; }
}