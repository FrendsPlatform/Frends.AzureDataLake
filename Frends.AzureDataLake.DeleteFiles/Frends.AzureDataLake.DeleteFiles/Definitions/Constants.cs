namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Constants used by the Frends.AzureDataLake.DeleteFiles task.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Connection method used to connect to Azure Data Lake.
    /// </summary>
    public enum ConnectionMethod
    {
        /// <summary>Connection string authentication.</summary>
        ConnectionString = 1,
        /// <summary>OAuth2 client credentials authentication.</summary>
        OAuth2 = 2
    }

    /// <summary>
    /// Message returned when no files match the delete pattern.
    /// </summary>
    public const string FileMissingMessage = "Nothing to delete";
}

