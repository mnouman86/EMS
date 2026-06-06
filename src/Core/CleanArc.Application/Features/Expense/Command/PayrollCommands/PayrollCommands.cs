using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Expense;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Expense.Command.PayrollCommands
{
    /* EXP-05 step 1: Create Draft PayrollRun + per-employee entries
       (pulls salary from EmployeeSalary + open advance balance from EmployeeAdvance) */
    public record StartPayrollRunCommand(int Month, int Year)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<StartPayrollRunCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<StartPayrollRunCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<StartPayrollRunCommand> v)
        {
            v.RuleFor(c => c.Month).InclusiveBetween(1, 12);
            v.RuleFor(c => c.Year).InclusiveBetween(2000, 2100);
            return v;
        }
    }

    internal class StartPayrollRunCommandHandler : IRequestHandler<StartPayrollRunCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public StartPayrollRunCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(StartPayrollRunCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.StartPayrollRunAsync(new StartPayrollRunDTO
            { Month = r.Month, Year = r.Year, CreatedBy = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* EXP-05 step 2: Admin tweaks a Draft entry (e.g. add a fine) */
    public record AdjustPayrollEntryCommand(int PayrollEntryId, decimal? FineDeduction, decimal? OtherDeduction, string? Notes)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<AdjustPayrollEntryCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<AdjustPayrollEntryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AdjustPayrollEntryCommand> v)
        {
            v.RuleFor(c => c.PayrollEntryId).GreaterThan(0);
            v.RuleFor(c => c.FineDeduction).GreaterThanOrEqualTo(0).When(c => c.FineDeduction.HasValue);
            v.RuleFor(c => c.OtherDeduction).GreaterThanOrEqualTo(0).When(c => c.OtherDeduction.HasValue);
            return v;
        }
    }

    internal class AdjustPayrollEntryCommandHandler : IRequestHandler<AdjustPayrollEntryCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public AdjustPayrollEntryCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(AdjustPayrollEntryCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.AdjustPayrollEntryAsync(new AdjustPayrollEntryDTO
            { PayrollEntryId = r.PayrollEntryId, FineDeduction = r.FineDeduction, OtherDeduction = r.OtherDeduction, Notes = r.Notes, UpdatedBy = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* EXP-05 step 3: Confirm disbursement — creates Expense rows, reduces advance balances, locks the run */
    public record ConfirmPayrollRunCommand(int PayrollRunId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ConfirmPayrollRunCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<ConfirmPayrollRunCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ConfirmPayrollRunCommand> v)
        { v.RuleFor(c => c.PayrollRunId).GreaterThan(0); return v; }
    }

    internal class ConfirmPayrollRunCommandHandler : IRequestHandler<ConfirmPayrollRunCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public ConfirmPayrollRunCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ConfirmPayrollRunCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.ConfirmPayrollRunAsync(new ConfirmPayrollRunDTO
            { PayrollRunId = r.PayrollRunId, ConfirmedByUserId = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
