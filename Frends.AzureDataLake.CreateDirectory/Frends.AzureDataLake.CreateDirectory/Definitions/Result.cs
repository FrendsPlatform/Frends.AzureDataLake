namespace Frends.AzureDataLake.CreateDirectory.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    /// <summary>
    /// Directory created.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// URI string of newly created directory.
    /// </summary>
    /// <example>https://test.lake.core.windows.net/containername/mydirectory</example>
    public string Uri { get; private set; }

    internal Result(bool success, string uri)
    {
        Success = success;
        Uri = uri;
    }
}
