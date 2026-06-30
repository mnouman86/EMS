using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Permission.Command
{
    /* =========================================================================
       Admin-only user lifecycle commands — used by the Users Management screen.
         CreateAppUserCommand   : creates a usr.Users row + assigns one role.
         ResetUserPasswordCommand: admin sets a new password for a user.
         SetUserActiveCommand   : locks/unlocks a user (Disable / Re-enable).
       Authorization is enforced by the controller (Roles = Roles.Admin).
       ========================================================================= */

    /* ---------- Create user ---------- */
    public record CreateAppUserCommand(string Email, string FullName, string Password, int RoleId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CreateAppUserCommand>
    {
        [JsonIgnore] public int UserId { get; set; }

        public IValidator<CreateAppUserCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAppUserCommand> v)
        {
            v.RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(150);
            v.RuleFor(c => c.FullName).NotEmpty().MaximumLength(100);
            v.RuleFor(c => c.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
            v.RuleFor(c => c.RoleId).GreaterThan(0);
            return v;
        }
    }

    internal class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, OperationResult<ResponseEntity>>
    {
        private readonly IAppUserManager _users;
        private readonly IRoleManagerService _roles;
        public CreateAppUserCommandHandler(IAppUserManager users, IRoleManagerService roles)
        { _users = users; _roles = roles; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateAppUserCommand r, CancellationToken ct)
        {
            // E-mail uniqueness check — UserName == Email is our convention for staff/parent logins.
            var existing = await _users.GetUserByEmail(r.Email);
            if (existing != null)
                return OperationResult<ResponseEntity>.FailureResult("A user with this email already exists.", 409);

            var role = await _roles.GetRoleByIdAsync(r.RoleId);
            if (role == null)
                return OperationResult<ResponseEntity>.FailureResult("Role not found.", 404);

            var user = new User
            {
                UserName = r.Email.Trim(),
                Email = r.Email.Trim(),
                EmailConfirmed = true,
                Name = r.FullName.Trim(),
                FamilyName = string.Empty,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                RoleId = role.Id,
                GeneratedCode = Guid.NewGuid().ToString("N").Substring(0, 8)
            };

            var create = await _users.CreateUserWithPasswordAsync(user, r.Password);
            if (!create.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", create.Errors.Select(e => e.Description)), 400);

            var addRole = await _users.AddUserToRoleAsync(user, role);
            if (!addRole.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", addRole.Errors.Select(e => e.Description)), 500);

            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true, Code = 200, Message = "User created", RecordID = user.Id.ToString()
            });
        }
    }

    /* ---------- Reset password (admin sets a new one) ---------- */
    public record ResetUserPasswordCommand(int TargetUserId, string NewPassword)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ResetUserPasswordCommand>
    {
        [JsonIgnore] public int UserId { get; set; }

        public IValidator<ResetUserPasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ResetUserPasswordCommand> v)
        {
            v.RuleFor(c => c.TargetUserId).GreaterThan(0);
            v.RuleFor(c => c.NewPassword).NotEmpty().MinimumLength(6).MaximumLength(100);
            return v;
        }
    }

    internal class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, OperationResult<ResponseEntity>>
    {
        private readonly IAppUserManager _users;
        public ResetUserPasswordCommandHandler(IAppUserManager users) { _users = users; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(ResetUserPasswordCommand r, CancellationToken ct)
        {
            var user = await _users.GetUserByIdAsync(r.TargetUserId);
            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User not found.", 404);

            var token = await _users.GeneratePasswordResetTokenAsync(user);
            var reset = await _users.ResetPasswordAsync(user, token, r.NewPassword);
            if (!reset.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", reset.Errors.Select(e => e.Description)), 400);

            // Force re-login of any active session.
            await _users.UpdateSecurityStampAsync(user);

            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true, Code = 200, Message = "Password reset", RecordID = r.TargetUserId.ToString()
            });
        }
    }

    /* ---------- Enable / Disable (lockout toggle) ---------- */
    public record SetUserActiveCommand(int TargetUserId, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>;

    internal class SetUserActiveCommandHandler : IRequestHandler<SetUserActiveCommand, OperationResult<ResponseEntity>>
    {
        private readonly IAppUserManager _users;
        public SetUserActiveCommandHandler(IAppUserManager users) { _users = users; }

        public async ValueTask<OperationResult<ResponseEntity>> Handle(SetUserActiveCommand r, CancellationToken ct)
        {
            var user = await _users.GetUserByIdAsync(r.TargetUserId);
            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User not found.", 404);

            // Disable = lock out far-future. Enable = clear lockout.
            user.LockoutEnabled = !r.IsActive;
            user.LockoutEnd = r.IsActive ? (DateTimeOffset?)null : DateTimeOffset.UtcNow.AddYears(100);

            var upd = await _users.UpdateUserAsync(user);
            if (!upd.Succeeded)
                return OperationResult<ResponseEntity>.FailureResult(string.Join(", ", upd.Errors.Select(e => e.Description)), 500);

            // Invalidate existing sessions when disabling.
            if (!r.IsActive) await _users.UpdateSecurityStampAsync(user);

            return OperationResult<ResponseEntity>.SuccessResult(new ResponseEntity
            {
                IsSuccess = true,
                Code = 200,
                Message = r.IsActive ? "User enabled" : "User disabled",
                RecordID = r.TargetUserId.ToString()
            });
        }
    }
}
