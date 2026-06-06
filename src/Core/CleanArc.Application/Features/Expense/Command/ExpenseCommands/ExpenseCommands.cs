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
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Expense.Command.ExpenseCommands
{
    /* EXP-02: Record an expense (manual entry) */
    public record RecordExpenseCommand(
        DateTime ExpenseDate, int CategoryId, string? Description, decimal Amount,
        string? PaymentMode, string? ReferenceNo, string? PaidTo,
        string? AttachmentPath, int? AcademicYearId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RecordExpenseCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RecordExpenseCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RecordExpenseCommand> v)
        {
            v.RuleFor(c => c.CategoryId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThan(0);
            return v;
        }
    }

    internal class RecordExpenseCommandHandler : IRequestHandler<RecordExpenseCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RecordExpenseCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RecordExpenseCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.RecordExpenseAsync(new RecordExpenseDTO
            {
                ExpenseDate = r.ExpenseDate, CategoryId = r.CategoryId,
                Description = r.Description, Amount = r.Amount,
                PaymentMode = r.PaymentMode, ReferenceNo = r.ReferenceNo, PaidTo = r.PaidTo,
                AttachmentPath = r.AttachmentPath, AcademicYearId = r.AcademicYearId,
                RecordedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* EXP-03: Delete an expense (soft default) */
    public record DeleteExpenseCommand(DeleteRequest deleteRequest)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DeleteExpenseCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<DeleteExpenseCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteExpenseCommand> v)
        { v.RuleFor(c => c.deleteRequest.SelectedIds).NotEmpty(); return v; }
    }

    internal class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public DeleteExpenseCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteExpenseCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.ExpenseRepository.DeleteExpenseAsync(r.deleteRequest, user.Id);
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
