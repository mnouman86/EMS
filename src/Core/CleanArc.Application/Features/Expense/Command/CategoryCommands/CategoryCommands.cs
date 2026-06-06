using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Expense;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Expense.Command.CategoryCommands
{
    /* EXP-01: Upsert category (supports subcategory via ParentCategoryId, optional monthly budget) */
    public record UpsertExpenseCategoryCommand(
        int? Id, string? Name, string? Code, int? ParentCategoryId, decimal? MonthlyBudget, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertExpenseCategoryCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertExpenseCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertExpenseCategoryCommand> v)
        {
            v.RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            v.RuleFor(c => c.Code).NotEmpty().MaximumLength(20);
            v.RuleFor(c => c.MonthlyBudget).GreaterThanOrEqualTo(0).When(c => c.MonthlyBudget.HasValue);
            return v;
        }
    }

    internal class UpsertExpenseCategoryCommandHandler : IRequestHandler<UpsertExpenseCategoryCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertExpenseCategoryCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertExpenseCategoryCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.UpsertCategoryAsync(new UpsertExpenseCategoryDTO
            {
                Id = r.Id, Name = r.Name, Code = r.Code, ParentCategoryId = r.ParentCategoryId,
                MonthlyBudget = r.MonthlyBudget, IsActive = r.IsActive, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* EXP-01: Delete category (soft default, hard via DeleteRequest.ForceHard) */
    public record DeleteExpenseCategoryCommand(DeleteRequest deleteRequest)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DeleteExpenseCategoryCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<DeleteExpenseCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteExpenseCategoryCommand> v)
        { v.RuleFor(c => c.deleteRequest.SelectedIds).NotEmpty(); return v; }
    }

    internal class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public DeleteExpenseCategoryCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteExpenseCategoryCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.DeleteCategoryAsync(r.deleteRequest, user.Id);
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
