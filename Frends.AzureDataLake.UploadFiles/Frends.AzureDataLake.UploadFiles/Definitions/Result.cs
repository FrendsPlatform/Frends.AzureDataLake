using System.Collections.Generic;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Error details returned when ThrowErrorOnFailure is false and an error occurs.
/// </summary>
public class ResultError
{
    /// <summary>
    /// Error message. Contains the value of Options.ErrorMessageOnFailure if set, otherwise the original exception message.
    /// </summary>
    /// <example>Upload failed.</example>
    public string Message { get; private set; }

    /// <summary>
    /// Original exception message providing additional context about the error.
    /// </summary>
    /// <example>Container ex does not exist.</example>
    public string AdditionalInfo { get; private set; }

    internal ResultError(string message, string additionalInfo)
    {
        Message = message;
        AdditionalInfo = additionalInfo;
    }
}

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation complete.
    /// Returns false if ThrowErrorOnFailure is disabled and an error occurred.
    /// Returns true otherwise.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Dictionary mapping local source file paths to their respective URLs in Azure Data Lake.
    /// Only contains successfully uploaded files.
    /// </summary>
    /// <example>{ { "c:\temp\file.txt", "https://storage.dfs.core.windows.net/container/file.txt" } }</example>
    public Dictionary<string, string> Files { get; private set; }

    /// <summary>
    /// Error details when the task fails and ThrowErrorOnFailure is false. Null on success.
    /// </summary>
    public ResultError Error { get; private set; }

    internal Result(bool success, Dictionary<string, string> files, ResultError error = null)
    {
        Success = success;
        Files = files;
        Error = error;
    }
}
