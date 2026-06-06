using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeSalary
{
    public record GetCurrentEmployeeSalaryQuery(SearchRequestById searchRequestById)
        : IRequest<OperationResult<EmployeeSalaryResult>>;

    public record GetEmployeeSalaryHistoryQuery(SearchRequestById searchRequestById)
        : IRequest<OperationResult<List<EmployeeSalaryResult>>>;

    public class EmployeeSalaryResult
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal FixedDeductions { get; set; }
        public string AllowanceBreakdownJson { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool? IsActive { get; set; }
    }

    internal class GetCurrentEmployeeSalaryQueryHandler : IRequestHandler<GetCurrentEmployeeSalaryQuery, OperationResult<EmployeeSalaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetCurrentEmployeeSalaryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<EmployeeSalaryResult>> Handle(GetCurrentEmployeeSalaryQuery r, CancellationToken ct)
        {
            var res = await _u.EmployeeRepository.GetCurrentSalaryAsync(r.searchRequestById);
            if (res.Code != 200 || res.Data == null)
                return OperationResult<EmployeeSalaryResult>.FailureResult(res.Message ?? "No salary on file", 404);
            return OperationResult<EmployeeSalaryResult>.SuccessResult(_m.Map<EmployeeSalaryResult>(res.Data));
        }
    }

    internal class GetEmployeeSalaryHistoryQueryHandler : IRequestHandler<GetEmployeeSalaryHistoryQuery, OperationResult<List<EmployeeSalaryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetEmployeeSalaryHistoryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<EmployeeSalaryResult>>> Handle(GetEmployeeSalaryHistoryQuery r, CancellationToken ct)
        {
            var res = await _u.EmployeeRepository.GetSalaryHistoryAsync(r.searchRequestById);
            if (res.Code != 200) return OperationResult<List<EmployeeSalaryResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<EmployeeSalaryResult>>.SuccessResult(_m.Map<List<EmployeeSalaryResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
