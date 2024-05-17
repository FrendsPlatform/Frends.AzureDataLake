using System.ComponentModel;

namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception.
    /// False: Return Result with IsSucces=false and ErrorMessage with description of exception.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; init; }
}
