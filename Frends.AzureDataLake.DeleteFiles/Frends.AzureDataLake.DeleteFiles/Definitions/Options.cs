using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception.
    /// False: Return Result with Success=false and Error with description of exception.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Custom error message to use when ThrowErrorOnFailure is true or when returning an error result.
    /// If empty, the original exception message is used.
    /// </summary>
    /// <example>Custom error message</example> 
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
