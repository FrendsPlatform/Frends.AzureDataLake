using System.Collections.Generic;

namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation completed successfully.
    /// Operation is seen as completed if all desired files were deleted.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; init; } = false;

    /// <summary>
    /// This object contains list of deleted files.
    /// </summary>
    /// <example>
    /// {
    ///     https://storage.blob.core.windows.net/container/examplefile.txt,
    ///     https://storage.blob.core.windows.net/container/examplefile2.txt,
    /// }
    /// </example>
    public List<string> DeletedFiles { get; init; } = new List<string>();

    /// <summary>
    /// Error information when Success is false and ThrowErrorOnFailure is false.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; init; }
}

