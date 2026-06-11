import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import {
  ExpenseCategory, UpsertExpenseCategory, RecurringTemplate, UpsertRecurringTemplate, GenerateRecurring,
  RecordExpense, ExpenseRow, BudgetMonitoringRow,
  PayrollRun, PayrollEntry, StartPayrollRun, AdjustPayrollEntry
} from './expense.models';

@Injectable({ providedIn: 'root' })
export class ExpenseService {
  private api = inject(ApiService);

  /* EXP-01: categories */
  getCategories(includeInactive = false): Observable<ApiResult<ExpenseCategory[]>> {
    return this.api.post<ExpenseCategory[]>('Expense/ExpenseGetCategories', { includeInactive });
  }
  upsertCategory(dto: UpsertExpenseCategory): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseUpsertCategory', dto);
  }
  deleteCategory(id: number, forceHard = false): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseDeleteCategory', { deleteRequest: { selectedIds: String(id), isDeleted: true, forceHard } });
  }

  /* EXP-02: recurring + record */
  getRecurringTemplates(): Observable<ApiResult<RecurringTemplate[]>> {
    return this.api.post<RecurringTemplate[]>('Expense/ExpenseGetRecurringTemplates', {});
  }
  upsertRecurringTemplate(dto: UpsertRecurringTemplate): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseUpsertRecurringTemplate', dto);
  }
  generateRecurring(dto: GenerateRecurring): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseGenerateRecurring', dto);
  }
  recordExpense(dto: RecordExpense): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseRecord', dto);
  }
  deleteExpense(id: number, forceHard = false): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseDelete', { deleteRequest: { selectedIds: String(id), isDeleted: true, forceHard } });
  }

  /* EXP-03: list */
  getExpenses(search: SearchRequest = defaultSearch()): Observable<ApiResult<ExpenseRow[]>> {
    return this.api.post<ExpenseRow[]>('Expense/ExpenseGetAll', { searchRequest: search });
  }

  /* EXP-04: budget monitoring */
  getBudgetMonitoring(month: number, year: number): Observable<ApiResult<BudgetMonitoringRow[]>> {
    return this.api.post<BudgetMonitoringRow[]>('Expense/ExpenseGetBudgetMonitoring', { month, year });
  }

  /* EXP-05: payroll */
  startPayroll(dto: StartPayrollRun): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseStartPayroll', dto);
  }
  adjustPayrollEntry(dto: AdjustPayrollEntry): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseAdjustPayrollEntry', dto);
  }
  confirmPayroll(payrollRunId: number): Observable<ApiResult<unknown>> {
    return this.api.post('Expense/ExpenseConfirmPayroll', { payrollRunId });
  }
  getPayrollRuns(year: number | null = null): Observable<ApiResult<PayrollRun[]>> {
    return this.api.post<PayrollRun[]>('Expense/ExpenseGetPayrollRuns', { year });
  }
  getPayrollEntries(payrollRunId: number): Observable<ApiResult<PayrollEntry[]>> {
    return this.api.post<PayrollEntry[]>('Expense/ExpenseGetPayrollEntries', { payrollRunId });
  }
  getSalarySlipPdf(payrollEntryId: number, includeBankDetails = false): Observable<Blob> {
    return this.api.postBlob('Expense/ExpenseGetSalarySlipPdf', { payrollEntryId, includeBankDetails });
  }
}
