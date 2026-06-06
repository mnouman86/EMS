using System;

namespace CleanArc.Application.Models.Expense
{
    /* ---------- Categories ---------- */

    public class UpsertExpenseCategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentCategoryId { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }

    /* ---------- Recurring template ---------- */

    public class UpsertRecurringTemplateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string DefaultPaymentMode { get; set; }
        public string PaidTo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class GenerateRecurringExpensesDTO
    {
        public DateTime ExpenseDate { get; set; }
        public int? AcademicYearId { get; set; }
        public int RecordedBy { get; set; }
    }

    /* ---------- Expense entry ---------- */

    public class RecordExpenseDTO
    {
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public string PaidTo { get; set; }
        public string AttachmentPath { get; set; }
        public int? AcademicYearId { get; set; }
        public int RecordedBy { get; set; }
    }

    /* ---------- Payroll ---------- */

    public class StartPayrollRunDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AdjustPayrollEntryDTO
    {
        public int PayrollEntryId { get; set; }
        public decimal? FineDeduction { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string Notes { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class ConfirmPayrollRunDTO
    {
        public int PayrollRunId { get; set; }
        public int ConfirmedByUserId { get; set; }
    }
}
