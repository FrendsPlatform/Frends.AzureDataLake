namespace Frends.AzureDataLake.DeleteContainer.Definitions;

/// <summary>
/// Result parameters.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Returns true when container has been deleted.
    /// </summary>
    /// <example>true</example>
    public bool ContainerWasDeleted { get; set; }

    /// <summary>
    /// Description about action's result.
    /// </summary>
    /// <example>Container deleted successfully.</example>
    public string Message { get; set; }

    /// <summary>
    /// Error details when the operation fails and ThrowErrorOnFailure is false.
    /// </summary>
    public Error Error { get; set; }
}
