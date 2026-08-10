namespace Frends.AzureDataLake.CreateContainer.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Container created successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; init; }

    /// <summary>
    /// URI string of newly created container.
    /// </summary>
    /// <example>https://test.lake.core.windows.net/test8f237ae0-ad33-b4b3-48d9-23b20a14c909</example>
    public string Uri { get; init; }

    /// <summary>
    /// Error information when Success is false and ThrowErrorOnFailure is false.
    /// </summary>
    public Error Error { get; init; }
}