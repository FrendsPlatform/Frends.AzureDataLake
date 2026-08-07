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
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    public Exception AdditionalInfo { get; set; }
}
