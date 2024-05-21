using System;
using System.Collections.Generic;

namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation complete.
    /// Operation is seens as completed if all desired files were deleted.
    /// </summary>
    /// <example>true</example>
    public bool IsSuccess { get; init; } = false;

    /// <summary>
    /// This object contains the source file URL as a key and the path of the downloaded file as a value.
    /// If overwrite is disabled, error message will be placed as a value
    /// </summary>
    /// <example>
    /// {
    ///     https://storage.blob.core.windows.net/container/examplefile.txt,
    ///     https://storage.blob.core.windows.net/container/examplefile2.txt,
    /// }
    /// </example>
    public List<string> DeletedFiles { get; init; } = new List<string>();

    /// <summary>
    /// This object contains the error message if task fails.
    /// </summary>
    /// <example>Container ex does not exist</example>
    public string ErrorMessage { get; init; } = string.Empty;
}
