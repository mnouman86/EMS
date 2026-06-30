using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArc.Application.Features.Fee.Queries.ConcessionQueries
{
    /* FEE-09: List concessions (optionally per student) */
    public record GetConcessionsQuery(int? StudentId) : IRequest<OperationResult<List<ConcessionResult>>>;

    public class ConcessionResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string ConcessionType { get; set; }
        public decimal Value { get; set; }
        public string ApplicableFeeTypesJson { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }

    internal class GetConcessionsQueryHandler : IRequestHandler<GetConcessionsQuery, OperationResult<List<ConcessionResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetConcessionsQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<ConcessionResult>>> Handle(GetConcessionsQuery r, CancellationToken ct)
        {
            if (_scope.IsTeacherScoped && r.StudentId.HasValue && !await _scope.OwnsStudentAsync(r.StudentId.Value, TeacherScopeKind.ClassTeacher))
                return OperationResult<List<ConcessionResult>>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.FeeRepository.GetConcessionsAsync(r.StudentId);
            if (res.Code != 200) return OperationResult<List<ConcessionResult>>.FailureResult(res.Message, res.Code);

            var rows = _m.Map<List<ConcessionResult>>(res.Data);
            var total = res.TotalCount;
            if (_scope.IsTeacherScoped && !r.StudentId.HasValue)
            {
                var owned = await _scope.FilterOwnedStudentsAsync(rows.Select(x => x.StudentId), TeacherScopeKind.ClassTeacher);
                rows = rows.Where(x => owned.Contains(x.StudentId)).ToList();
                total = rows.Count;
            }
            return OperationResult<List<ConcessionResult>>.SuccessResult(rows, res.Code, res.Message, total);
        }
    }

    /* FEE-08: Get advance balance for a student */
    public record GetAdvanceBalanceQuery(int StudentId) : IRequest<OperationResult<AdvanceBalanceResult>>;

    public class AdvanceBalanceResult
    {
        public int StudentId { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }

    internal class GetAdvanceBalanceQueryHandler : IRequestHandler<GetAdvanceBalanceQuery, OperationResult<AdvanceBalanceResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetAdvanceBalanceQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<AdvanceBalanceResult>> Handle(GetAdvanceBalanceQuery r, CancellationToken ct)
        {
            if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(r.StudentId, TeacherScopeKind.ClassTeacher))
                return OperationResult<AdvanceBalanceResult>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.FeeRepository.GetAdvanceBalanceAsync(r.StudentId);
            if (res.Code != 200 || res.Data == null)
                return OperationResult<AdvanceBalanceResult>.SuccessResult(new AdvanceBalanceResult { StudentId = r.StudentId });
            return OperationResult<AdvanceBalanceResult>.SuccessResult(_m.Map<AdvanceBalanceResult>(res.Data));
        }
    }
}
