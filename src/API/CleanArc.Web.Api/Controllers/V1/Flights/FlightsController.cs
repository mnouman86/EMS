using Asp.Versioning;
using Azure;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Admin.Commands.AddAdminCommand;
using CleanArc.Application.Features.Admin.Commands.ChangePasswordCommand;
using CleanArc.Application.Features.Admin.Commands.ForgotPasswordCommand;
using CleanArc.Application.Features.Admin.Commands.ResendVerificationEmailCommand;
using CleanArc.Application.Features.Admin.Commands.ResetPasswordCommand;
using CleanArc.Application.Features.Admin.Commands.SendOTPCommand;
using CleanArc.Application.Features.Admin.Commands.VerifyEmailCommand;
using CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand;
using CleanArc.Application.Features.Admin.Queries.GetToken;
using CleanArc.Application.Features.Flights.Queries;
using CleanArc.Application.Features.SearchAutoComplete.Queries.GetSearchAutoComplete;
using CleanArc.Application.Features.UserProfile.Commands.UpdateUserProfile;
using CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Features.Users.Queries.GetUsers;
using CleanArc.Application.Models.Request;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.BaseController;
using CleanArc.WebFramework.WebExtensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArc.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/SearchAutoComplete")]
    public class FlightsController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<FlightsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
        private readonly ILookupService _lookup;


        private string controllerName = "AdminManagerController";
        public FlightsController(ISender sender, ILogger<FlightsController> logger, IHttpContextAccessor httpContextAccessor,ILookupService lookup)
        {
            _sender = sender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _lookup = lookup;
            _lookup = lookup;
        }
        //[Authorize]
        [HttpPost("GetFlights")]
        public async Task<IActionResult> GetFlights([FromBody] GetFlightsQuery request)
        {
           // int id = UserId;
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext))
            {
                //GetSearchAutoCompleteQuery query = new GetSearchAutoCompleteQuery(request);
                var commandResult = await _sender.Send(request);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);
                //_logger.LogInformation("Executed {@actionName} action in {@controllerName} with response {@query}", actionName, controllerName, query);

                return OperationResult(commandResult);
            }

        }

        [HttpGet("airports")]
        public async Task<IActionResult> GetAirports(CancellationToken ct)
        {
            var list = await _lookup.GetAirportsAsync(ct);
            return Ok(list); // returns list of { code, name }
        }
    }
}
