import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { SearchRequest, defaultSearch } from '../core/models/search-request';
import { ApiResult } from '../core/models/api-result';
import {
  FeeType, UpsertFeeType, FeeStructureRow, UpsertFeeAmount, ConfigureFeeCalendar,
  GenerateInvoices, InvoicePreviewRow, CancelInvoice,
  RecordPayment, ClearCheque, ApplyAdvance, ReversePayment,
  StudentLedgerEntry, CollectionSummary, CollectionByClass, CollectionByDay,
  PendingFeeRow, AgeingRow, AdvanceBalance, Concession, GrantConcession, RevokeConcession,
  SendReminders, SendRemindersResult, CarryForwardArrears, WriteOffArrear, FeeArrearRow, ParentFeeSummary,
  FeeClassBoardRow
} from './fee.models';

@Injectable({ providedIn: 'root' })
export class FeeService {
  private api = inject(ApiService);

  /* FEE-01: configuration */
  getTypes(search: SearchRequest = defaultSearch()): Observable<ApiResult<FeeType[]>> {
    return this.api.post<FeeType[]>('Fee/FeeGetTypes', { searchRequest: search });
  }
  upsertType(dto: UpsertFeeType): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeUpsertType', dto);
  }
  deleteType(id: number, forceHard = false): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeDeleteType', { deleteRequest: { selectedIds: String(id), isDeleted: true, forceHard } });
  }
  getStructureForClass(schoolClassId: number, academicYearId: number): Observable<ApiResult<FeeStructureRow[]>> {
    return this.api.post<FeeStructureRow[]>('Fee/FeeGetStructureForClass', { schoolClassId, academicYearId });
  }
  upsertAmount(dto: UpsertFeeAmount): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeUpsertAmount', dto);
  }
  configureCalendar(dto: ConfigureFeeCalendar): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeConfigureCalendar', dto);
  }

  /* FEE-02: invoices */
  generateInvoices(dto: GenerateInvoices): Observable<ApiResult<InvoicePreviewRow[]>> {
    return this.api.post<InvoicePreviewRow[]>('Fee/FeeGenerateMonthlyInvoices', dto);
  }
  cancelInvoice(dto: CancelInvoice): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeCancelInvoice', dto);
  }
  /** Download the monthly invoice as PDF (with arrears + any override history). */
  getInvoicePdf(invoiceId: number): Observable<Blob> {
    return this.api.postBlob('Fee/FeeGetInvoicePdf', { invoiceId });
  }

  /* FEE-03 / 08 / 11: payments */
  recordPayment(dto: RecordPayment): Observable<ApiResult<{ recordID?: number }>> {
    return this.api.post('Fee/FeeRecordPayment', dto);
  }
  clearCheque(dto: ClearCheque): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeClearCheque', dto);
  }
  applyAdvance(dto: ApplyAdvance): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeApplyAdvance', dto);
  }
  reversePayment(dto: ReversePayment): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeReversePayment', dto);
  }

  /* FEE-04: receipt PDF (binary) */
  getReceiptPdf(paymentId: number, isDuplicate = false): Observable<Blob> {
    return this.api.postBlob('Fee/FeeGetReceiptPdf', { paymentId, isDuplicate });
  }

  /* FEE-04 / 13: ledger */
  getStudentLedger(studentId: number, academicYearId: number | null = null): Observable<ApiResult<StudentLedgerEntry[]>> {
    return this.api.post<StudentLedgerEntry[]>('Fee/FeeGetStudentLedger', { studentId, academicYearId });
  }

  /* FEE-05: dashboard */
  getCollectionSummary(fromDate: string, toDate: string, classId: number | null = null): Observable<ApiResult<CollectionSummary>> {
    return this.api.post<CollectionSummary>('Fee/FeeGetCollectionSummary', { fromDate, toDate, classId });
  }
  getCollectionByClass(fromDate: string, toDate: string): Observable<ApiResult<CollectionByClass[]>> {
    return this.api.post<CollectionByClass[]>('Fee/FeeGetCollectionByClass', { fromDate, toDate });
  }
  getCollectionByDay(fromDate: string, toDate: string): Observable<ApiResult<CollectionByDay[]>> {
    return this.api.post<CollectionByDay[]>('Fee/FeeGetCollectionByDay', { fromDate, toDate });
  }

  /* FEE-06 / 07 / 14: reports */
  getPendingList(classId: number | null, minOutstanding: number | null, minDaysOverdue: number | null): Observable<ApiResult<PendingFeeRow[]>> {
    return this.api.post<PendingFeeRow[]>('Fee/FeeGetPendingList', { classId, minOutstanding, minDaysOverdue });
  }
  getMonthlyNonSubmitted(month: number, year: number, classId: number | null): Observable<ApiResult<PendingFeeRow[]>> {
    return this.api.post<PendingFeeRow[]>('Fee/FeeGetMonthlyNonSubmitted', { month, year, classId });
  }
  getOutstandingAgeing(classId: number | null): Observable<ApiResult<AgeingRow[]>> {
    return this.api.post<AgeingRow[]>('Fee/FeeGetOutstandingAgeing', { classId });
  }

  /* FEE-08 / 09: advance + concessions */
  getAdvanceBalance(studentId: number): Observable<ApiResult<AdvanceBalance>> {
    return this.api.post<AdvanceBalance>('Fee/FeeGetAdvanceBalance', { studentId });
  }
  getConcessions(studentId: number | null = null): Observable<ApiResult<Concession[]>> {
    return this.api.post<Concession[]>('Fee/FeeGetConcessions', { studentId });
  }
  grantConcession(dto: GrantConcession): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeGrantConcession', dto);
  }
  revokeConcession(dto: RevokeConcession): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeRevokeConcession', dto);
  }

  /* FEE-10: reminders */
  sendReminders(dto: SendReminders): Observable<ApiResult<SendRemindersResult>> {
    return this.api.post<SendRemindersResult>('Fee/FeeSendReminders', dto);
  }

  /* FEE-12: arrears */
  carryForwardArrears(dto: CarryForwardArrears): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeCarryForwardArrears', dto);
  }
  writeOffArrear(dto: WriteOffArrear): Observable<ApiResult<unknown>> {
    return this.api.post('Fee/FeeWriteOffArrear', dto);
  }
  getArrears(studentId: number | null = null, includeWrittenOff = false): Observable<ApiResult<FeeArrearRow[]>> {
    return this.api.post<FeeArrearRow[]>('Fee/FeeGetArrears', { studentId, includeWrittenOff });
  }

  /* FEE-13: parent self-service (anonymous) */
  parentSearch(studentCode: string, secondFactor: string): Observable<ApiResult<ParentFeeSummary>> {
    return this.api.post<ParentFeeSummary>('Fee/FeeParentSearch', { studentCode, secondFactor });
  }

  /* Class Fee Board — per-student fee state for one class.
     period (year/month) is optional; pass nulls for lifetime totals. */
  getClassBoard(classId: number, periodYear: number | null = null, periodMonth: number | null = null,
                academicYearId: number | null = null): Observable<ApiResult<FeeClassBoardRow[]>> {
    return this.api.post<FeeClassBoardRow[]>('Fee/FeeGetClassBoard', {
      classId, academicYearId, periodYear, periodMonth
    });
  }
}
