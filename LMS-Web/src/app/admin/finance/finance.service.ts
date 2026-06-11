import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ApiResult } from '../../core/models/api-result';
import {
  IncomeSummary, MonthAmount, CategoryAmount, ExpenseSummary, PnLRow,
  FeeCollectionVsTarget, OpeningCashBalance, SetOpeningCashBalance, CashFlowLedgerRow
} from './finance.models';

@Injectable({ providedIn: 'root' })
export class FinanceService {
  private api = inject(ApiService);

  /* FIN-01: income */
  getIncomeSummary(academicYearId: number): Observable<ApiResult<IncomeSummary>> {
    return this.api.post<IncomeSummary>('Finance/FinanceGetIncomeSummary', { academicYearId });
  }
  getIncomeByMonth(academicYearId: number): Observable<ApiResult<MonthAmount[]>> {
    return this.api.post<MonthAmount[]>('Finance/FinanceGetIncomeByMonth', { academicYearId });
  }
  getIncomeByClass(academicYearId: number): Observable<ApiResult<CategoryAmount[]>> {
    return this.api.post<CategoryAmount[]>('Finance/FinanceGetIncomeByClass', { academicYearId });
  }
  getIncomeByFeeType(academicYearId: number): Observable<ApiResult<CategoryAmount[]>> {
    return this.api.post<CategoryAmount[]>('Finance/FinanceGetIncomeByFeeType', { academicYearId });
  }

  /* FIN-02: expense */
  getExpenseSummary(academicYearId: number): Observable<ApiResult<ExpenseSummary>> {
    return this.api.post<ExpenseSummary>('Finance/FinanceGetExpenseSummary', { academicYearId });
  }
  getExpenseByMonth(academicYearId: number): Observable<ApiResult<MonthAmount[]>> {
    return this.api.post<MonthAmount[]>('Finance/FinanceGetExpenseByMonth', { academicYearId });
  }
  getExpenseByCategory(academicYearId: number): Observable<ApiResult<CategoryAmount[]>> {
    return this.api.post<CategoryAmount[]>('Finance/FinanceGetExpenseByCategory', { academicYearId });
  }

  /* FIN-03: P&L */
  getPnL(fromDate: string, toDate: string): Observable<ApiResult<PnLRow[]>> {
    return this.api.post<PnLRow[]>('Finance/FinanceGetPnL', { fromDate, toDate });
  }
  getPnLPdf(fromDate: string, toDate: string, periodLabel: string | null = null, generatedBy: string | null = null): Observable<Blob> {
    return this.api.postBlob('Finance/FinanceGetPnLPdf', { fromDate, toDate, periodLabel, generatedBy });
  }

  /* FIN-04: reports */
  getMonthlySummary(month: number, year: number): Observable<ApiResult<PnLRow[]>> {
    return this.api.post<PnLRow[]>('Finance/FinanceGetMonthlySummary', { month, year });
  }
  getAnnualSummary(academicYearId: number): Observable<ApiResult<PnLRow[]>> {
    return this.api.post<PnLRow[]>('Finance/FinanceGetAnnualSummary', { academicYearId });
  }
  getCategoryWiseExpense(fromDate: string, toDate: string): Observable<ApiResult<CategoryAmount[]>> {
    return this.api.post<CategoryAmount[]>('Finance/FinanceGetCategoryWiseExpense', { fromDate, toDate });
  }
  getFeeCollectionVsTarget(academicYearId: number): Observable<ApiResult<FeeCollectionVsTarget[]>> {
    return this.api.post<FeeCollectionVsTarget[]>('Finance/FinanceGetFeeCollectionVsTarget', { academicYearId });
  }

  /* FIN-05: cash flow */
  setOpeningBalance(dto: SetOpeningCashBalance): Observable<ApiResult<unknown>> {
    return this.api.post('Finance/FinanceSetOpeningBalance', dto);
  }
  getOpeningBalance(academicYearId: number): Observable<ApiResult<OpeningCashBalance>> {
    return this.api.post<OpeningCashBalance>('Finance/FinanceGetOpeningBalance', { academicYearId });
  }
  getCashFlowLedger(fromDate: string, toDate: string, academicYearId: number): Observable<ApiResult<CashFlowLedgerRow[]>> {
    return this.api.post<CashFlowLedgerRow[]>('Finance/FinanceGetCashFlowLedger', { fromDate, toDate, academicYearId });
  }
}
