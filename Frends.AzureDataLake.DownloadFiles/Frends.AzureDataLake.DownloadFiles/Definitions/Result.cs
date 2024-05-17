using System.Collections.Generic;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation complete.
    /// Operation is seens as completed if all desired files was downloaded.
    /// </summary>
    /// <example>true</example>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// This object contains the source file URL as a key and the path of the downloaded file as a value.
    /// If overwrite is disabled, error message will be placed as a value
    /// </summary>
    /// <example>{ {https://storage.dfs.core.windows.net/container/examplefile.txt,  c:\temp\examplefile.txt }, { https://storage.dfs.core.windows.net/container/examplefile2.txt, File examplefile2 already exists. } }</example>
    public Dictionary<string, string> DownladedFiles { get; private set; }

    /// <summary>
    /// This object contains the error message if task fails.
    /// </summary>
    /// <example>Container ex does not exist</example>
    public string ErrorMessage { get; private set; }
}
