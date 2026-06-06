using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Expense;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Expense.Command.RecurringCommands
{
    /* EXP-02: Upsert recurring template (rent, internet, etc.) */
    public record UpsertRecurringTemplateCommand(
        int? Id, string? Name, int CategoryId, decimal Amount,
        string? DefaultPaymentMode, string? PaidTo, string? Description, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertRecurringTemplateCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertRecurringTemplateCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertRecurringTemplateCommand> v)
        {
            v.RuleFor(c => c.Name).NotEmpty();
            v.RuleFor(c => c.CategoryId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThan(0);
            return v;
        }
    }

    internal class UpsertRecurringTemplateCommandHandler : IRequestHandler<UpsertRecurringTemplateCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertRecurringTemplateCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertRecurringTemplateCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.UpsertRecurringTemplateAsync(new UpsertRecurringTemplateDTO
            {
                Id = r.Id, Name = r.Name, CategoryId = r.CategoryId, Amount = r.Amount,
                DefaultPaymentMode = r.DefaultPaymentMode, PaidTo = r.PaidTo,
                Description = r.Description, IsActive = r.IsActive, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* EXP-02: One-click generate this month's expenses from all active templates */
    public record GenerateRecurringExpensesCommand(DateTime ExpenseDate, int? AcademicYearId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<GenerateRecurringExpensesCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<GenerateRecurringExpensesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GenerateRecurringExpensesCommand> v)
        { return v; }
    }

    internal class GenerateRecurringExpensesCommandHandler : IRequestHandler<GenerateRecurringExpensesCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public GenerateRecurringExpensesCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(GenerateRecurringExpensesCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.GenerateRecurringExpensesAsync(new GenerateRecurringExpensesDTO
            {
                ExpenseDate = r.ExpenseDate, AcademicYearId = r.AcademicYearId, RecordedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
