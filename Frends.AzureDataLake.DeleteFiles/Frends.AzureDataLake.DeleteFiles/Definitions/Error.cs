using System;

namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Error information returned when the task fails and ThrowErrorOnFailure is false.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    /// <example>Container deletion failed.</example>
    public string Message { get; init; }

    /// <summary>
    /// The exception that caused the failure.
    /// </summary>
    public Exception AdditionalInfo { get; init; }
}
