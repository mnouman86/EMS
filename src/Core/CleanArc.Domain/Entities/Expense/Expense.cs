using System;

namespace CleanArc.Domain.Entities.Expense
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? ParentCategoryId { get; set; }    // null = root category
        public decimal? MonthlyBudget { get; set; }   // optional
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ExpenseRecurringTemplate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string? DefaultPaymentMode { get; set; }
        public string? PaidTo { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class Expense
    {
        public int Id { get; set; }
        public string? ExpenseCode { get; set; }       // EXP-YYYY-NNNN
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMode { get; set; }
        public string? ReferenceNo { get; set; }
        public string? PaidTo { get; set; }
        public int? RecordedBy { get; set; }
        public string? AttachmentPath { get; set; }
        public int? LinkedPurchaseId { get; set; }     // INV-02 link
        public int? LinkedPayrollEntryId { get; set; } // EXP-05 link
        public int? AcademicYearId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class PayrollRun
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string? Status { get; set; }            // Draft / Confirmed
        public decimal TotalGross { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalNet { get; set; }
        public int? ConfirmedByUserId { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class PayrollEntry
    {
        public int Id { get; set; }
        public int PayrollRunId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? Designation { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal FixedDeductions { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal FineDeduction { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal Gross { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPayable { get; set; }
        public string? SalarySlipNo { get; set; }      // SAL-YYYY-MM-EMPID
        public int? LinkedExpenseId { get; set; }      // set on Confirm
        public string? Notes { get; set; }
    }

    /* ---------- Query projections ---------- */

    public class BudgetVsActualRow
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal? Budget { get; set; }
        public decimal Spent { get; set; }
        public decimal? Remaining { get; set; }
        public decimal? PercentUsed { get; set; }
        public string? AlertLevel { get; set; }        // Green / Amber / Red
    }
}
