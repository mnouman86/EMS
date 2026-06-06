using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Finance.Queries.IncomeQueries;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Finance.Queries.ExpenseDashboardQueries
{
    /* FIN-02 KPIs */
    public record GetExpenseSummaryQuery(int AcademicYearId) : IRequest<OperationResult<ExpenseSummaryResult>>;

    public class ExpenseSummaryResult
    {
        public decimal ExpensesThisMonth { get; set; }
        public decimal ExpensesThisYear { get; set; }
        public string LargestCategoryName { get; set; }
        public decimal LargestCategoryAmount { get; set; }
    }

    internal class GetExpenseSummaryQueryHandler : IRequestHandler<GetExpenseSummaryQuery, OperationResult<ExpenseSummaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetExpenseSummaryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ExpenseSummaryResult>> Handle(GetExpenseSummaryQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetExpenseSummaryAsync(r.AcademicYearId);
            if (res.Code != 200 || res.Data == null) return OperationResult<ExpenseSummaryResult>.FailureResult(res.Message ?? "No data", res.Code);
            return OperationResult<ExpenseSummaryResult>.SuccessResult(_m.Map<ExpenseSummaryResult>(res.Data));
        }
    }

    /* FIN-02 chart: expenses per month (reuses MonthAmountResult shape) */
    public record GetExpenseByMonthQuery(int AcademicYearId) : IRequest<OperationResult<List<MonthAmountResult>>>;

    internal class GetExpenseByMonthQueryHandler : IRequestHandler<GetExpenseByMonthQuery, OperationResult<List<MonthAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetExpenseByMonthQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<MonthAmountResult>>> Handle(GetExpenseByMonthQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetExpenseByMonthAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<MonthAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<MonthAmountResult>>.SuccessResult(_m.Map<List<MonthAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-02 donut: expenses by category (reuses CategoryAmountResult shape) */
    public record GetExpenseByCategoryQuery(int AcademicYearId) : IRequest<OperationResult<List<CategoryAmountResult>>>;

    internal class GetExpenseByCategoryQueryHandler : IRequestHandler<GetExpenseByCategoryQuery, OperationResult<List<CategoryAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetExpenseByCategoryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<CategoryAmountResult>>> Handle(GetExpenseByCategoryQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetExpenseByCategoryAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<CategoryAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CategoryAmountResult>>.SuccessResult(_m.Map<List<CategoryAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
