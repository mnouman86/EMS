using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Expense.Queries.ExpenseQueries
{
    /* EXP-03 list */
    public record GetExpensesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<ExpenseResult>>>;

    public class ExpenseResult
    {
        public int Id { get; set; }
        public string ExpenseCode { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public string PaidTo { get; set; }
        public string AttachmentPath { get; set; }
        public int? LinkedPurchaseId { get; set; }
        public int? LinkedPayrollEntryId { get; set; }
    }

    internal class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, OperationResult<List<ExpenseResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetExpensesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<ExpenseResult>>> Handle(GetExpensesQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetExpensesAsync(r.searchRequest);
            if (res.Code != 200) return OperationResult<List<ExpenseResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ExpenseResult>>.SuccessResult(_m.Map<List<ExpenseResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* EXP-04: Budget vs Actual monitoring per category */
    public record GetBudgetMonitoringQuery(int Month, int Year) : IRequest<OperationResult<List<BudgetMonitoringResult>>>;

    public class BudgetMonitoringResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? Budget { get; set; }
        public decimal Spent { get; set; }
        public decimal? Remaining { get; set; }
        public decimal? PercentUsed { get; set; }
        public string AlertLevel { get; set; }
    }

    internal class GetBudgetMonitoringQueryHandler : IRequestHandler<GetBudgetMonitoringQuery, OperationResult<List<BudgetMonitoringResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetBudgetMonitoringQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<BudgetMonitoringResult>>> Handle(GetBudgetMonitoringQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetBudgetMonitoringAsync(r.Month, r.Year);
            if (res.Code != 200) return OperationResult<List<BudgetMonitoringResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<BudgetMonitoringResult>>.SuccessResult(_m.Map<List<BudgetMonitoringResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
