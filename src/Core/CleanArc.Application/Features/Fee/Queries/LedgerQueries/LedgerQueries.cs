using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Fee.Queries.LedgerQueries
{
    /* FEE-04 / FEE-13: student ledger (all invoices + payments running balance) */
    public record GetStudentLedgerQuery(int StudentId, int? AcademicYearId)
        : IRequest<OperationResult<List<StudentLedgerEntryResult>>>;

    public class StudentLedgerEntryResult
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal RunningBalance { get; set; }
    }

    internal class GetStudentLedgerQueryHandler : IRequestHandler<GetStudentLedgerQuery, OperationResult<List<StudentLedgerEntryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetStudentLedgerQueryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<StudentLedgerEntryResult>>> Handle(GetStudentLedgerQuery r, CancellationToken ct)
        {
            // Fee module = class teacher's own-class responsibility (CT-only).
            if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(r.StudentId, TeacherScopeKind.ClassTeacher))
                return OperationResult<List<StudentLedgerEntryResult>>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.FeeRepository.GetStudentLedgerAsync(r.StudentId, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<StudentLedgerEntryResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StudentLedgerEntryResult>>.SuccessResult(_m.Map<List<StudentLedgerEntryResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
