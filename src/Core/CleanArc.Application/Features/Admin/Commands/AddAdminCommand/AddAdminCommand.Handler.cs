using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Settings;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CleanArc.Application.Features.Admin.Commands.AddAdminCommand
{
    internal class AddAdminCommandHandler:IRequestHandler<AddAdminCommand,OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IRoleManagerService _roleManagerService;
        private readonly IEmailVerificationService _emailVerification;
        private readonly EmailVerificationSettings _settings;

        public AddAdminCommandHandler(IAppUserManager userManager, IRoleManagerService roleManagerService
            , IEmailVerificationService emailVerification, IOptions<EmailVerificationSettings> settings)
        {
            _userManager = userManager;
            _roleManagerService = roleManagerService;
            _emailVerification = emailVerification;
            _settings=settings.Value;
        }

        public async ValueTask<OperationResult<bool>> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManagerService.GetRoleByIdAsync(request.RoleId);

            if (role is null)
            {
                //return OperationResult<bool>.NotFoundResult("Specified role not found");
                return OperationResult<bool>.FailureResult(statusCode:404,
                errorCode: ErrorCodes.RoleNotFound // Optional error code
            );
            }
            bool emailConfirmed = false;
            if (!_settings.Enabled)
            {
                emailConfirmed = true;
            }
            var newAdmin = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                RoleId = request.RoleId,
                EmailConfirmed = emailConfirmed
            };

            var adminCreateResult =
                await _userManager.CreateUserWithPasswordAsync(
                    newAdmin, request.Password);

            if (!adminCreateResult.Succeeded)
            {
                string errorCode = adminCreateResult.Errors.FirstOrDefault()?.Code;
                string description = adminCreateResult.Errors.FirstOrDefault()?.Description;
                return OperationResult<bool>.FailureResult(description, errorCode: errorCode);
                //return OperationResult<bool>.FailureResult(adminCreateResult.Errors.StringifyIdentityResultErrors());
            }
            //return OperationResult<bool>.SuccessResult(true, 200, adminCreateResult.Errors.StringifyIdentityResultErrors());

            var addAdminToRoleResult = await _userManager.AddUserToRoleAsync(newAdmin, role);

            if (addAdminToRoleResult.Succeeded)
            {
                if (_settings.Enabled)
                {
                    var generateAndSendCode = await _emailVerification.GenerateAndSendCodeAsync(request.Email);

                    //return OperationResult<bool>.SuccessResult(true, 200, "User Created Successfully");
                    if (generateAndSendCode.Succeeded)
                    {
                        //return OperationResult<bool>.SuccessResult(true, 200,
                        //    "User created successfully. A verification code has been sent to your email. " +
                        //    "Please check your inbox to verify your account.");
                        return OperationResult<bool>.SuccessResult(true, 200,
                            SuccessCodes.UserCreatedWithPendingVerification);
                    }
                    string errorCode = generateAndSendCode.Errors.FirstOrDefault()?.Code;
                    string description = generateAndSendCode.Errors.FirstOrDefault()?.Description;
                    return OperationResult<bool>.FailureResult(description, errorCode: errorCode);
                }
                else
                {
                    return OperationResult<bool>.SuccessResult(true, 200, "User Created Successfully");
                    
                }
                
                //return OperationResult<bool>.FailureResult(generateAndSendCode.Errors.StringifyIdentityResultErrors());

            }
            else
            {
                string errorCode = addAdminToRoleResult.Errors.FirstOrDefault()?.Code;
                string description = addAdminToRoleResult.Errors.FirstOrDefault()?.Description;
                return OperationResult<bool>.FailureResult(description, errorCode: errorCode);
            }
            //return OperationResult<bool>.FailureResult(addAdminToRoleResult.Errors.StringifyIdentityResultErrors());
        }
    }
}
