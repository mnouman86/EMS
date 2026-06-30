using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System.Collections.Generic;
using System.Linq;

namespace CleanArc.Application.Features.Fee.Queries.ReportQueries
{
    /* FEE-06: Pending fee list (defaulter report) */
    public record GetPendingFeeListQuery(int? ClassId, int? MinOutstanding, int? MinDaysOverdue)
        : IRequest<OperationResult<List<PendingFeeRowResult>>>;

    public class PendingFeeRowResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string MonthsDue { get; set; }
        public decimal TotalOutstanding { get; set; }
        public int DaysOverdue { get; set; }
        public string ParentMobile { get; set; }
        public string EmergencyContactPhone { get; set; }
        public bool HasActiveWaiver { get; set; }
    }

    internal class GetPendingFeeListQueryHandler : IRequestHandler<GetPendingFeeListQuery, OperationResult<List<PendingFeeRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetPendingFeeListQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<PendingFeeRowResult>>> Handle(GetPendingFeeListQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetPendingFeeListAsync(r.ClassId, r.MinOutstanding, r.MinDaysOverdue);
            if (res.Code != 200) return OperationResult<List<PendingFeeRowResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<PendingFeeRowResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped)
            {
                var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.ClassTeacher);
                rows = rows.Where(x => classIds.Contains(x.ClassId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<PendingFeeRowResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }

    /* FEE-07: Month-specific non-submitted list */
    public record GetMonthlyNonSubmittedQuery(int Month, int Year, int? ClassId)
        : IRequest<OperationResult<List<PendingFeeRowResult>>>;

    internal class GetMonthlyNonSubmittedQueryHandler : IRequestHandler<GetMonthlyNonSubmittedQuery, OperationResult<List<PendingFeeRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetMonthlyNonSubmittedQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<PendingFeeRowResult>>> Handle(GetMonthlyNonSubmittedQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetMonthlyNonSubmittedAsync(r.Month, r.Year, r.ClassId);
            if (res.Code != 200) return OperationResult<List<PendingFeeRowResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<PendingFeeRowResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped)
            {
                var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.ClassTeacher);
                rows = rows.Where(x => classIds.Contains(x.ClassId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<PendingFeeRowResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }

    /* FEE-14: Outstanding ageing report (0-30 / 31-60 / 61-90 / 90+) */
    public record GetOutstandingAgeingQuery(int? ClassId)
        : IRequest<OperationResult<List<AgeingRowResult>>>;

    public class AgeingRowResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public decimal Bucket0_30 { get; set; }
        public decimal Bucket31_60 { get; set; }
        public decimal Bucket61_90 { get; set; }
        public decimal Bucket91Plus { get; set; }
        public decimal Total { get; set; }
    }

    internal class GetOutstandingAgeingQueryHandler : IRequestHandler<GetOutstandingAgeingQuery, OperationResult<List<AgeingRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetOutstandingAgeingQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<AgeingRowResult>>> Handle(GetOutstandingAgeingQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetOutstandingAgeingAsync(r.ClassId);
            if (res.Code != 200) return OperationResult<List<AgeingRowResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<AgeingRowResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped)
            {
                // Ageing rows don't carry ClassId — batched ownership filter.
                var owned = await _scope.FilterOwnedStudentsAsync(rows.Select(x => x.StudentId), TeacherScopeKind.ClassTeacher);
                rows = rows.Where(x => owned.Contains(x.StudentId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<AgeingRowResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }
}
