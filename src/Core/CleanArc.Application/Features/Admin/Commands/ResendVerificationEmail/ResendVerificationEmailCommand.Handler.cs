using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ResendVerificationEmailCommand
{
    internal class ResendVerificationEmailCommandHandler:IRequestHandler<ResendVerificationEmailCommand,OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IEmailVerificationService _emailVerification;

        public ResendVerificationEmailCommandHandler(IAppUserManager userManager, IRoleManagerService roleManagerService
            , IEmailVerificationService emailVerification)
        {
            _userManager = userManager;
            _emailVerification = emailVerification;
        }

        public async ValueTask<OperationResult<bool>> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var generateAndSendCode = await _emailVerification.GenerateAndSendCodeAsync(request.Email);

            //return OperationResult<bool>.SuccessResult(true, 200, "User Created Successfully");
            if (generateAndSendCode.Succeeded)
            {
                //return OperationResult<bool>.SuccessResult(true, 200,
                //    "An email verification code has been sent to your email. " +
                //    "Please check your inbox to verify your account.");
                return OperationResult<bool>.SuccessResult(true,200 ,SuccessCodes.VerificationEmailSent);
            }
            string errorCode = generateAndSendCode.Errors.FirstOrDefault()?.Code;
            string description = generateAndSendCode.Errors.FirstOrDefault()?.Description;
            return OperationResult<bool>.FailureResult(description, errorCode: errorCode);
            //return OperationResult<bool>.FailureResult(generateAndSendCode.Errors.StringifyIdentityResultErrors());

        }
    }
}
