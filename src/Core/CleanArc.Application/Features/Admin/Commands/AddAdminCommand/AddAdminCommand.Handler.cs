using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.AddAdminCommand
{
    internal class AddAdminCommandHandler:IRequestHandler<AddAdminCommand,OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IRoleManagerService _roleManagerService;

        public AddAdminCommandHandler(IAppUserManager userManager, IRoleManagerService roleManagerService)
        {
            _userManager = userManager;
            _roleManagerService = roleManagerService;
        }

        public async ValueTask<OperationResult<bool>> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManagerService.GetRoleByIdAsync(request.RoleId);

            if(role is null)
                return OperationResult<bool>.NotFoundResult("Specified role not found");

            var newAdmin = new User { UserName = request.UserName, Email = request.Email,RoleId=request.RoleId };

            var adminCreateResult =
                await _userManager.CreateUserWithPasswordAsync(
                    newAdmin, request.Password);

            if(!adminCreateResult.Succeeded)
                return OperationResult<bool>.SuccessResult(true, 200, adminCreateResult.Errors.StringifyIdentityResultErrors());

            var addAdminToRoleResult = await _userManager.AddUserToRoleAsync(newAdmin, role);

            if(addAdminToRoleResult.Succeeded)
                return OperationResult<bool>.SuccessResult(true,200,"User Created Successfully");

            return OperationResult<bool>.SuccessResult(true, 200, addAdminToRoleResult.Errors.StringifyIdentityResultErrors());
        }
    }
}
