using System.ComponentModel;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Optional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// True: Throw an exception.
    /// False: If the error is ignorable, such as when a File already exists, the error will be added to the Result.ErrorMessages list instead of stopping the Task.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// How the existing file will be handled.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool Overwrite { get; set; } = false;

    /// <summary>
    /// True: Upload all files matching pattern, even in nested directories.
    /// False: Upload files matching pattern, which are only directly in Source Directory
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool UploadFilesRecursively { get; set; } = true;
}
