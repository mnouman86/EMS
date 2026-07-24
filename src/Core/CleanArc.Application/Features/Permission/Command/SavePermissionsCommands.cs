using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Permission;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Permission.Command
{
    /* Assign a permission matrix to one OR many users (bulk). Used for single-user
       save, multi-user bulk assign, copy-from-user and copy-from-role (the SPA fetches
       the source matrix then posts it to the target users). */
    public record SaveUserPermissionsCommand(List<int> UserIds, List<FeaturePermissionInput> Permissions)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SaveUserPermissionsCommand>
    {
        [JsonIgnore] public int UserId { get; set; }   // the admin performing the change

        public IValidator<SaveUserPermissionsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SaveUserPermissionsCommand> v)
        {
            v.RuleFor(c => c.UserIds).NotNull().NotEmpty().WithMessage("Select at least one user.");
            v.RuleFor(c => c.Permissions).NotNull().WithMessage("Permissions payload is required.");
            return v;
        }
    }

    internal class SaveUserPermissionsCommandHandler : IRequestHandler<SaveUserPermissionsCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        private readonly IUserPermissionProvider _provider;
        public SaveUserPermissionsCommandHandler(IUnitOfWork u, IUserPermissionProvider provider) { _u = u; _provider = provider; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(SaveUserPermissionsCommand r, CancellationToken ct)
        {
            var json = JsonSerializer.Serialize(r.Permissions ?? new List<FeaturePermissionInput>());
            ResponseEntity last = null;
            foreach (var targetUserId in r.UserIds)
            {
                last = await _u.PermissionRepository.SaveUserFeaturePermissionsAsync(targetUserId, r.UserId, json);
                if (last != null && last.Code != 200)
                    return OperationResult<ResponseEntity>.FailureResult(last.Message, last.Code);
                _provider.Invalidate(targetUserId);   // drop cached permission set so changes apply immediately
            }
            return OperationResult<ResponseEntity>.SuccessResult(
                last ?? new ResponseEntity { IsSuccess = true, Code = 200, Message = "Permissions saved" });
        }
    }

    /* Assign / update the role-level permission template. Any user later created
       under this role gets these permissions copied over (see CreateAppUserCommand).
       Existing users of the role are NOT retroactively updated — their per-user
       grants are edited via the User Permissions screen. */
    public record SaveRolePermissionsCommand(int RoleId, List<FeaturePermissionInput> Permissions)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SaveRolePermissionsCommand>
    {
        [JsonIgnore] public int UserId { get; set; }   // the admin performing the change

        public IValidator<SaveRolePermissionsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SaveRolePermissionsCommand> v)
        {
            v.RuleFor(c => c.RoleId).GreaterThan(0);
            v.RuleFor(c => c.Permissions).NotNull().WithMessage("Permissions payload is required.");
            return v;
        }
    }

    internal class SaveRolePermissionsCommandHandler : IRequestHandler<SaveRolePermissionsCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public SaveRolePermissionsCommandHandler(IUnitOfWork u) { _u = u; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(SaveRolePermissionsCommand r, CancellationToken ct)
        {
            var json = JsonSerializer.Serialize(r.Permissions ?? new List<FeaturePermissionInput>());
            var res = await _u.PermissionRepository.SaveRoleFeaturePermissionsAsync(r.RoleId, r.UserId, json);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
