using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArc.Application.Features.Fee.Queries.DashboardQueries
{
    /* FEE-05: Collection dashboard — three datasets returned via three queries
       (kept separate so each can be cached / refreshed independently).
       Teachers never see institution-wide aggregates (Summary, ByDay); the
       per-class breakdown is filtered to their assigned classes. */

    public record GetCollectionSummaryQuery(DateTime FromDate, DateTime ToDate, int? ClassId)
        : IRequest<OperationResult<CollectionSummaryResult>>;

    public class CollectionSummaryResult
    {
        public decimal TotalCollected { get; set; }
        public decimal TotalPending { get; set; }
        public decimal TotalInvoiced { get; set; }
        public decimal CollectionRatePercent { get; set; }
    }

    internal class GetCollectionSummaryQueryHandler : IRequestHandler<GetCollectionSummaryQuery, OperationResult<CollectionSummaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetCollectionSummaryQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<CollectionSummaryResult>> Handle(GetCollectionSummaryQuery r, CancellationToken ct)
        {
            // Institution-wide aggregate: teachers see only their classes' aggregate when
            // a ClassId is supplied AND that class is in scope; otherwise empty result.
            if (_scope.IsTeacherScoped)
            {
                if (!r.ClassId.HasValue) return OperationResult<CollectionSummaryResult>.SuccessResult(new CollectionSummaryResult());
                var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.ClassTeacher);
                if (!classIds.Contains(r.ClassId.Value)) return OperationResult<CollectionSummaryResult>.SuccessResult(new CollectionSummaryResult());
            }

            var res = await _u.FeeRepository.GetCollectionSummaryAsync(r.FromDate, r.ToDate, r.ClassId);
            if (res.Code != 200 || res.Data == null) return OperationResult<CollectionSummaryResult>.FailureResult(res.Message ?? "No data", res.Code);
            return OperationResult<CollectionSummaryResult>.SuccessResult(_m.Map<CollectionSummaryResult>(res.Data));
        }
    }

    public record GetCollectionByClassQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<CollectionByClassResult>>>;

    public class CollectionByClassResult
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int StudentCount { get; set; }
        public decimal Invoiced { get; set; }
        public decimal Collected { get; set; }
        public decimal Pending { get; set; }
        public decimal RatePercent { get; set; }
    }

    internal class GetCollectionByClassQueryHandler : IRequestHandler<GetCollectionByClassQuery, OperationResult<List<CollectionByClassResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetCollectionByClassQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<CollectionByClassResult>>> Handle(GetCollectionByClassQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetCollectionByClassAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<CollectionByClassResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<CollectionByClassResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped)
            {
                var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.ClassTeacher);
                rows = rows.Where(x => classIds.Contains(x.ClassId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<CollectionByClassResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }

    public record GetCollectionByDayQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<CollectionByDayResult>>>;

    public class CollectionByDayResult
    {
        public DateTime Day { get; set; }
        public decimal Amount { get; set; }
    }

    internal class GetCollectionByDayQueryHandler : IRequestHandler<GetCollectionByDayQuery, OperationResult<List<CollectionByDayResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetCollectionByDayQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<CollectionByDayResult>>> Handle(GetCollectionByDayQuery r, CancellationToken ct)
        {
            // Day-grain aggregate carries no class context — skip for teachers.
            if (_scope.IsTeacherScoped) return OperationResult<List<CollectionByDayResult>>.SuccessResult(new List<CollectionByDayResult>(), 200, "Not available", 0);

            var res = await _u.FeeRepository.GetCollectionByDayAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<CollectionByDayResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CollectionByDayResult>>.SuccessResult(_m.Map<List<CollectionByDayResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
