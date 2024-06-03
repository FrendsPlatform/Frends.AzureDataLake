using System.Collections.Generic;

namespace Frends.AzureDataLake.ListFiles.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    /// <summary>
    /// List of Azure Data Lake files.
    /// </summary>
    /// <example>[ { Name = file.txt, URL = https://storage.dfs.core.windows.net/containername/file.txt} ]</example>
    public List<FileData> FileList { get; private set; }

    internal Result(List<FileData> fileList)
    {
        FileList = fileList;
    }
}
