using System;

namespace Frends.AzureDataLake.DeleteContainer.Definitions;

/// <summary>
/// Error details.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    /// <example>Container 'my-container' does not exist.</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>System.Exception: Container 'my-container' does not exist.</example>
    public Exception AdditionalInfo { get; set; }
}
