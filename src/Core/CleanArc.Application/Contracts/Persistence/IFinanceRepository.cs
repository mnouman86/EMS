using CleanArc.Application.Common;
using CleanArc.Application.Models.Finance;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Finance;
using System;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IFinanceRepository
    {
        /* FIN-01 Income dashboard */
        Task<SingleResponseWrapper<IncomeDashboardSummary>> GetIncomeSummaryAsync(int academicYearId);
        Task<ListResponseWrapper<MonthAmountRow>> GetIncomeByMonthAsync(int academicYearId);
        Task<ListResponseWrapper<CategoryAmountRow>> GetIncomeByClassAsync(int academicYearId);
        Task<ListResponseWrapper<CategoryAmountRow>> GetIncomeByFeeTypeAsync(int academicYearId);

        /* FIN-02 Expense dashboard */
        Task<SingleResponseWrapper<ExpenseDashboardSummary>> GetExpenseSummaryAsync(int academicYearId);
        Task<ListResponseWrapper<MonthAmountRow>> GetExpenseByMonthAsync(int academicYearId);
        Task<ListResponseWrapper<CategoryAmountRow>> GetExpenseByCategoryAsync(int academicYearId);

        /* FIN-03 P&L */
        Task<ListResponseWrapper<PnLRow>> GetPnLAsync(DateTime fromDate, DateTime toDate);

        /* FIN-04 Reports */
        Task<ListResponseWrapper<PnLRow>> GetMonthlySummaryAsync(int month, int year);
        Task<ListResponseWrapper<PnLRow>> GetAnnualSummaryAsync(int academicYearId);
        Task<ListResponseWrapper<CategoryAmountRow>> GetCategoryWiseExpenseAsync(DateTime fromDate, DateTime toDate);
        Task<ListResponseWrapper<FeeCollectionVsTargetRow>> GetFeeCollectionVsTargetAsync(int academicYearId);

        /* FIN-05 Cash flow */
        Task<ResponseEntity> SetOpeningCashBalanceAsync(SetOpeningCashBalanceDTO dto);
        Task<SingleResponseWrapper<OpeningCashBalance>> GetOpeningCashBalanceAsync(int academicYearId);
        Task<ListResponseWrapper<CashFlowLedgerRow>> GetCashFlowLedgerAsync(DateTime fromDate, DateTime toDate, int academicYearId);
    }
}
