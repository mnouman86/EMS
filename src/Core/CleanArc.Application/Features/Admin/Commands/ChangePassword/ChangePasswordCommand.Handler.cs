using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.KBDescription;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ChangePasswordCommand
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;

        public ChangePasswordCommandHandler(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public async ValueTask<OperationResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<bool>.FailureResult(ErrorMessages.GetMessage(ErrorCodes.UserNotFound), errorCode: ErrorCodes.UserNotFound);


            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if(result.Succeeded)
            {
                return OperationResult<bool>.SuccessResult(true, 200, SuccessMessages.GetMessage(SuccessCodes.PasswordChanged), 0, SuccessCodes.PasswordChanged);
            }
            else
            {
                var errorCode = result.Errors.FirstOrDefault()?.Code;
                var description = result.Errors.FirstOrDefault()?.Description;
                return OperationResult<bool>.FailureResult(description, errorCode: errorCode);
            }
               
        }
    }
}
