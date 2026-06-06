using CleanArc.Application.Common;
using CleanArc.Application.Models.Expense;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Expense;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IExpenseRepository
    {
        /* Categories (EXP-01) */
        Task<ResponseEntity> UpsertCategoryAsync(UpsertExpenseCategoryDTO dto);
        Task<ResponseEntity> DeleteCategoryAsync(DeleteRequest req, int? updatedBy);
        Task<ListResponseWrapper<ExpenseCategory>> GetCategoriesAsync(bool includeInactive);

        /* Recurring templates (EXP-02) */
        Task<ResponseEntity> UpsertRecurringTemplateAsync(UpsertRecurringTemplateDTO dto);
        Task<ListResponseWrapper<ExpenseRecurringTemplate>> GetRecurringTemplatesAsync();
        Task<ResponseEntity> GenerateRecurringExpensesAsync(GenerateRecurringExpensesDTO dto);

        /* Expenses (EXP-02 / EXP-03) */
        Task<ResponseEntity> RecordExpenseAsync(RecordExpenseDTO dto);
        Task<ResponseEntity> DeleteExpenseAsync(DeleteRequest req, int? updatedBy);
        Task<ListResponseWrapper<Expense>> GetExpensesAsync(SearchRequest req);

        /* Budget monitoring (EXP-04) */
        Task<ListResponseWrapper<BudgetVsActualRow>> GetBudgetMonitoringAsync(int month, int year);

        /* Payroll (EXP-05) */
        Task<ResponseEntity> StartPayrollRunAsync(StartPayrollRunDTO dto);
        Task<ResponseEntity> AdjustPayrollEntryAsync(AdjustPayrollEntryDTO dto);
        Task<ResponseEntity> ConfirmPayrollRunAsync(ConfirmPayrollRunDTO dto);
        Task<ListResponseWrapper<PayrollRun>> GetPayrollRunsAsync(int? year);
        Task<ListResponseWrapper<PayrollEntry>> GetPayrollEntriesAsync(int payrollRunId);
        Task<SingleResponseWrapper<PayrollEntry>> GetPayrollEntryAsync(int payrollEntryId);
    }
}
