using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Complaint;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Complaint.Command
{
    /* ---------- Nature: upsert / deactivate ---------- */

    public record UpsertComplaintNatureCommand
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertComplaintNatureCommand>
    {
        public int? ComplaintNatureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ComplaintType { get; set; } = "Complaint";
        public int DisplayOrder { get; set; } = 99;
        public bool IsActive { get; set; } = true;

        public IValidator<UpsertComplaintNatureCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertComplaintNatureCommand> v)
        {
            v.RuleFor(c => c.Name).NotEmpty().MaximumLength(120);
            v.RuleFor(c => c.ComplaintType).NotEmpty()
                .Must(t => t == "Complaint" || t == "Suggestion" || t == "Either")
                .WithMessage("ComplaintType must be Complaint, Suggestion or Either.");
            return v;
        }
    }

    internal class UpsertComplaintNatureHandler : IRequestHandler<UpsertComplaintNatureCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public UpsertComplaintNatureHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertComplaintNatureCommand r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.UpsertNatureAsync(new UpsertComplaintNatureDTO
            {
                ComplaintNatureId = r.ComplaintNatureId,
                Name = r.Name,
                ComplaintType = r.ComplaintType,
                DisplayOrder = r.DisplayOrder,
                IsActive = r.IsActive
            });
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    public record DeactivateComplaintNatureCommand(int ComplaintNatureId) : IRequest<OperationResult<ResponseEntity>>;
    internal class DeactivateComplaintNatureHandler : IRequestHandler<DeactivateComplaintNatureCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public DeactivateComplaintNatureHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeactivateComplaintNatureCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.ComplaintRepository.DeactivateNatureAsync(r.ComplaintNatureId));
    }

    /* ---------- Complaint: create / update ---------- */

    public record CreateComplaintCommand
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CreateComplaintCommand>
    {
        public int ComplaintNatureId { get; set; }
        public string ComplainantName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? ComplaintAgainst { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        [JsonIgnore] public int LogonUserId { get; set; }

        public IValidator<CreateComplaintCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateComplaintCommand> v)
        {
            v.RuleFor(c => c.ComplaintNatureId).GreaterThan(0);
            v.RuleFor(c => c.ComplainantName).NotEmpty().MaximumLength(200);
            v.RuleFor(c => c.ContactNumber).NotEmpty().MaximumLength(40);
            v.RuleFor(c => c.Description).NotEmpty().MinimumLength(10).MaximumLength(2000);
            v.RuleFor(c => c.ComplaintAgainst!).MaximumLength(200).When(c => c.ComplaintAgainst != null);
            return v;
        }
    }

    internal class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public CreateComplaintHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateComplaintCommand r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.CreateComplaintAsync(new CreateComplaintDTO
            {
                LogonUserId = r.LogonUserId,
                ComplaintNatureId = r.ComplaintNatureId,
                ComplainantName = r.ComplainantName,
                ContactNumber = r.ContactNumber,
                ComplaintAgainst = r.ComplaintAgainst,
                Description = r.Description,
                AttachmentPath = r.AttachmentPath,
                AttachmentOriginalName = r.AttachmentOriginalName
            });
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    public record UpdateComplaintCommand
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpdateComplaintCommand>
    {
        public int ComplaintId { get; set; }
        public int ComplaintNatureId { get; set; }
        public string ComplainantName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? ComplaintAgainst { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        public bool ClearAttachment { get; set; }
        [JsonIgnore] public int ActorUserId { get; set; }

        public IValidator<UpdateComplaintCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateComplaintCommand> v)
        {
            v.RuleFor(c => c.ComplaintId).GreaterThan(0);
            v.RuleFor(c => c.ComplaintNatureId).GreaterThan(0);
            v.RuleFor(c => c.ComplainantName).NotEmpty().MaximumLength(200);
            v.RuleFor(c => c.ContactNumber).NotEmpty().MaximumLength(40);
            v.RuleFor(c => c.Description).NotEmpty().MinimumLength(10).MaximumLength(2000);
            return v;
        }
    }

    internal class UpdateComplaintHandler : IRequestHandler<UpdateComplaintCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public UpdateComplaintHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateComplaintCommand r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.UpdateComplaintAsync(new UpdateComplaintDTO
            {
                ComplaintId = r.ComplaintId,
                ActorUserId = r.ActorUserId,
                ComplaintNatureId = r.ComplaintNatureId,
                ComplainantName = r.ComplainantName,
                ContactNumber = r.ContactNumber,
                ComplaintAgainst = r.ComplaintAgainst,
                Description = r.Description,
                AttachmentPath = r.AttachmentPath,
                AttachmentOriginalName = r.AttachmentOriginalName,
                ClearAttachment = r.ClearAttachment
            });
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Admin: status / note / soft-delete / restore ---------- */

    public record ChangeComplaintStatusCommand(int ComplaintId, string ToStatus, string Note)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ChangeComplaintStatusCommand>
    {
        [JsonIgnore] public int ActorUserId { get; set; }
        public IValidator<ChangeComplaintStatusCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ChangeComplaintStatusCommand> v)
        {
            v.RuleFor(c => c.ToStatus).NotEmpty().MaximumLength(20);
            v.RuleFor(c => c.Note).NotEmpty().MinimumLength(10).MaximumLength(2000);
            return v;
        }
    }

    internal class ChangeComplaintStatusHandler : IRequestHandler<ChangeComplaintStatusCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public ChangeComplaintStatusHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ChangeComplaintStatusCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.ComplaintRepository.ChangeStatusAsync(r.ComplaintId, r.ActorUserId, r.ToStatus, r.Note));
    }

    public record AddComplaintNoteCommand(int ComplaintId, string Note)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<AddComplaintNoteCommand>
    {
        [JsonIgnore] public int ActorUserId { get; set; }
        public IValidator<AddComplaintNoteCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddComplaintNoteCommand> v)
        { v.RuleFor(c => c.Note).NotEmpty().MaximumLength(2000); return v; }
    }

    internal class AddComplaintNoteHandler : IRequestHandler<AddComplaintNoteCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public AddComplaintNoteHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(AddComplaintNoteCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.ComplaintRepository.AddNoteAsync(r.ComplaintId, r.ActorUserId, r.Note));
    }

    public record SoftDeleteComplaintCommand(int ComplaintId, string Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SoftDeleteComplaintCommand>
    {
        [JsonIgnore] public int ActorUserId { get; set; }
        public IValidator<SoftDeleteComplaintCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SoftDeleteComplaintCommand> v)
        { v.RuleFor(c => c.Reason).NotEmpty().MinimumLength(10).MaximumLength(500); return v; }
    }

    internal class SoftDeleteComplaintHandler : IRequestHandler<SoftDeleteComplaintCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public SoftDeleteComplaintHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SoftDeleteComplaintCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.ComplaintRepository.SoftDeleteAsync(r.ComplaintId, r.ActorUserId, r.Reason));
    }

    public record RestoreComplaintCommand(int ComplaintId) : IRequest<OperationResult<ResponseEntity>>
    {
        [JsonIgnore] public int ActorUserId { get; set; }
    }

    internal class RestoreComplaintHandler : IRequestHandler<RestoreComplaintCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public RestoreComplaintHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RestoreComplaintCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.ComplaintRepository.RestoreAsync(r.ComplaintId, r.ActorUserId));
    }
}
