namespace Frends.AzureDataLake.DeleteDirectory.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Returns true if directory has been deleted.
    /// </summary>
    /// <example>true</example>
    public bool DirectoryWasDeleted { get; set; }

    /// <summary>
    /// Description about action's result.
    /// </summary>
    /// <example>Directory deleted successfully.</example>
    public string Message { get; set; }

    /// <summary>
    /// Error information if the operation failed and ThrowErrorOnFailure is false.
    /// </summary>
    public Error Error { get; set; }
}
