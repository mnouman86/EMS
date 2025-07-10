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
    [Route("api/v{version:apiVersion}/AdminManager")]
    public class AdminManagerController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<AdminManagerController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        private string controllerName = "AdminManagerController";
        public AdminManagerController(ISender sender, ILogger<AdminManagerController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _sender = sender;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> AdminLogin(AdminGetTokenQuery model)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, model))
            {
                //string actionName = "AdminLogin";

                //_logger.LogInformation("Executing {@actionName} action in {@controllerName} with request model {@model}", actionName, controllerName, model);
                var commandResult = await _sender.Send(model);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);
                //_logger.LogInformation("Executed {@actionName} action in {@controllerName} with response {@query}", actionName, controllerName, query);

                return base.OperationResult(commandResult);
            }

        }

       // [Authorize(Roles = "admin")]
        [HttpPost("NewAdmin")]
        public async Task<IActionResult> AddNewAdmin(AddAdminCommand model)
        {
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

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailCommand model)
        {
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
        [HttpPost("ResendVerificationEmail")]
        public async Task<IActionResult> ResendVerificationEmail(ResendVerificationEmailCommand model)
        {
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

        [HttpPost("LoginWith2FA")]
        public async Task<IActionResult> LoginWith2FA(SendOTPCommand command)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, command))
            {
                var commandResult = await _sender.Send(command);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
            //var result = await _mediator.Send(command);
            //return result.ToActionResult();
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(VerifyOTPCommand command)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, command))
            {
                var commandResult = await _sender.Send(command);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, command))
            {
                command.UserId=UserId;
                var commandResult = await _sender.Send(command);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, command))
            {
                var commandResult = await _sender.Send(command);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, command))
            {
                var commandResult = await _sender.Send(command);
                //_logger.LogInformation($"Executed {@actionName} action in {@controllerName} with response {@commandResult}", actionName, controllerName, commandResult);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(commandResult);

                return base.OperationResult(commandResult);
            }
        }
    }
}
