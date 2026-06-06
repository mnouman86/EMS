using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeAdvances
{
    public record GetEmployeeAdvancesQuery(SearchRequestById searchRequestById)
        : IRequest<OperationResult<List<EmployeeAdvanceResult>>>;

    public class EmployeeAdvanceResult
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string Reason { get; set; }
        public DateTime IssuedAt { get; set; }
        public string Status { get; set; }
        public DateTime? SettledAt { get; set; }
    }

    internal class GetEmployeeAdvancesQueryHandler : IRequestHandler<GetEmployeeAdvancesQuery, OperationResult<List<EmployeeAdvanceResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetEmployeeAdvancesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<EmployeeAdvanceResult>>> Handle(GetEmployeeAdvancesQuery r, CancellationToken ct)
        {
            var res = await _u.EmployeeRepository.GetAdvancesAsync(r.searchRequestById);
            if (res.Code != 200) return OperationResult<List<EmployeeAdvanceResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<EmployeeAdvanceResult>>.SuccessResult(_m.Map<List<EmployeeAdvanceResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
