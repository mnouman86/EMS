using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.VerifyEmailCommand
{
    internal class VerifyEmailCommandHandler:IRequestHandler<VerifyEmailCommand,OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IEmailVerificationService _emailVerification;

        public VerifyEmailCommandHandler(IAppUserManager userManager, IRoleManagerService roleManagerService
            , IEmailVerificationService emailVerification)
        {
            _userManager = userManager;;
            _emailVerification = emailVerification;
        }

        public async ValueTask<OperationResult<bool>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var verificationResult = await _emailVerification.VerifyCodeAsync(request.Email, request.Code);
            if (!verificationResult.Succeeded)
            {
                //return verificationResult;
                var errorCode = verificationResult.Errors.FirstOrDefault()?.Code;
                var description=verificationResult.Errors.FirstOrDefault()?.Description;
                return OperationResult<bool>.FailureResult(description,errorCode: errorCode);
                //return OperationResult<bool>.SuccessResult(true, 200, verificationResult.Errors.StringifyIdentityResultErrors());

            }

            // If code is valid, mark user's email as confirmed
            var user = await _userManager.GetUserByEmail(request.Email);
            if (user == null)
            {
                return OperationResult<bool>.FailureResult(ErrorCodes.UserNotFound);
                //return OperationResult<bool>.SuccessResult(true, 200, "User not found");

                //return OperationResult<bool>.FailureResult("User not found");
                //return Result.Failure("User not found");
            }

            user.EmailConfirmed = true;
            var updateUser = await _userManager.UpdateUserAsync(user);

            if (!updateUser.Succeeded)
            {
                string errorCode = updateUser.Errors.FirstOrDefault().Code;
                string description = updateUser.Errors.FirstOrDefault().Description;
                return OperationResult<bool>.FailureResult(description,errorCode:errorCode);
                //return OperationResult<bool>.FailureResult(updateUser.Errors.StringifyIdentityResultErrors());
                //return OperationResult<bool>.FailureResult(updateResult.Errors.Select(e => e.Description).ToArray());
            }

            //return OperationResult<bool>.SuccessResult(true, 200,
            //            "Email verified successfully. Your account is now activated.");
            return OperationResult<bool>.SuccessResult(true, 200, SuccessCodes.EmailVerified);
               
        }
    }
}
