using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Settings;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using System.Web;

namespace CleanArc.Application.Features.Admin.Commands.ForgotPasswordCommand
{
    internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IEmailVerificationService _emailVerification;


        public ForgotPasswordCommandHandler(IAppUserManager userManager, IEmailVerificationService emailVerification)
        {
            _userManager = userManager;
            _emailVerification = emailVerification;
        }

        public async ValueTask<OperationResult<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            
            // If code is valid, mark user's email as confirmed
            var user = await _userManager.GetUserByEmail(request.Email);
            if (user == null)
            {
                return OperationResult<bool>.FailureResult(ErrorCodes.EmailNotFound);
                //return OperationResult<bool>.SuccessResult(true, 200, "User not found");

                //return OperationResult<bool>.FailureResult("User not found");
                //return Result.Failure("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLinkResult = await _emailVerification.GeneratePasswordResetLinkAsync(request.Email, token);

            if (!resetLinkResult.Succeeded)
            {
                string errorCode = resetLinkResult.Errors.FirstOrDefault().Code;
                string description = resetLinkResult.Errors.FirstOrDefault().Description;
                return OperationResult<bool>.FailureResult(description,errorCode:errorCode);
                //return OperationResult<bool>.FailureResult(updateUser.Errors.StringifyIdentityResultErrors());
                //return OperationResult<bool>.FailureResult(updateResult.Errors.Select(e => e.Description).ToArray());
            }

            //return OperationResult<bool>.SuccessResult(true, 200,
            //            "Email verified successfully. Your account is now activated.");
            return OperationResult<bool>.SuccessResult(true, 200, SuccessMessages.GetMessage(SuccessCodes.ResetPasswordEmailSent), 0, SuccessCodes.ResetPasswordEmailSent);
               
        }
    }
}
