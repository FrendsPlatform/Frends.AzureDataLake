using System.ComponentModel;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception when an error occurs.
    /// False: Return an error result instead of throwing.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Custom error message returned in Result.Error.Message when ThrowErrorOnFailure is false.
    /// If left empty, the original exception message is used.
    /// </summary>
    /// <example>Upload failed.</example>
    public string ErrorMessageOnFailure { get; set; }
}
