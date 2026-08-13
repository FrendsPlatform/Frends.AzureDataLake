using System.Collections.Generic;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation complete.
    /// Operation is seen as completed if all desired files were downloaded.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; init; } = false;

    /// <summary>
    /// This object contains the source file URL as a key and the path of the downloaded file as a value.
    /// If overwrite is disabled, error message will be placed as a value.
    /// </summary>
    /// <example>{ {https://storage.blob.core.windows.net/container/examplefile.txt,  c:\temp\examplefile.txt }, { https://storage.blob.core.windows.net/container/examplefile2.txt, File examplefile2 already exists. } }</example>
    public Dictionary<string, string> DownladedFiles { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Error information when Success is false and ThrowErrorOnFailure is false.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; init; }
}
