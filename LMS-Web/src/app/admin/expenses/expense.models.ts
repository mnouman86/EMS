/* ---------- EXP-01: Categories ---------- */
export interface ExpenseCategory {
  id: number;
  name: string;
  code: string;
  parentCategoryId?: number | null;
  monthlyBudget?: number | null;
  isActive: boolean;
}

export interface UpsertExpenseCategory {
  id?: number | null;
  name: string;
  code: string;
  parentCategoryId?: number | null;
  monthlyBudget?: number | null;
  isActive: boolean;
}

/* ---------- EXP-02: Recurring templates ---------- */
export interface RecurringTemplate {
  id: number;
  name: string;
  categoryId: number;
  amount: number;
  defaultPaymentMode: string;
  paidTo: string;
  description: string;
  isActive: boolean;
}

export interface UpsertRecurringTemplate {
  id?: number | null;
  name: string;
  categoryId: number;
  amount: number;
  defaultPaymentMode?: string | null;
  paidTo?: string | null;
  description?: string | null;
  isActive: boolean;
}

export interface GenerateRecurring {
  expenseDate: string;
  academicYearId?: number | null;
}

/* ---------- EXP-02/03: Expenses ---------- */
export interface RecordExpense {
  expenseDate: string;
  categoryId: number;
  description?: string | null;
  amount: number;
  paymentMode?: string | null;
  referenceNo?: string | null;
  paidTo?: string | null;
  attachmentPath?: string | null;
  academicYearId?: number | null;
}

export interface ExpenseRow {
  id: number;
  expenseCode: string;
  expenseDate: string;
  categoryId: number;
  categoryName: string;
  description: string;
  amount: number;
  paymentMode: string;
  referenceNo: string;
  paidTo: string;
  attachmentPath: string;
  linkedPurchaseId?: number | null;
  linkedPayrollEntryId?: number | null;
}

/* ---------- EXP-04: Budget monitoring ---------- */
export interface BudgetMonitoringRow {
  categoryId: number;
  categoryName: string;
  budget?: number | null;
  spent: number;
  remaining?: number | null;
  percentUsed?: number | null;
  alertLevel: string;
}

/* ---------- EXP-05: Payroll ---------- */
export interface PayrollRun {
  id: number;
  month: number;
  year: number;
  status: string; // Draft / Confirmed
  totalGross: number;
  totalDeductions: number;
  totalNet: number;
  confirmedAt?: string | null;
}

export interface PayrollEntry {
  id: number;
  payrollRunId: number;
  employeeId: number;
  employeeCode: string;
  employeeFullName: string;
  designation: string;
  basicSalary: number;
  allowances: number;
  fixedDeductions: number;
  advanceDeduction: number;
  fineDeduction: number;
  otherDeduction: number;
  gross: number;
  totalDeductions: number;
  netPayable: number;
  salarySlipNo: string;
  linkedExpenseId?: number | null;
  notes: string;
}

export interface StartPayrollRun {
  month: number;
  year: number;
}

export interface AdjustPayrollEntry {
  payrollEntryId: number;
  fineDeduction?: number | null;
  otherDeduction?: number | null;
  notes?: string | null;
}
