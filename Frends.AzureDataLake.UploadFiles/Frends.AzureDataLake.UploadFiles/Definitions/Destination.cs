using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Destination parameters.
/// </summary>
public class Destination
{
    /// <summary>
    /// Connection method to be used to connect to Azure Data Lake.
    /// </summary>
    /// <example>ConnectionMethod.ConnectionString</example>
    [DefaultValue(ConnectionMethod.ConnectionString)]
    public ConnectionMethod ConnectionMethod { get; set; }

    /// <summary>
    /// Name of the Azure Data Lake container.
    /// Task will convert all letters to lowercase.
    /// </summary>
    /// <example>examplecontainer</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string ContainerName { get; set; }

    /// <summary>
    /// Connection string for Azure Data Lake.
    /// </summary>
    /// <example>DefaultEndpointsProtocol=https;AccountName=accountname;AccountKey=Pdlrxyz==;EndpointSuffix=core.windows.net</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.ConnectionString)]
    [PasswordPropertyText]
    [DisplayFormat(DataFormatString = "Text")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Name of the Azure storage account.
    /// </summary>
    /// <example>Storager</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string StorageAccountName { get; set; }

    /// <summary>
    /// Application (Client) ID of Azure AD Application.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string ApplicationID { get; set; }

    /// <summary>
    /// Tenant ID of Azure Tenant.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string TenantID { get; set; }

    /// <summary>
    /// Client Secret of Azure AD Application.
    /// </summary>
    /// <example>Password!</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    [PasswordPropertyText]
    public string ClientSecret { get; set; }
}
