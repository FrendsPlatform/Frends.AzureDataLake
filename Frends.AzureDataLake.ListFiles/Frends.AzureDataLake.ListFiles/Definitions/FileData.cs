namespace Frends.AzureDataLake.ListFiles.Definitions;

using System;

/// <summary>
/// Represents a file or directory in Azure Data Lake.
/// </summary>
public class FileData
{

    /// <summary>
    /// Indicates whether the item is a directory. Returns true if the item is a directory, otherwise false
    /// </summary>
    /// <example>true</example>
    public bool IsDirectory { get; set; }

    /// <summary>
    /// Azure Data Lake file name.
    /// </summary>
    /// <example>file.txt, directory/file.txt</example>
    public string Name { get; set; }

    /// <summary>
    /// Azure Data Lake file URL.
    /// </summary>
    /// <example>https://storage.dfs.core.windows.net/containername/file.txt</example>
    public string URL { get; set; }
}
