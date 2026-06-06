using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Expense.Queries.PayrollQueries
{
    public record GetPayrollRunsQuery(int? Year) : IRequest<OperationResult<List<PayrollRunResult>>>;

    public class PayrollRunResult
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalNet { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }

    internal class GetPayrollRunsQueryHandler : IRequestHandler<GetPayrollRunsQuery, OperationResult<List<PayrollRunResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetPayrollRunsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PayrollRunResult>>> Handle(GetPayrollRunsQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetPayrollRunsAsync(r.Year);
            if (res.Code != 200) return OperationResult<List<PayrollRunResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PayrollRunResult>>.SuccessResult(_m.Map<List<PayrollRunResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetPayrollEntriesQuery(int PayrollRunId) : IRequest<OperationResult<List<PayrollEntryResult>>>;

    public class PayrollEntryResult
    {
        public int Id { get; set; }
        public int PayrollRunId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string Designation { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal FixedDeductions { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal FineDeduction { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal Gross { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPayable { get; set; }
        public string SalarySlipNo { get; set; }
        public int? LinkedExpenseId { get; set; }
        public string Notes { get; set; }
    }

    internal class GetPayrollEntriesQueryHandler : IRequestHandler<GetPayrollEntriesQuery, OperationResult<List<PayrollEntryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetPayrollEntriesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PayrollEntryResult>>> Handle(GetPayrollEntriesQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetPayrollEntriesAsync(r.PayrollRunId);
            if (res.Code != 200) return OperationResult<List<PayrollEntryResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PayrollEntryResult>>.SuccessResult(_m.Map<List<PayrollEntryResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
