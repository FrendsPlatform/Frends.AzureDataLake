using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.DeleteContainer.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Which connection method should be used for connecting to Azure Data Lake?
    /// </summary>
    /// <example>ConnectionMethod.ConnectionString</example>
    [DefaultValue(ConnectionMethod.ConnectionString)]
    public ConnectionMethod ConnectionMethod { get; set; }

    /// <summary>
    /// Name of the Azure Data Lake container which will be deleted.
    /// </summary>
    /// <example>test-container</example>
    public string ContainerName { get; set; }

    /// <summary>
    /// Connection string to Azure Data Lake.
    /// </summary>
    /// <example>DefaultEndpointsProtocol=https;AccountName=accountname;AccountKey=Pdlrxyz==;EndpointSuffix=core.windows.net</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.ConnectionString)]
    [PasswordPropertyText]
    public string ConnectionString { get; set; }

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
    /// <example>YouShallNotPass!</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    [PasswordPropertyText]
    public string ClientSecret { get; set; }

    /// <summary>
    /// Name of the storage account.
    /// </summary>
    /// <example>Storager</example>
    [UIHint(nameof(ConnectionMethod), "", ConnectionMethod.OAuth2)]
    public string StorageAccountName { get; set; }
}
