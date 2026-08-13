namespace Frends.AzureDataLake.DownloadFiles.Definitions;

/// <summary>
/// Constants used by Frends.AzureDataLake.DownloadFiles.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Connection method used to connect to Azure Data Lake.
    /// </summary>
    public enum ConnectionMethod
    {
        /// <summary>Use a connection string to authenticate.</summary>
        ConnectionString = 1,
        /// <summary>Use OAuth2 client credentials to authenticate.</summary>
        OAuth2 = 2
    }

    /// <summary>
    /// Message returned when a file already exists and overwrite is disabled.
    /// </summary>
    public const string FileExistsMessage = "File already exists";
}
