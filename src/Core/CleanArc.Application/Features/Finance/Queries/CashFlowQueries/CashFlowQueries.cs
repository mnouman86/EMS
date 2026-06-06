using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Finance.Queries.CashFlowQueries
{
    /* FIN-05: Opening cash balance (read) */
    public record GetOpeningCashBalanceQuery(int AcademicYearId) : IRequest<OperationResult<OpeningCashBalanceResult>>;

    public class OpeningCashBalanceResult
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
        public decimal OpeningAmount { get; set; }
        public DateTime AsOfDate { get; set; }
    }

    internal class GetOpeningCashBalanceQueryHandler : IRequestHandler<GetOpeningCashBalanceQuery, OperationResult<OpeningCashBalanceResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetOpeningCashBalanceQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<OpeningCashBalanceResult>> Handle(GetOpeningCashBalanceQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetOpeningCashBalanceAsync(r.AcademicYearId);
            if (res.Code != 200 || res.Data == null)
                return OperationResult<OpeningCashBalanceResult>.SuccessResult(new OpeningCashBalanceResult { AcademicYearId = r.AcademicYearId });
            return OperationResult<OpeningCashBalanceResult>.SuccessResult(_m.Map<OpeningCashBalanceResult>(res.Data));
        }
    }

    /* FIN-05: Cash flow ledger */
    public record GetCashFlowLedgerQuery(DateTime FromDate, DateTime ToDate, int AcademicYearId)
        : IRequest<OperationResult<List<CashFlowLedgerResult>>>;

    public class CashFlowLedgerResult
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal MoneyIn { get; set; }
        public decimal MoneyOut { get; set; }
        public decimal RunningBalance { get; set; }
        public string Section { get; set; }
    }

    internal class GetCashFlowLedgerQueryHandler : IRequestHandler<GetCashFlowLedgerQuery, OperationResult<List<CashFlowLedgerResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetCashFlowLedgerQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<CashFlowLedgerResult>>> Handle(GetCashFlowLedgerQuery r, CancellationToken ct)
        {
            var res = await _u.FinanceRepository.GetCashFlowLedgerAsync(r.FromDate, r.ToDate, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<CashFlowLedgerResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<CashFlowLedgerResult>>.SuccessResult(_m.Map<List<CashFlowLedgerResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
