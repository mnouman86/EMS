import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ApiService } from '../core/services/api.service';
import { ApiResult } from '../core/models/api-result';
import { StudentDetail } from '../students/student.models';
import { StudentLedgerEntry } from '../fees/fee.models';
import { ResultSession, StudentResultCard } from '../results/result.models';
import { ParentChild, StudentPayment } from './parent.models';

/**
 * Parent portal data + shared child-selection state. All child-scoped calls are
 * ownership-enforced server-side (the API returns 403 for an unlinked student),
 * so the UI trusts whatever the switcher offers.
 */
@Injectable({ providedIn: 'root' })
export class ParentService {
  private api = inject(ApiService);

  readonly children = signal<ParentChild[]>([]);
  readonly selectedChildId = signal<number | null>(null);
  readonly selectedChild = computed(
    () => this.children().find(c => c.studentId === this.selectedChildId()) ?? null
  );
  private loaded = false;

  /** Loads the parent's children once (cached). Auto-selects the first child. */
  loadChildren(force = false): Observable<ParentChild[]> {
    if (this.loaded && !force) return of(this.children());
    return this.api.post<ParentChild[]>('Parent/ParentGetMyChildren', {}).pipe(
      map(r => r.data ?? []),
      tap(list => {
        this.children.set(list);
        this.loaded = true;
        if (list.length && !list.some(c => c.studentId === this.selectedChildId())) {
          this.selectedChildId.set(list[0].studentId);
        }
      })
    );
  }

  selectChild(studentId: number): void {
    this.selectedChildId.set(studentId);
  }

  getChildProfile(studentId: number): Observable<ApiResult<StudentDetail>> {
    return this.api.post<StudentDetail>('Parent/ParentGetChildProfile', { studentId });
  }

  getChildFees(studentId: number): Observable<ApiResult<StudentLedgerEntry[]>> {
    return this.api.post<StudentLedgerEntry[]>('Parent/ParentGetChildFees', { studentId });
  }

  getChildPayments(studentId: number): Observable<ApiResult<StudentPayment[]>> {
    return this.api.post<StudentPayment[]>('Parent/ParentGetChildPayments', { studentId });
  }

  getReceiptPdf(studentId: number, paymentId: number): Observable<Blob> {
    return this.api.postBlob('Parent/ParentGetChildReceiptPdf', { studentId, paymentId });
  }

  getResultSessions(): Observable<ApiResult<ResultSession[]>> {
    return this.api.post<ResultSession[]>('Parent/ParentGetResultSessions', {});
  }

  getChildResultCard(studentId: number, resultSessionId: number): Observable<ApiResult<StudentResultCard>> {
    return this.api.post<StudentResultCard>('Parent/ParentGetChildResultCard', { studentId, resultSessionId });
  }

  clear(): void {
    this.children.set([]);
    this.selectedChildId.set(null);
    this.loaded = false;
  }

  /* ---------- Admin: manage parent↔student links ---------- */
  getChildrenOf(userId: number): Observable<ApiResult<ParentChild[]>> {
    return this.api.post<ParentChild[]>('Parent/ParentGetChildrenOf', { userId });
  }
  linkChild(parentUserId: number, studentId: number, relationship: string): Observable<ApiResult<unknown>> {
    return this.api.post('Parent/ParentLinkChild', { parentUserId, studentId, relationship });
  }
  unlinkChild(parentUserId: number, studentId: number): Observable<ApiResult<unknown>> {
    return this.api.post('Parent/ParentUnlinkChild', { parentUserId, studentId });
  }
}
