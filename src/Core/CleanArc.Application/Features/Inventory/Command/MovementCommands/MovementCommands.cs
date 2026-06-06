using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Inventory;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Inventory.Command.MovementCommands
{
    /* INV-02: Record purchase */
    public record PurchaseLineInput(int ItemId, decimal Quantity, decimal UnitPrice);

    public record RecordPurchaseCommand(
        DateTime PurchaseDate, string? VendorName, string? VendorInvoiceNo,
        string? PaymentMode, string? Notes, string? AttachmentPath,
        List<PurchaseLineInput> Lines)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RecordPurchaseCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RecordPurchaseCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RecordPurchaseCommand> v)
        {
            v.RuleFor(c => c.VendorName).NotEmpty();
            v.RuleFor(c => c.Lines).NotNull().NotEmpty();
            v.RuleForEach(c => c.Lines).ChildRules(l =>
            {
                l.RuleFor(x => x.ItemId).GreaterThan(0);
                l.RuleFor(x => x.Quantity).GreaterThan(0);
                l.RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            });
            return v;
        }
    }

    internal class RecordPurchaseCommandHandler : IRequestHandler<RecordPurchaseCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RecordPurchaseCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RecordPurchaseCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            // NOTE: INV-02 spec says auto-create an Expense record in EXP-03. The SP
            // currently sets LinkedExpenseId = NULL; when Module 8 (Expense) ships, the
            // SP will be extended to insert into dbo.Expense and stamp the link.
            var res = await _u.InventoryRepository.RecordPurchaseAsync(new RecordPurchaseDTO
            {
                PurchaseDate = r.PurchaseDate,
                VendorName = r.VendorName,
                VendorInvoiceNo = r.VendorInvoiceNo,
                PaymentMode = r.PaymentMode,
                Notes = r.Notes,
                AttachmentPath = r.AttachmentPath,
                LinesJson = JsonSerializer.Serialize(r.Lines),
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-03: Issue items */
    public record IssueLineInput(int ItemId, decimal Quantity);

    public record IssueInventoryItemsCommand(
        DateTime IssueDate, string? IssuedToType, int? IssuedToId, string? IssuedToName,
        string? Purpose, List<IssueLineInput> Lines)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<IssueInventoryItemsCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<IssueInventoryItemsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<IssueInventoryItemsCommand> v)
        {
            v.RuleFor(c => c.IssuedToType).NotEmpty()
                .Must(x => x == null || new[] { "Class", "Teacher", "Department", "Student" }.Contains(x))
                .WithMessage("IssuedToType must be Class / Teacher / Department / Student");
            v.RuleFor(c => c.IssuedToName).NotEmpty();
            v.RuleFor(c => c.Lines).NotNull().NotEmpty();
            v.RuleForEach(c => c.Lines).ChildRules(l =>
            {
                l.RuleFor(x => x.ItemId).GreaterThan(0);
                l.RuleFor(x => x.Quantity).GreaterThan(0);
            });
            return v;
        }
    }

    internal class IssueInventoryItemsCommandHandler : IRequestHandler<IssueInventoryItemsCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public IssueInventoryItemsCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(IssueInventoryItemsCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.IssueItemsAsync(new IssueItemsDTO
            {
                IssueDate = r.IssueDate,
                IssuedToType = r.IssuedToType,
                IssuedToId = r.IssuedToId,
                IssuedToName = r.IssuedToName,
                Purpose = r.Purpose,
                LinesJson = JsonSerializer.Serialize(r.Lines),
                IssuedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-04: Record return */
    public record ReturnLineInput(int IssueLineId, int ItemId, decimal Quantity, string Condition, string? Notes);

    public record RecordInventoryReturnCommand(int IssueId, DateTime ReturnDate, string? Notes, List<ReturnLineInput> Lines)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RecordInventoryReturnCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RecordInventoryReturnCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RecordInventoryReturnCommand> v)
        {
            v.RuleFor(c => c.IssueId).GreaterThan(0);
            v.RuleFor(c => c.Lines).NotNull().NotEmpty();
            v.RuleForEach(c => c.Lines).ChildRules(l =>
            {
                l.RuleFor(x => x.IssueLineId).GreaterThan(0);
                l.RuleFor(x => x.Quantity).GreaterThan(0);
                l.RuleFor(x => x.Condition).NotEmpty()
                    .Must(x => x == null || new[] { "Good", "Damaged", "Lost" }.Contains(x))
                    .WithMessage("Condition must be Good / Damaged / Lost");
            });
            return v;
        }
    }

    internal class RecordInventoryReturnCommandHandler : IRequestHandler<RecordInventoryReturnCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RecordInventoryReturnCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RecordInventoryReturnCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.RecordReturnAsync(new RecordReturnDTO
            {
                IssueId = r.IssueId,
                ReturnDate = r.ReturnDate,
                Notes = r.Notes,
                LinesJson = JsonSerializer.Serialize(r.Lines),
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-07: Stock adjustment */
    public record RecordStockAdjustmentCommand(int ItemId, string? AdjustmentType, decimal Quantity, string? Reason, int? ApprovedByUserId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RecordStockAdjustmentCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RecordStockAdjustmentCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RecordStockAdjustmentCommand> v)
        {
            v.RuleFor(c => c.ItemId).GreaterThan(0);
            v.RuleFor(c => c.Quantity).GreaterThan(0);
            v.RuleFor(c => c.Reason).NotEmpty();
            v.RuleFor(c => c.AdjustmentType).NotEmpty()
                .Must(x => x == null || new[] { "Increase", "Decrease" }.Contains(x))
                .WithMessage("AdjustmentType must be Increase / Decrease");
            return v;
        }
    }

    internal class RecordStockAdjustmentCommandHandler : IRequestHandler<RecordStockAdjustmentCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RecordStockAdjustmentCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RecordStockAdjustmentCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.RecordAdjustmentAsync(new StockAdjustmentDTO
            {
                ItemId = r.ItemId,
                AdjustmentType = r.AdjustmentType,
                Quantity = r.Quantity,
                Reason = r.Reason,
                AdjustedBy = user.Id,
                ApprovedByUserId = r.ApprovedByUserId
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-06: Snooze low-stock alert */
    public record SnoozeInventoryAlertCommand(int ItemId, int SnoozeForDays, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SnoozeInventoryAlertCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<SnoozeInventoryAlertCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SnoozeInventoryAlertCommand> v)
        {
            v.RuleFor(c => c.ItemId).GreaterThan(0);
            v.RuleFor(c => c.SnoozeForDays).InclusiveBetween(1, 60);
            v.RuleFor(c => c.Reason).NotEmpty();
            return v;
        }
    }

    internal class SnoozeInventoryAlertCommandHandler : IRequestHandler<SnoozeInventoryAlertCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public SnoozeInventoryAlertCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SnoozeInventoryAlertCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.SnoozeAlertAsync(new SnoozeAlertDTO
            {
                ItemId = r.ItemId, SnoozeForDays = r.SnoozeForDays, Reason = r.Reason, SnoozedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
