using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.PaymentCommands
{
    /* FEE-03: Record payment (multi-invoice, optional advance surplus) */
    public record PaymentAllocationInput(int InvoiceId, decimal Amount);

    public record RecordPaymentCommand(
        int StudentId, DateTime PaymentDate, decimal Amount,
        string? PaymentMode, string? ReferenceNo,
        List<PaymentAllocationInput>? Allocations, string? Remarks, int CollectingStaffId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RecordPaymentCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RecordPaymentCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RecordPaymentCommand> v)
        {
            v.RuleFor(c => c.StudentId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThan(0);
            v.RuleFor(c => c.PaymentMode).NotEmpty()
                .Must(x => x == null || new[] { "Cash", "Cheque", "BankTransfer", "Online" }.Contains(x))
                .WithMessage("PaymentMode must be Cash / Cheque / BankTransfer / Online");
            return v;
        }
    }

    internal class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RecordPaymentCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RecordPaymentCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var json = JsonSerializer.Serialize(r.Allocations ?? new List<PaymentAllocationInput>());
            var res = await _u.FeeRepository.RecordPaymentAsync(new RecordPaymentDTO
            {
                StudentId = r.StudentId,
                PaymentDate = r.PaymentDate,
                Amount = r.Amount,
                PaymentMode = r.PaymentMode,
                ReferenceNo = r.ReferenceNo,
                AllocationsJson = json,
                Remarks = r.Remarks,
                CollectingStaffId = r.CollectingStaffId,
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-03 follow-up: cheque clearance */
    public record ClearChequeCommand(int PaymentId, DateTime ClearanceDate, bool Bounced)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ClearChequeCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<ClearChequeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ClearChequeCommand> v)
        {
            v.RuleFor(c => c.PaymentId).GreaterThan(0);
            return v;
        }
    }

    internal class ClearChequeCommandHandler : IRequestHandler<ClearChequeCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public ClearChequeCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ClearChequeCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.ClearChequeAsync(new ClearChequeDTO
            {
                PaymentId = r.PaymentId, ClearanceDate = r.ClearanceDate, Bounced = r.Bounced, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-08: Manually apply advance balance to a specific invoice */
    public record ApplyAdvanceCommand(int StudentId, int InvoiceId, decimal Amount)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ApplyAdvanceCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<ApplyAdvanceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ApplyAdvanceCommand> v)
        {
            v.RuleFor(c => c.StudentId).GreaterThan(0);
            v.RuleFor(c => c.InvoiceId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThan(0);
            return v;
        }
    }

    internal class ApplyAdvanceCommandHandler : IRequestHandler<ApplyAdvanceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public ApplyAdvanceCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ApplyAdvanceCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.ApplyAdvanceAsync(new ApplyAdvanceDTO
            {
                StudentId = r.StudentId, InvoiceId = r.InvoiceId, Amount = r.Amount, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-11: Reverse a payment (creates counter-entry) */
    public record ReversePaymentCommand(int PaymentId, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ReversePaymentCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<ReversePaymentCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ReversePaymentCommand> v)
        {
            v.RuleFor(c => c.PaymentId).GreaterThan(0);
            v.RuleFor(c => c.Reason).NotEmpty().WithMessage("Reason required for reversal");
            return v;
        }
    }

    internal class ReversePaymentCommandHandler : IRequestHandler<ReversePaymentCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public ReversePaymentCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ReversePaymentCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.ReversePaymentAsync(new ReversePaymentDTO
            {
                PaymentId = r.PaymentId, Reason = r.Reason, ActorUserId = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
