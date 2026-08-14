namespace Frends.AzureDataLake.DeleteDirectory.Definitions;

using System;

/// <summary>
/// Error information returned when an operation fails and ThrowErrorOnFailure is false.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message describing what went wrong.
    /// </summary>
    /// <example>The specified resource does not exist.</example>
    public string Message { get; set; }

    /// <summary>
    /// The exception that caused the error.
    /// </summary>
    public Exception AdditionalInfo { get; set; }
}
