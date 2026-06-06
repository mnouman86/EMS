using Asp.Versioning;
using CleanArc.Application.Features.Auth.Queries.GetToken;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.BaseController;
using CleanArc.WebFramework.WebExtensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Text;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArc.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Auth")]
    public class AuthController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        private string controllerName = "AuthController";
        public AuthController(ISender sender, ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _sender = sender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Vendor token endpoint (client_credentials)
        /// Content-Type: application/x-www-form-urlencoded
        /// Body: grant_type=client_credentials&client_id=...&client_secret=...
        /// </summary>
        [HttpPost("VendorLogin")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<IActionResult> VendorLogin([FromForm] AuthTokenRequest model)
        {
            _logger.LogInformation("VendorLogin request for client_id={clientId}", model.client_id);

            var result = await _sender.Send(new AuthGetTokenQuery(
                model.grant_type,
                model.client_id,
                model.client_secret));

            //if (!result.IsSuccess)
            //{
            //    // Map common error result to proper HTTP response. Adjust as your OperationResult handles status codes.
            //    if (result.StatusCode == 404)
            //        return NotFound(new { error = result.ErrorMessage ?? "Not found" });

            //    return BadRequest(new { error = result.ErrorMessage ?? "Invalid request" });
            //}

            // Success — return the vendor token payload
            return new JsonResult(result);
        }
    }
}
