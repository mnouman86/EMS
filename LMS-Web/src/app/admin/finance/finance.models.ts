/* ---------- FIN-01: Income ---------- */
export interface IncomeSummary {
  revenueThisMonth: number;
  revenueThisYear: number;
  revenueLastMonth: number;
  monthOverMonthPercent: number;
  collectionRatePercent: number;
}

export interface MonthAmount {
  month: number;
  year: number;
  amount: number;
  invoiced?: number | null;
}

export interface CategoryAmount {
  id: number;
  name: string;
  amount: number;
}

/* ---------- FIN-02: Expense ---------- */
export interface ExpenseSummary {
  expensesThisMonth: number;
  expensesThisYear: number;
  largestCategoryName: string;
  largestCategoryAmount: number;
}

/* ---------- FIN-03 / FIN-04: P&L rows ---------- */
export interface PnLRow {
  section: string; // Income / Expense
  line: string;
  amount: number;
}

/* ---------- FIN-04: Fee collection vs target ---------- */
export interface FeeCollectionVsTarget {
  month: number;
  year: number;
  target: number;
  collected: number;
  achievementPercent: number;
}

/* ---------- FIN-05: Cash flow ---------- */
export interface OpeningCashBalance {
  id: number;
  academicYearId: number;
  openingAmount: number;
  asOfDate: string;
}

export interface SetOpeningCashBalance {
  academicYearId: number;
  openingAmount: number;
  asOfDate: string;
}

export interface CashFlowLedgerRow {
  date: string;
  reference: string;
  description: string;
  moneyIn: number;
  moneyOut: number;
  runningBalance: number;
  section: string;
}
