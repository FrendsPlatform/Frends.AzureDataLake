namespace Frends.AzureDataLake.ListFiles.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Options class usually contains parameters that are required.
/// </summary>
public class Options
{
    /// <summary>
    /// List Azure Data Lake files in a flat listing structure or hierarchically.
    /// Hierarchical listing returns container's files and subdirectories names such as file.txt or directoryname/. 
    /// Flat listing does the same as hierarchical listing but also returns files in those subdirectories such as directoryname/file.txt.
    /// </summary>
    /// <example>ListingStructure.Flat</example>
    [DefaultValue(ListingStructure.Flat)]
    public ListingStructure ListingStructure { get; set; }

    /// <summary>
    /// Specify a dictionary name to return files and dictionaries within the specified directory. 
    /// In the case of providing the folder name, files in subfolders will not be listed (the hierarchical listing option will be applied).
    /// </summary>
    /// <example>test</example>
    public string DictionaryName { get; set; }
}