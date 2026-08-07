namespace Frends.AzureDataLake.DeleteDirectory.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception on failure.
    /// False: Return Result with Success=false and Error with description of exception.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Custom error message to include when an error occurs.
    /// Leave empty to use the original exception message.
    /// </summary>
    /// <example>Custom error message here</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
