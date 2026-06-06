using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.EmployeeAdvanceCommands
{
    /* ----- Issue ----- */

    public record IssueEmployeeAdvanceCommand(int EmployeeId, decimal Amount, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<IssueEmployeeAdvanceCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<IssueEmployeeAdvanceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<IssueEmployeeAdvanceCommand> v)
        {
            v.RuleFor(c => c.EmployeeId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThan(0);
            return v;
        }
    }

    internal class IssueEmployeeAdvanceCommandHandler : IRequestHandler<IssueEmployeeAdvanceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public IssueEmployeeAdvanceCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(IssueEmployeeAdvanceCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.EmployeeRepository.IssueAdvanceAsync(new IssueEmployeeAdvanceDTO
            {
                EmployeeId = r.EmployeeId, Amount = r.Amount, Reason = r.Reason, IssuedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ----- Adjust (repayment or correction) ----- */

    public record AdjustEmployeeAdvanceCommand(int AdvanceId, decimal AmountAdjusted, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<AdjustEmployeeAdvanceCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<AdjustEmployeeAdvanceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AdjustEmployeeAdvanceCommand> v)
        {
            v.RuleFor(c => c.AdvanceId).GreaterThan(0);
            v.RuleFor(c => c.AmountAdjusted).NotEqual(0).WithMessage("AmountAdjusted must be non-zero (positive = repayment)");
            return v;
        }
    }

    internal class AdjustEmployeeAdvanceCommandHandler : IRequestHandler<AdjustEmployeeAdvanceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public AdjustEmployeeAdvanceCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(AdjustEmployeeAdvanceCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.EmployeeRepository.AdjustAdvanceAsync(new AdjustEmployeeAdvanceDTO
            {
                AdvanceId = r.AdvanceId, AmountAdjusted = r.AmountAdjusted, Reason = r.Reason, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
