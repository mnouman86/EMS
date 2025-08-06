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
using CleanArc.Application.Features.UserProfile.Commands.UpdateUserProfile;
using CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Features.Users.Queries.GetUsers;
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
    [Route("api/v{version:apiVersion}/UserProfile")]
    public class UserProfileController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<UserProfileController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        private string controllerName = "AdminManagerController";
        public UserProfileController(ISender sender, ILogger<UserProfileController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _sender = sender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            int id = UserId;
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext))
            {
                GetUserProfileQuery query = new GetUserProfileQuery();
                query.UserId = UserId;
                var commandResult = await _sender.Send(query);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);
                //_logger.LogInformation("Executed {@actionName} action in {@controllerName} with response {@query}", actionName, controllerName, query);

                return base.OperationResult(commandResult);
            }

        }

        // [Authorize(Roles = "admin")]
        [Authorize]
        [HttpPost("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand model)
        {
            model.UserId = UserId;
            //string actionName = "AddNewAdmin";
            //_logger.LogInformation($"Executing {@actionName} action in {@controllerName} with request model {@model}", actionName, controllerName, model);
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, model))
            {
                var commandResult = await _sender.Send(model);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
        }

    }
}
