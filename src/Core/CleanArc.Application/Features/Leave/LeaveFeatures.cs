using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Leave;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Leave;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Leave
{
    /* ====================== Commands ====================== */

    /* --- Leave Type CRUD (admin) --- */
    public record UpsertLeaveTypeCommand : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertLeaveTypeCommand>
    {
        public int? LeaveTypeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal DefaultAnnualQuota { get; set; }
        public bool IsPaid { get; set; } = true;
        public bool RequiresAttachment { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 99;
        [JsonIgnore] public int UserId { get; set; }

        public IValidator<UpsertLeaveTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertLeaveTypeCommand> v)
        {
            v.RuleFor(c => c.Code).NotEmpty().MaximumLength(20);
            v.RuleFor(c => c.Name).NotEmpty().MaximumLength(80);
            v.RuleFor(c => c.DefaultAnnualQuota).GreaterThanOrEqualTo(0);
            return v;
        }
    }
    internal class UpsertLeaveTypeHandler : IRequestHandler<UpsertLeaveTypeCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public UpsertLeaveTypeHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertLeaveTypeCommand r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.UpsertLeaveTypeAsync(new UpsertLeaveTypeDTO
            {
                LeaveTypeId = r.LeaveTypeId, Code = r.Code, Name = r.Name,
                DefaultAnnualQuota = r.DefaultAnnualQuota, IsPaid = r.IsPaid,
                RequiresAttachment = r.RequiresAttachment, IsActive = r.IsActive, DisplayOrder = r.DisplayOrder
            }, r.UserId);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    public record DeleteLeaveTypeCommand(int LeaveTypeId) : IRequest<OperationResult<ResponseEntity>>
    { [JsonIgnore] public int UserId { get; set; } }
    internal class DeleteLeaveTypeHandler : IRequestHandler<DeleteLeaveTypeCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public DeleteLeaveTypeHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteLeaveTypeCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.DeleteLeaveTypeAsync(r.LeaveTypeId, r.UserId));
    }

    /* --- Leave Policy (per-employee overrides) --- */
    public record UpsertLeavePolicyCommand(int EmployeeId, int LeaveTypeId, int AcademicYearId, decimal AnnualQuota, string? Notes)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertLeavePolicyCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertLeavePolicyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertLeavePolicyCommand> v)
        {
            v.RuleFor(c => c.EmployeeId).GreaterThan(0);
            v.RuleFor(c => c.LeaveTypeId).GreaterThan(0);
            v.RuleFor(c => c.AcademicYearId).GreaterThan(0);
            v.RuleFor(c => c.AnnualQuota).GreaterThanOrEqualTo(0);
            return v;
        }
    }
    internal class UpsertLeavePolicyHandler : IRequestHandler<UpsertLeavePolicyCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public UpsertLeavePolicyHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertLeavePolicyCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.UpsertLeavePolicyAsync(new UpsertLeavePolicyDTO
            {
                EmployeeId = r.EmployeeId, LeaveTypeId = r.LeaveTypeId, AcademicYearId = r.AcademicYearId,
                AnnualQuota = r.AnnualQuota, Notes = r.Notes
            }, r.UserId));
    }

    /* --- Approval routing --- */
    public record UpsertLeaveRouteCommand(int ApplicantRoleId, int ApproverUserId, string? Notes)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertLeaveRouteCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertLeaveRouteCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertLeaveRouteCommand> v)
        {
            v.RuleFor(c => c.ApplicantRoleId).GreaterThan(0);
            v.RuleFor(c => c.ApproverUserId).GreaterThan(0);
            return v;
        }
    }
    internal class UpsertLeaveRouteHandler : IRequestHandler<UpsertLeaveRouteCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public UpsertLeaveRouteHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertLeaveRouteCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.UpsertLeaveRouteAsync(new UpsertLeaveRouteDTO
            {
                ApplicantRoleId = r.ApplicantRoleId, ApproverUserId = r.ApproverUserId, Notes = r.Notes
            }, r.UserId));
    }

    /* --- Application submit --- */
    public record SubmitLeaveApplicationCommand : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SubmitLeaveApplicationCommand>
    {
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool HalfDayFrom { get; set; }
        public bool HalfDayTo { get; set; }
        public string? Reason { get; set; }
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        [JsonIgnore] public int ApplicantUserId { get; set; }

        public IValidator<SubmitLeaveApplicationCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SubmitLeaveApplicationCommand> v)
        {
            v.RuleFor(c => c.LeaveTypeId).GreaterThan(0);
            v.RuleFor(c => c.EndDate).GreaterThanOrEqualTo(c => c.StartDate)
                .WithMessage("End date must be on or after start date.");
            return v;
        }
    }
    internal class SubmitLeaveApplicationHandler : IRequestHandler<SubmitLeaveApplicationCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public SubmitLeaveApplicationHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SubmitLeaveApplicationCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.SubmitLeaveApplicationAsync(new SubmitLeaveApplicationDTO
            {
                ApplicantUserId = r.ApplicantUserId, LeaveTypeId = r.LeaveTypeId,
                StartDate = r.StartDate, EndDate = r.EndDate,
                HalfDayFrom = r.HalfDayFrom, HalfDayTo = r.HalfDayTo,
                Reason = r.Reason,
                AttachmentPath = r.AttachmentPath, AttachmentOriginalName = r.AttachmentOriginalName
            }));
    }

    /* --- Preview working days (SPA uses this to show accurate day count) --- */
    public record PreviewLeaveWorkingDaysQuery(DateTime StartDate, DateTime EndDate, bool HalfDayFrom, bool HalfDayTo)
        : IRequest<OperationResult<decimal>>;
    internal class PreviewLeaveWorkingDaysHandler : IRequestHandler<PreviewLeaveWorkingDaysQuery, OperationResult<decimal>>
    {
        private readonly IUnitOfWork _u; public PreviewLeaveWorkingDaysHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<decimal>> Handle(PreviewLeaveWorkingDaysQuery r, CancellationToken ct)
        {
            if (r.EndDate < r.StartDate) return OperationResult<decimal>.FailureResult("End date must be >= start date.", 400);
            var days = await _u.LeaveRepository.PreviewWorkingDaysAsync(r.StartDate, r.EndDate, r.HalfDayFrom, r.HalfDayTo);
            return OperationResult<decimal>.SuccessResult(days);
        }
    }

    /* --- Decide / Cancel --- */
    public record DecideLeaveApplicationCommand(int LeaveApplicationId, string Decision, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DecideLeaveApplicationCommand>
    {
        [JsonIgnore] public int DeciderUserId { get; set; }
        public IValidator<DecideLeaveApplicationCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DecideLeaveApplicationCommand> v)
        {
            v.RuleFor(c => c.LeaveApplicationId).GreaterThan(0);
            v.RuleFor(c => c.Decision).Must(x => x == "Approved" || x == "Rejected")
                .WithMessage("Decision must be Approved or Rejected.");
            v.RuleFor(c => c.Reason).NotEmpty().MinimumLength(3)
                .When(c => c.Decision == "Rejected")
                .WithMessage("Rejection reason is required.");
            return v;
        }
    }
    internal class DecideLeaveApplicationHandler : IRequestHandler<DecideLeaveApplicationCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public DecideLeaveApplicationHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DecideLeaveApplicationCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.DecideLeaveApplicationAsync(r.LeaveApplicationId, r.DeciderUserId, r.Decision, r.Reason));
    }

    public record CancelLeaveApplicationCommand(int LeaveApplicationId, string? Reason) : IRequest<OperationResult<ResponseEntity>>
    { [JsonIgnore] public int ActorUserId { get; set; } }
    internal class CancelLeaveApplicationHandler : IRequestHandler<CancelLeaveApplicationCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; public CancelLeaveApplicationHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(CancelLeaveApplicationCommand r, CancellationToken ct)
            => OperationResult<ResponseEntity>.SuccessResult(await _u.LeaveRepository.CancelLeaveApplicationAsync(r.LeaveApplicationId, r.ActorUserId, r.Reason));
    }

    /* ====================== Queries ====================== */

    public record GetLeaveTypesQuery(bool ActiveOnly = false) : IRequest<OperationResult<List<LeaveType>>>;
    internal class GetLeaveTypesHandler : IRequestHandler<GetLeaveTypesQuery, OperationResult<List<LeaveType>>>
    {
        private readonly IUnitOfWork _u; public GetLeaveTypesHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<LeaveType>>> Handle(GetLeaveTypesQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetAllLeaveTypesAsync(r.ActiveOnly);
            if (res.Code != 200) return OperationResult<List<LeaveType>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LeaveType>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetLeavePoliciesQuery(int? AcademicYearId, int? EmployeeId) : IRequest<OperationResult<List<LeavePolicyRow>>>;
    internal class GetLeavePoliciesHandler : IRequestHandler<GetLeavePoliciesQuery, OperationResult<List<LeavePolicyRow>>>
    {
        private readonly IUnitOfWork _u; public GetLeavePoliciesHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<LeavePolicyRow>>> Handle(GetLeavePoliciesQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetAllLeavePoliciesAsync(r.AcademicYearId, r.EmployeeId);
            if (res.Code != 200) return OperationResult<List<LeavePolicyRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LeavePolicyRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetLeaveRoutesQuery : IRequest<OperationResult<List<LeaveApprovalRouteRow>>>;
    internal class GetLeaveRoutesHandler : IRequestHandler<GetLeaveRoutesQuery, OperationResult<List<LeaveApprovalRouteRow>>>
    {
        private readonly IUnitOfWork _u; public GetLeaveRoutesHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<LeaveApprovalRouteRow>>> Handle(GetLeaveRoutesQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetAllLeaveRoutesAsync();
            if (res.Code != 200) return OperationResult<List<LeaveApprovalRouteRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LeaveApprovalRouteRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetLeaveBalanceQuery(int? AcademicYearId) : IRequest<OperationResult<List<LeaveBalanceRow>>>
    { [JsonIgnore] public int UserId { get; set; } }
    internal class GetLeaveBalanceHandler : IRequestHandler<GetLeaveBalanceQuery, OperationResult<List<LeaveBalanceRow>>>
    {
        private readonly IUnitOfWork _u; public GetLeaveBalanceHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<LeaveBalanceRow>>> Handle(GetLeaveBalanceQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetLeaveBalanceAsync(r.UserId, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<LeaveBalanceRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LeaveBalanceRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetLeaveApplicationsQuery : IRequest<OperationResult<List<LeaveApplicationRow>>>
    {
        public string Scope { get; set; } = "Mine";
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? LeaveTypeId { get; set; }
        [JsonIgnore] public int CallerUserId { get; set; }
    }
    internal class GetLeaveApplicationsHandler : IRequestHandler<GetLeaveApplicationsQuery, OperationResult<List<LeaveApplicationRow>>>
    {
        private readonly IUnitOfWork _u; public GetLeaveApplicationsHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<LeaveApplicationRow>>> Handle(GetLeaveApplicationsQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetLeaveApplicationsAsync(new GetLeaveApplicationsFilter
            {
                Scope = r.Scope, Status = r.Status, FromDate = r.FromDate, ToDate = r.ToDate, LeaveTypeId = r.LeaveTypeId
            }, r.CallerUserId);
            if (res.Code != 200) return OperationResult<List<LeaveApplicationRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LeaveApplicationRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetLeaveDashboardQuery : IRequest<OperationResult<LeaveDashboardBundle>>;
    internal class GetLeaveDashboardHandler : IRequestHandler<GetLeaveDashboardQuery, OperationResult<LeaveDashboardBundle>>
    {
        private readonly IUnitOfWork _u; public GetLeaveDashboardHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<LeaveDashboardBundle>> Handle(GetLeaveDashboardQuery r, CancellationToken ct)
        {
            var res = await _u.LeaveRepository.GetDashboardAsync();
            if (res.Code != 200) return OperationResult<LeaveDashboardBundle>.FailureResult(res.Message, res.Code);
            return OperationResult<LeaveDashboardBundle>.SuccessResult(res.Data, res.Code, res.Message);
        }
    }
}
