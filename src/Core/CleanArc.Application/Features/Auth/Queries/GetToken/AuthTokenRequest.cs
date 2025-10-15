using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Auth.Queries.GetToken
{
    public record AuthTokenRequest(
        [property: FromForm(Name = "grant_type")] string grant_type,
        [property: FromForm(Name = "client_id")] string client_id,
        [property: FromForm(Name = "client_secret")] string client_secret);
}
