namespace Frends.AzureDataLake.ListFiles.Definitions;

using System;

public class FileData
{

    /// <summary>
    /// The type of the item (either "File" or "Directory").
    /// </summary>
    /// <example>File</example>
    public string Type { get; set; }

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
