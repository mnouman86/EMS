using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Finance.Queries.IncomeQueries
{
    /* FIN-01: Income summary KPIs */
    public record GetIncomeSummaryQuery(int AcademicYearId) : IRequest<OperationResult<IncomeSummaryResult>>;

    public class IncomeSummaryResult
    {
        public decimal RevenueThisMonth { get; set; }
        public decimal RevenueThisYear { get; set; }
        public decimal RevenueLastMonth { get; set; }
        public decimal MonthOverMonthPercent { get; set; }
        public decimal CollectionRatePercent { get; set; }
    }

    internal class GetIncomeSummaryQueryHandler : IRequestHandler<GetIncomeSummaryQuery, OperationResult<IncomeSummaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIncomeSummaryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<IncomeSummaryResult>> Handle(GetIncomeSummaryQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetIncomeSummaryAsync(r.AcademicYearId);
            if (res.Code != 200 || res.Data == null) return OperationResult<IncomeSummaryResult>.FailureResult(res.Message ?? "No data", res.Code);
            return OperationResult<IncomeSummaryResult>.SuccessResult(_m.Map<IncomeSummaryResult>(res.Data));
        }
    }

    /* FIN-01: Income by month chart */
    public record GetIncomeByMonthQuery(int AcademicYearId) : IRequest<OperationResult<List<MonthAmountResult>>>;

    public class MonthAmountResult
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public decimal? Invoiced { get; set; }
    }

    internal class GetIncomeByMonthQueryHandler : IRequestHandler<GetIncomeByMonthQuery, OperationResult<List<MonthAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIncomeByMonthQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<MonthAmountResult>>> Handle(GetIncomeByMonthQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetIncomeByMonthAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<MonthAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<MonthAmountResult>>.SuccessResult(_m.Map<List<MonthAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FIN-01: Income breakdown — by class or by fee type (two queries share a row shape) */
    public record GetIncomeByClassQuery(int AcademicYearId) : IRequest<OperationResult<List<CategoryAmountResult>>>;
    public record GetIncomeByFeeTypeQuery(int AcademicYearId) : IRequest<OperationResult<List<CategoryAmountResult>>>;

    public class CategoryAmountResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }

    internal class GetIncomeByClassQueryHandler : IRequestHandler<GetIncomeByClassQuery, OperationResult<List<CategoryAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIncomeByClassQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<CategoryAmountResult>>> Handle(GetIncomeByClassQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetIncomeByClassAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<CategoryAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CategoryAmountResult>>.SuccessResult(_m.Map<List<CategoryAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    internal class GetIncomeByFeeTypeQueryHandler : IRequestHandler<GetIncomeByFeeTypeQuery, OperationResult<List<CategoryAmountResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIncomeByFeeTypeQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<CategoryAmountResult>>> Handle(GetIncomeByFeeTypeQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetIncomeByFeeTypeAsync(r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<CategoryAmountResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CategoryAmountResult>>.SuccessResult(_m.Map<List<CategoryAmountResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
