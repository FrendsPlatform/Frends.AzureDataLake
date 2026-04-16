using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AzureDataLake.UploadFiles.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
    /// <summary>
    /// Authentication method used to connect to Azure Data Lake.
    /// </summary>
    /// <example>AuthenticationMethod.ConnectionString</example>
    [DefaultValue(AuthenticationMethod.ConnectionString)]
    public AuthenticationMethod AuthenticationMethod { get; set; }

    /// <summary>
    /// Connection string for Azure Data Lake.
    /// </summary>
    /// <example>DefaultEndpointsProtocol=https;AccountName=accountname;AccountKey=Pdlrxyz==;EndpointSuffix=core.windows.net</example>
    [UIHint(nameof(AuthenticationMethod), "", AuthenticationMethod.ConnectionString)]
    [PasswordPropertyText]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Name of the Azure storage account.
    /// </summary>
    /// <example>storage</example>
    [UIHint(nameof(AuthenticationMethod), "", AuthenticationMethod.OAuth2)]
    public string StorageAccountName { get; set; }

    /// <summary>
    /// Application (Client) ID of Azure AD Application.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(AuthenticationMethod), "", AuthenticationMethod.OAuth2)]
    public string ApplicationId { get; set; }

    /// <summary>
    /// Tenant ID of Azure Tenant.
    /// </summary>
    /// <example>Y6b1hf2a-80e2-xyz2-qwer3h-3a7c3a8as4b7f</example>
    [UIHint(nameof(AuthenticationMethod), "", AuthenticationMethod.OAuth2)]
    public string TenantId { get; set; }

    /// <summary>
    /// Client Secret of Azure AD Application.
    /// </summary>
    /// <example>Password!</example>
    [UIHint(nameof(AuthenticationMethod), "", AuthenticationMethod.OAuth2)]
    [PasswordPropertyText]
    public string ClientSecret { get; set; }
}
