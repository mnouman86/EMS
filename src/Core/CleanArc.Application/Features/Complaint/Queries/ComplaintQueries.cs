using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Complaint;
using CleanArc.Domain.Entities.Complaint;
using Mediator;
using System.Linq;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Complaint.Queries
{
    /* Nature dropdown / admin list */
    public record GetComplaintNaturesQuery(bool ActiveOnly = false) : IRequest<OperationResult<List<ComplaintNature>>>;

    internal class GetComplaintNaturesHandler : IRequestHandler<GetComplaintNaturesQuery, OperationResult<List<ComplaintNature>>>
    {
        private readonly IUnitOfWork _u;
        public GetComplaintNaturesHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<ComplaintNature>>> Handle(GetComplaintNaturesQuery r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.GetNaturesAsync(r.ActiveOnly);
            if (res.Code != 200) return OperationResult<List<ComplaintNature>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ComplaintNature>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    /* My complaints — uses logged-in user from claim */
    public record GetMyComplaintsQuery : IRequest<OperationResult<List<ComplaintRow>>>
    {
        [JsonIgnore] public int LogonUserId { get; set; }
    }

    internal class GetMyComplaintsHandler : IRequestHandler<GetMyComplaintsQuery, OperationResult<List<ComplaintRow>>>
    {
        private readonly IUnitOfWork _u;
        public GetMyComplaintsHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<ComplaintRow>>> Handle(GetMyComplaintsQuery r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.GetMyComplaintsAsync(r.LogonUserId);
            if (res.Code != 200) return OperationResult<List<ComplaintRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ComplaintRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    /* Admin / Reviewer report */
    public record GetAllComplaintsQuery : IRequest<OperationResult<List<ComplaintRow>>>
    {
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public string? ComplaintType { get; set; }
        public int? ComplaintNatureId { get; set; }
        public string? Status { get; set; }
        public string? ComplainantLike { get; set; }
        public int? LogonUserId { get; set; }
        public bool? HasAttachment { get; set; }
        public bool IncludeDeleted { get; set; }
    }

    internal class GetAllComplaintsHandler : IRequestHandler<GetAllComplaintsQuery, OperationResult<List<ComplaintRow>>>
    {
        private readonly IUnitOfWork _u;
        public GetAllComplaintsHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<List<ComplaintRow>>> Handle(GetAllComplaintsQuery r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.GetAllComplaintsAsync(new GetComplaintsReportDTO
            {
                FromDate = r.FromDate, ToDate = r.ToDate,
                ComplaintType = r.ComplaintType, ComplaintNatureId = r.ComplaintNatureId,
                Status = r.Status, ComplainantLike = r.ComplainantLike,
                LogonUserId = r.LogonUserId, HasAttachment = r.HasAttachment,
                IncludeDeleted = r.IncludeDeleted
            });
            if (res.Code != 200) return OperationResult<List<ComplaintRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ComplaintRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    /* Detail (header + audit trail) */
    public record GetComplaintDetailQuery(int ComplaintId) : IRequest<OperationResult<ComplaintDetail>>
    {
        [JsonIgnore] public int CallerUserId { get; set; }
        [JsonIgnore] public bool IsAdmin { get; set; }
    }

    internal class GetComplaintDetailHandler : IRequestHandler<GetComplaintDetailQuery, OperationResult<ComplaintDetail>>
    {
        private readonly IUnitOfWork _u;
        public GetComplaintDetailHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ComplaintDetail>> Handle(GetComplaintDetailQuery r, CancellationToken ct)
        {
            var res = await _u.ComplaintRepository.GetComplaintDetailAsync(r.ComplaintId, r.CallerUserId, r.IsAdmin);
            if (res.Code != 200) return OperationResult<ComplaintDetail>.FailureResult(res.Message, res.Code);
            return OperationResult<ComplaintDetail>.SuccessResult(res.Data!, res.Code, res.Message);
        }
    }
}
