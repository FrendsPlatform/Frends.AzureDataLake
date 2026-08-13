using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception on failure.
    /// False: Return Result with Success=false and Error with description of exception.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; init; } = true;

    /// <summary>
    /// Custom error message to use when ThrowErrorOnFailure is true or when returning an error result.
    /// If empty, the original exception message is used.
    /// </summary>
    /// <example></example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; init; } = string.Empty;
}
