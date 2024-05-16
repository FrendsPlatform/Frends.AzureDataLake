using System.Collections.Generic;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Operation complete.
    /// Operation is seens as completed if an ignorable error has occured and Options.ThrowErrorOnFailure is set to false.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// This object contains the source file path and the URL of the file.
    /// If an ignorable error occurs, such as when a file already exists and Options.ThrowErrorOnFailure is set to false, the URL will be replaced with the corresponding error message.age.
    /// </summary>
    /// <example>{ { c:\temp\examplefile.txt, https://storage.dfs.core.windows.net/container/examplefile.txt }, { c:\temp\examplefile2.txt, File examplefile2 already exists. } }</example>
    public Dictionary<string, string> Data { get; private set; }

    /// <summary>
    /// This object contains the error message if task fail.
    /// </summary>
    /// <example>Container ex does not exist</example>
    public string ErrorMessage { get; private set; }

    internal Result(bool success, Dictionary<string, string> data, string errorMessage = "")
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }
}
