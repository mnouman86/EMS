using System;

namespace CleanArc.Domain.Entities.Finance
{
    public class OpeningCashBalance
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
        public decimal OpeningAmount { get; set; }
        public DateTime AsOfDate { get; set; }
        public int? SetByUserId { get; set; }
        public DateTime? SetAt { get; set; }
    }

    /* ---------- Query projections ---------- */

    public class IncomeDashboardSummary
    {
        public decimal RevenueThisMonth { get; set; }
        public decimal RevenueThisYear { get; set; }
        public decimal RevenueLastMonth { get; set; }
        public decimal MonthOverMonthPercent { get; set; }
        public decimal CollectionRatePercent { get; set; }
    }

    public class ExpenseDashboardSummary
    {
        public decimal ExpensesThisMonth { get; set; }
        public decimal ExpensesThisYear { get; set; }
        public string? LargestCategoryName { get; set; }
        public decimal LargestCategoryAmount { get; set; }
    }

    public class MonthAmountRow
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public decimal? Invoiced { get; set; }  // for income chart only
    }

    public class CategoryAmountRow
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
    }

    public class PnLRow
    {
        public string? Section { get; set; }     // Income / Expense / Net
        public string? Line { get; set; }
        public decimal Amount { get; set; }
    }

    public class FeeCollectionVsTargetRow
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Target { get; set; }
        public decimal Collected { get; set; }
        public decimal AchievementPercent { get; set; }
    }

    public class CashFlowLedgerRow
    {
        public DateTime Date { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public decimal MoneyIn { get; set; }
        public decimal MoneyOut { get; set; }
        public decimal RunningBalance { get; set; }
        public string? Section { get; set; }    // Confirmed / Provisional (cheques pending)
    }
}
