using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Finance.Queries.IncomeQueries;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Finance.Queries.ReportQueries
{
    public class PnLRowResult
    {
        public string Section { get; set; }
        public string Line { get; set; }
        public decimal Amount { get; set; }
    }

    /* FIN-03: P&L for any period (frontend can call twice for comparison view) */
    public record GetPnLStatementQuery(DateTime FromDate, DateTime ToDate) : IRequest<OperationResult<List<PnLRowResult>>>;

    internal class GetPnLStatementQueryHandler : IRequestHandler<GetPnLStatementQuery, OperationResult<List<PnLRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetPnLStatementQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PnLRowResult>>> Handle(GetPnLStatementQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetPnLAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<PnLRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PnLRowResult>>.SuccessResult(_m.Map<List<PnLRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-04 (a): Monthly summary */
    public record GetMonthlySummaryQuery(int Month, int Year) : IRequest<OperationResult<List<PnLRowResult>>>;

    internal class GetMonthlySummaryQueryHandler : IRequestHandler<GetMonthlySummaryQuery, OperationResult<List<PnLRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetMonthlySummaryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PnLRowResult>>> Handle(GetMonthlySummaryQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetMonthlySummaryAsync(r.Month, r.Year);
            if (res.Code != 200) return OperationResult<List<PnLRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PnLRowResult>>.SuccessResult(_m.Map<List<PnLRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-04 (b): Annual summary */
    public record GetAnnualSummaryQuery(int AcademicYearId) : IRequest<OperationResult<List<PnLRowResult>>>;

    internal class GetAnnualSummaryQueryHandler : IRequestHandler<GetAnnualSummaryQuery, OperationResult<List<PnLRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetAnnualSummaryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PnLRowResult>>> Handle(GetAnnualSummaryQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetAnnualSummaryAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<PnLRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PnLRowResult>>.SuccessResult(_m.Map<List<PnLRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-04 (c): Category-wise expense */
    public record GetCategoryWiseExpenseQuery(DateTime FromDate, DateTime ToDate) : IRequest<OperationResult<List<CategoryAmountResult>>>;

    internal class GetCategoryWiseExpenseQueryHandler : IRequestHandler<GetCategoryWiseExpenseQuery, OperationResult<List<CategoryAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetCategoryWiseExpenseQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<CategoryAmountResult>>> Handle(GetCategoryWiseExpenseQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetCategoryWiseExpenseAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<CategoryAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CategoryAmountResult>>.SuccessResult(_m.Map<List<CategoryAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-04 (d): Fee collection vs target */
    public record GetFeeCollectionVsTargetQuery(int AcademicYearId) : IRequest<OperationResult<List<FeeCollectionVsTargetResult>>>;

    public class FeeCollectionVsTargetResult
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Target { get; set; }
        public decimal Collected { get; set; }
        public decimal AchievementPercent { get; set; }
    }

    internal class GetFeeCollectionVsTargetQueryHandler : IRequestHandler<GetFeeCollectionVsTargetQuery, OperationResult<List<FeeCollectionVsTargetResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetFeeCollectionVsTargetQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<FeeCollectionVsTargetResult>>> Handle(GetFeeCollectionVsTargetQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetFeeCollectionVsTargetAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<FeeCollectionVsTargetResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<FeeCollectionVsTargetResult>>.SuccessResult(_m.Map<List<FeeCollectionVsTargetResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
