using System;

namespace Frends.AzureDataLake.DeleteContainer.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Returns true when container has been deleted.
    /// </summary>
    /// <example>true</example>
    public bool ContainerWasDeleted { get; private set; }

    /// <summary>
    /// Description about action's result.
    /// </summary>
    /// <example>Container deleted successfully.</example>
    public string Message { get; private set; }

    /// <summary>
    /// Error details when the operation fails and ThrowErrorOnFailure is false.
    /// </summary>
    public Error Error { get; private set; }

    internal Result(bool success, bool containerWasDeleted, string message)
    {
        Success = success;
        ContainerWasDeleted = containerWasDeleted;
        Message = message;
    }

    internal Result(bool success, Error error)
    {
        Success = success;
        Error = error;
    }
}

/// <summary>
/// Error details.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    public string Message { get; internal set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    public Exception AdditionalInfo { get; internal set; }
}
