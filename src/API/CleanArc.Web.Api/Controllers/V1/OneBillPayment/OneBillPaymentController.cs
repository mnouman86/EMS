using Asp.Versioning;
using Azure;
using CleanArc.Application.Features.Admin.Commands.AddAdminCommand;
using CleanArc.Application.Features.Admin.Commands.ChangePasswordCommand;
using CleanArc.Application.Features.Admin.Commands.ForgotPasswordCommand;
using CleanArc.Application.Features.Admin.Commands.ResendVerificationEmailCommand;
using CleanArc.Application.Features.Admin.Commands.ResetPasswordCommand;
using CleanArc.Application.Features.Admin.Commands.SendOTPCommand;
using CleanArc.Application.Features.Admin.Commands.VerifyEmailCommand;
using CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand;
using CleanArc.Application.Features.Admin.Queries.GetToken;
using CleanArc.Application.Features.OneBillPayment.Command.UpdateOneBillPaymentCommand;
using CleanArc.Application.Features.OneBillPayment.Queries.GetOneBillPayment;
using CleanArc.Application.Features.UserProfile.Commands.UpdateUserProfile;
using CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Features.Users.Queries.GetUsers;
using CleanArc.Application.Models.OneBillPayment;
using CleanArc.Application.Models.Request;
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
    [Route("api/v{version:apiVersion}/Payments")]
    public class OneBillPaymentController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<OneBillPaymentController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        private string controllerName = "AdminManagerController";
        public OneBillPaymentController(ISender sender, ILogger<OneBillPaymentController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _sender = sender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        //[Authorize]
        [HttpPost("LoanInquiry")]
        public async Task<IActionResult> GetOneBillPayment([FromBody] OneBillInquiryRequestDto request)
        {
           // int id = UserId;
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext))
            {
                GetOneBillPaymentQuery query = new GetOneBillPaymentQuery(request);
                var commandResult = await _sender.Send(query);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);
                //_logger.LogInformation("Executed {@actionName} action in {@controllerName} with response {@query}", actionName, controllerName, query);

                //var res = Ok(commandResult);
                return new JsonResult(commandResult) { StatusCode = 200 };
                //return Ok(commandResult);
            }

        }

        [HttpPost("LoanPayment")]
        public async Task<IActionResult> MarkLoanPayment([FromBody] OneBillPaymentRequestDto request)
        {
            var command = new UpdateOneBillPaymentCommand(request);
            var result = await _sender.Send(command);
            return new JsonResult(result);
        }
    }
}
