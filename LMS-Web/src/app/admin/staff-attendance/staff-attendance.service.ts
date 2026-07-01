import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ApiResult } from '../../core/models/api-result';
import {
  StaffAttendanceRow, StaffAttendanceOverviewRow, StaffCheckInPayload, StaffCheckOutPayload
} from './staff-attendance.models';

/**
 * Service holds today's row as a shared signal so the panel + any consumer
 * screen never trigger a duplicate GET during a single session.
 */
@Injectable({ providedIn: 'root' })
export class StaffAttendanceService {
  private api = inject(ApiService);

  /* Shared state — one signal, one fetch per session. */
  readonly today = signal<StaffAttendanceRow | null>(null);
  private todayFetched = false;
  readonly hasCheckedIn = computed(() => !!this.today());
  readonly hasCheckedOut = computed(() => !!this.today()?.checkOutTime);

  /** Idempotent: only calls the API once per session unless force = true. */
  loadToday(force = false): Observable<ApiResult<StaffAttendanceRow | null>> {
    if (this.todayFetched && !force) {
      return of({ data: this.today(), message: 'cached', statusCode: 200, isSuccess: true, totalCount: 0 } as ApiResult<StaffAttendanceRow | null>);
    }
    return this.api.post<StaffAttendanceRow | null>('StaffAttendance/StaffAttendanceGetMyToday', {})
      .pipe(tap(res => { this.today.set(res.data ?? null); this.todayFetched = true; }));
  }

  checkIn(payload: StaffCheckInPayload) {
    return this.api.post<{ recordID?: string | number }>('StaffAttendance/StaffAttendanceCheckIn', payload);
  }

  checkOut(payload: StaffCheckOutPayload) {
    return this.api.post<{ recordID?: string | number }>('StaffAttendance/StaffAttendanceCheckOut', payload);
  }

  history(fromDate: string, toDate: string): Observable<ApiResult<StaffAttendanceRow[]>> {
    return this.api.post<StaffAttendanceRow[]>('StaffAttendance/StaffAttendanceGetMyHistory', { fromDate, toDate });
  }

  overview(fromDate: string, toDate: string, userId: number | null = null): Observable<ApiResult<StaffAttendanceOverviewRow[]>> {
    return this.api.post<StaffAttendanceOverviewRow[]>('StaffAttendance/StaffAttendanceGetOverview', { fromDate, toDate, userId });
  }

  /** Called by /login post-success to reset cache for the next user. */
  clear(): void { this.today.set(null); this.todayFetched = false; }
}
