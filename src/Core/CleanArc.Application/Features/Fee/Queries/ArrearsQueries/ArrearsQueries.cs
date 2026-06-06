using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArc.Application.Features.Fee.Queries.ArrearsQueries
{
    /* FEE-12: List arrears (optionally per student; written-off excluded by default) */
    public record GetFeeArrearsQuery(int? StudentId, bool IncludeWrittenOff)
        : IRequest<OperationResult<List<FeeArrearResult>>>;

    public class FeeArrearResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public int FromAcademicYearId { get; set; }
        public int ToAcademicYearId { get; set; }
        public string FromYearName { get; set; }
        public string ToYearName { get; set; }
        public decimal Amount { get; set; }
        public bool IsWrittenOff { get; set; }
        public string WriteOffReason { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    internal class GetFeeArrearsQueryHandler : IRequestHandler<GetFeeArrearsQuery, OperationResult<List<FeeArrearResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetFeeArrearsQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<FeeArrearResult>>> Handle(GetFeeArrearsQuery r, CancellationToken ct)
        {
            // Per-student call → ownership check first.
            if (_scope.IsTeacherScoped && r.StudentId.HasValue && !await _scope.OwnsStudentAsync(r.StudentId.Value))
                return OperationResult<List<FeeArrearResult>>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.FeeRepository.GetArrearsAsync(r.StudentId, r.IncludeWrittenOff);
            if (res.Code != 200) return OperationResult<List<FeeArrearResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<FeeArrearResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped && !r.StudentId.HasValue)
            {
                var owned = await _scope.FilterOwnedStudentsAsync(rows.Select(x => x.StudentId));
                rows = rows.Where(x => owned.Contains(x.StudentId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<FeeArrearResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }
}
