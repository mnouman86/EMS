using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Auth.Queries.GetToken
{
    /// <summary>
    /// Response shape must match requested JSON keys:
    /// { "token_type": "bearer", "access_token": "...", "expires_in": 20 }
    /// </summary>
    public class AuthTokenResponse
    {
        // Intentionally snake_case to match client expectation
        public string token_type { get; set; } = "bearer";
        public string access_token { get; set; } = string.Empty;
        public int expires_in { get; set; }
    }
}
