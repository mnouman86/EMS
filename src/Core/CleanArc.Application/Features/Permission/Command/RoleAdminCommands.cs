using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Identity;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Permission.Command
{
    /* =========================================================================
       Admin-only role lifecycle commands.
         CreateAppRoleCommand   : adds a new role to usr.Roles.
         UpdateAppRoleCommand   : renames an existing role.
         DeleteAppRoleCommand   : deletes a role + unhooks all assigned users.
       ========================================================================= */

    /* ---------- Create ---------- */
    public record CreateAppRoleCommand(string RoleName)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CreateAppRoleCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<CreateAppRoleCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAppRoleCommand> v)
        {
            v.RuleFor(c => c.RoleName).NotEmpty().MaximumLength(50);
            return v;
        }
    }

    internal class CreateAppRoleCommandHandler : IRequestHandler<CreateAppRoleCommand, OperationResult<ResponseEntity>>
    {
        private readonly IRoleManagerService _roles;
        public CreateAppRoleCommandHandler(IRoleManagerService roles) { _roles = roles; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateAppRoleCommand r, CancellationToken ct)
        {
            var name = r.RoleName.Trim().ToLowerInvariant();
            var result = await _roles.CreateRoleAsync(new CreateRoleDto { RoleName = name });
            if (!result.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", result.Errors.Select(e => e.Description)), 400);
            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true, Code = 200, Message = "Role created", RecordID = name
            });
        }
    }

    /* ---------- Update (rename) ---------- */
    public record UpdateAppRoleCommand(int RoleId, string NewName)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpdateAppRoleCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpdateAppRoleCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateAppRoleCommand> v)
        {
            v.RuleFor(c => c.RoleId).GreaterThan(0);
            v.RuleFor(c => c.NewName).NotEmpty().MaximumLength(50);
            return v;
        }
    }

    internal class UpdateAppRoleCommandHandler : IRequestHandler<UpdateAppRoleCommand, OperationResult<ResponseEntity>>
    {
        private readonly IRoleManagerService _roles;
        public UpdateAppRoleCommandHandler(IRoleManagerService roles) { _roles = roles; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateAppRoleCommand r, CancellationToken ct)
        {
            var result = await _roles.UpdateRoleAsync(r.RoleId, r.NewName.Trim().ToLowerInvariant());
            if (!result.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", result.Errors.Select(e => e.Description)), 400);
            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true, Code = 200, Message = "Role renamed", RecordID = r.RoleId.ToString()
            });
        }
    }

    /* ---------- Delete ---------- */
    public record DeleteAppRoleCommand(int RoleId) : IRequest<OperationResult<ResponseEntity>>;

    internal class DeleteAppRoleCommandHandler : IRequestHandler<DeleteAppRoleCommand, OperationResult<ResponseEntity>>
    {
        private readonly IRoleManagerService _roles;
        public DeleteAppRoleCommandHandler(IRoleManagerService roles) { _roles = roles; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteAppRoleCommand r, CancellationToken ct)
        {
            var role = await _roles.GetRoleByIdAsync(r.RoleId);
            if (role == null)
                return OperationResult<ResponseEntity>.FailureResult("Role not found.", 404);

            if (string.Equals(role.Name, "admin", System.StringComparison.OrdinalIgnoreCase))
                return OperationResult<ResponseEntity>.FailureResult("The 'admin' role cannot be deleted.", 400);

            var ok = await _roles.DeleteRoleAsync(r.RoleId);
            if (!ok)
                return OperationResult<ResponseEntity>.FailureResult("Could not delete the role.", 500);

            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true, Code = 200, Message = "Role deleted", RecordID = r.RoleId.ToString()
            });
        }
    }
}
