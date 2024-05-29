namespace Frends.AzureDataLake.DownloadFiles.Definitions;

#pragma warning disable CS1591 // self explanatory
public static class Constants
{
    public enum ConnectionMethod
    {
        ConnectionString = 1,
        OAuth2 = 2
    }

    public const string FileExistsMessage = "File already exists";
}
#pragma warning restore CS1591 // self explanatory
