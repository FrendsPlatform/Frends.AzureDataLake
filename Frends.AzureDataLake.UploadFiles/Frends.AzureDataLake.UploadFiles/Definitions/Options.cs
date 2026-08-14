using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: File already exists is treated as an error. 
    /// Whether an exception is thrown or Result with Success=false 
    /// is returned depends on ThrowErrorOnFailure option.
    /// False: If file already exists, it will be skipped and added 
    /// to the Result dictionary instead of stopping the Task.
    /// </summary>
    [DefaultValue(true)]
    public bool FailOnFileExists { get; set; } = true;

    /// <summary>
    /// True: Throw an exception on failure.
    /// False: Return Result with Success=false and Error with description of exception.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Custom error message to use when ThrowErrorOnFailure is true or when returning an error result.
    /// If empty, the original exception message is used.
    /// </summary>
    /// <example></example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
