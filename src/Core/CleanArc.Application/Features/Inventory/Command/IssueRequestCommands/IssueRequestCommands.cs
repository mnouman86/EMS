using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Inventory;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Inventory.Command.IssueRequestCommands
{
    public record IssueRequestLineInput(int ItemId, decimal Quantity);

    /* ---------- Create (any authenticated staff) ---------- */
    public record CreateInventoryIssueRequestCommand(string Purpose, List<IssueRequestLineInput> Lines)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CreateInventoryIssueRequestCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<CreateInventoryIssueRequestCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateInventoryIssueRequestCommand> v)
        {
            v.RuleFor(c => c.Purpose).NotEmpty().MaximumLength(500);
            v.RuleFor(c => c.Lines).NotNull().NotEmpty();
            v.RuleForEach(c => c.Lines).ChildRules(l =>
            {
                l.RuleFor(x => x.ItemId).GreaterThan(0);
                l.RuleFor(x => x.Quantity).GreaterThan(0);
            });
            return v;
        }
    }

    internal class CreateInventoryIssueRequestCommandHandler : IRequestHandler<CreateInventoryIssueRequestCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public CreateInventoryIssueRequestCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateInventoryIssueRequestCommand r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.CreateIssueRequestAsync(new CreateInventoryIssueRequestDTO
            {
                RequestedByUserId = r.UserId,
                Purpose = r.Purpose,
                LinesJson = JsonSerializer.Serialize(r.Lines)
            });
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Approve (admin/principal/accountant) ---------- */
    public record ApproveInventoryIssueRequestCommand(int RequestId) : IRequest<OperationResult<ResponseEntity>>
    {
        [JsonIgnore] public int UserId { get; set; }
    }

    internal class ApproveInventoryIssueRequestCommandHandler : IRequestHandler<ApproveInventoryIssueRequestCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public ApproveInventoryIssueRequestCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ApproveInventoryIssueRequestCommand r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.ApproveIssueRequestAsync(r.RequestId, r.UserId);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Reject ---------- */
    public record RejectInventoryIssueRequestCommand(int RequestId, string Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RejectInventoryIssueRequestCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RejectInventoryIssueRequestCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RejectInventoryIssueRequestCommand> v)
        { v.RuleFor(c => c.Reason).NotEmpty().MaximumLength(500); return v; }
    }

    internal class RejectInventoryIssueRequestCommandHandler : IRequestHandler<RejectInventoryIssueRequestCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public RejectInventoryIssueRequestCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RejectInventoryIssueRequestCommand r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.RejectIssueRequestAsync(r.RequestId, r.UserId, r.Reason);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Fulfill ---------- */
    public record FulfillInventoryIssueRequestCommand(int RequestId, DateTime? IssueDate) : IRequest<OperationResult<ResponseEntity>>
    {
        [JsonIgnore] public int UserId { get; set; }
    }

    internal class FulfillInventoryIssueRequestCommandHandler : IRequestHandler<FulfillInventoryIssueRequestCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public FulfillInventoryIssueRequestCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(FulfillInventoryIssueRequestCommand r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.FulfillIssueRequestAsync(r.RequestId, r.UserId, r.IssueDate ?? DateTime.UtcNow);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
