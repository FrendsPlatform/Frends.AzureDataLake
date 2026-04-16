namespace Frends.AzureDataLake.UploadFiles.Definitions;

#pragma warning disable CS1591 // self explanatory
public enum AuthenticationMethod
{
    ConnectionString,
    OAuth2
}

public enum HandleExistingFile
{
    Error,
    Overwrite,
}
#pragma warning restore CS1591 // self explanatory
