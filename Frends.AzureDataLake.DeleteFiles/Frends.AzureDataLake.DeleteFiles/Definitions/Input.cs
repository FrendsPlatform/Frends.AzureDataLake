using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Frends.AzureDataLake.DeleteFiles.Definitions.Constants;

namespace Frends.AzureDataLake.DeleteFiles.Definitions;

/// <summary>
/// Input Data Lake parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Which connection method should be used for connecting to Azure Data Lake.
    /// </summary>
    /// <example>ConnectionMethod.ConnectionString</example>
    [DefaultValue(ConnectionMethod.ConnectionString)]
    public ConnectionMethod ConnectionMethod { get; init; } = ConnectionMethod.ConnectionString;

    /// <summary>
    /// Connection string to Azure Data Lake.
    /// </summary>
    /// <example>DefaultEndpointsProtocol=https;AccountName=accountname;AccountKey=Pdlrxyz==;EndpointSuffix=core.windows.net</example>
    [PasswordPropertyText]
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.ConnectionString)]
    public string ConnectionString { get; init; }

    /// <summary>
    /// Name of the Azure Data Lake account.
    /// </summary>
    /// <example>storager</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string StorageAccountName { get; init; }

    /// <summary>
    /// Application (Client) ID of Azure AD Application.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string ApplicationID { get; init; }

    /// <summary>
    /// Tenant ID of Azure Tenant.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string TenantID { get; init; }

    /// <summary>
    /// Client Secret of Azure AD Application.
    /// </summary>
    /// <example>Password!</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    [PasswordPropertyText]
    public string ClientSecret { get; init; }

    /// <summary>
    /// Name of the Azure Data Lake container where the data will be uploaded.
    /// Naming: lowercase
    /// Valid chars: alphanumeric and dash, but cannot start or end with dash.
    /// </summary>
    /// <example>test-container</example>
    [DefaultValue("test-container")]
    [DisplayFormat(DataFormatString = "Text")]
    public string ContainerName { get; init; }

    /// <summary>
    /// Path of file(s) you want to delete from DataLake
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
    /// </summary>
    /// <example>file?.txt</example>
    public string DeleteFilePattern { get; init; }
}
