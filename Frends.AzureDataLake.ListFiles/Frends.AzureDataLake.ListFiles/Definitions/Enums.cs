namespace Frends.AzureDataLake.ListFiles.Definitions;

/// <summary>
/// Authentication options.
/// </summary>
public enum ConnectionMethod
{
    /// <summary>
    /// Authenticate with connectiong string.
    /// </summary>
    ConnectionString,

    /// <summary>
    /// OAuth2.
    /// </summary>
    OAuth2
}

/// <summary>
/// Listing options.
/// </summary>
public enum ListingStructure
{
    /// <summary>
    /// Flat listing structure.
    /// </summary>
    Flat,

    /// <summary>
    /// Hierarchical listing structure.
    /// </summary>
    Hierarchical
}