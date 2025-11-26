using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Connection;

/// <summary>
/// https://www.youtube.com/watch?v=lnFvuanTkdc&ab_channel=MeetKamalToday-CloudMastery
/// </summary>
public class AzureSqlAuthenticationProvider : SqlAuthenticationProvider
{
    //Once we define or need any scope for sql server, need to fill it also here: https://database.windows.net//.default
    private static readonly string[] _azureSqlDatabaseScope = [""];

    private static readonly TokenCredential _tokenCredential = new DefaultAzureCredential();

    public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
    {
        var tokenRequestContext = new TokenRequestContext(_azureSqlDatabaseScope);
        var tokenResult = await _tokenCredential.GetTokenAsync(tokenRequestContext, default);
        return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
    }

    public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) => authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow);
}
