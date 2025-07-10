using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Web;

namespace CleanArc.Application.Features.Admin.Commands.ResetPasswordCommand
{
    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;

        public ResetPasswordCommandHandler(IAppUserManager userManager, IRoleManagerService roleManagerService
            , IEmailVerificationService emailVerification)
        {
            _userManager = userManager;
        }

        public async ValueTask<OperationResult<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            
            


            // If code is valid, mark user's email as confirmed
            var user = await _userManager.GetUserByEmail(request.Email);
            if (user == null)
            {
                return OperationResult<bool>.FailureResult(ErrorCodes.UserNotFound);
            }
           string token= HttpUtility.UrlDecode(request.Token);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
            {
                string errorCode = result.Errors.FirstOrDefault().Code;
                string description = result.Errors.FirstOrDefault().Description;
                return OperationResult<bool>.FailureResult(description,errorCode:errorCode);
            }
            return OperationResult<bool>.SuccessResult(true, 200, SuccessMessages.GetMessage(SuccessCodes.PasswordChanged), 0, SuccessCodes.PasswordChanged);
               
        }
    }
}
