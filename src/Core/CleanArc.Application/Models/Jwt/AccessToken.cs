using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArc.Application.Models.Jwt;

public class AccessToken
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public int userID { get; set; }
    public int roleID { get; set; }

    // The access token is an encrypted JWE, so the client cannot read its claims.
    // These are surfaced here (unencrypted) so the SPA can resolve identity & roles.
    public string userName { get; set; }
    public string email { get; set; }
    public List<string> roles { get; set; } = new();

    // Set from usr.Users.MustChangePassword by the login handler — the SPA uses
    // it to force the user into /change-password before touching anything else.
    public bool mustChangePassword { get; set; }

    public AccessToken(JwtSecurityToken securityToken,string refreshToken="",int loginuserID=0,int loginRoleID=0)
    {
        access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        token_type = "Bearer";
        expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        refresh_token = refreshToken;
        userID = loginuserID;
		roleID = loginRoleID;
    }
}