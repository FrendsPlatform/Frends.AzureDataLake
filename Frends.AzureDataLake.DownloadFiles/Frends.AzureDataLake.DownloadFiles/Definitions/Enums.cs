namespace Frends.AzureDataLake.DownloadFiles.Definitions;

#pragma warning disable CS1591 // self explanatory
public enum ConnectionMethod
{
    ConnectionString,
    OAuth2
}

public enum FileExistsAction
{
    Error,
    Rename,
    Overwrite
}
#pragma warning restore CS1591 // self explanatory
