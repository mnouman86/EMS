import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { ApiResult } from '../core/models/api-result';
import {
  AttendanceGridRow, AttendancePeriod, AttendanceSummary, BulkSaveAttendancePayload
} from './attendance.models';

@Injectable({ providedIn: 'root' })
export class AttendanceService {
  private api = inject(ApiService);

  /* ---------- Staff (admin / principal / teacher) ---------- */
  getClassGrid(schoolClassId: number, periodYear: number, periodMonth: number): Observable<ApiResult<AttendanceGridRow[]>> {
    return this.api.post<AttendanceGridRow[]>('Attendance/AttendanceGetClassGrid', { schoolClassId, periodYear, periodMonth });
  }
  bulkSave(payload: BulkSaveAttendancePayload): Observable<ApiResult<unknown>> {
    return this.api.post('Attendance/AttendanceBulkSave', payload);
  }
  getStudentHistory(studentId: number, academicYearId?: number | null): Observable<ApiResult<AttendancePeriod[]>> {
    return this.api.post<AttendancePeriod[]>('Attendance/AttendanceGetStudentHistory', { studentId, academicYearId });
  }
  getStudentSummary(studentId: number, academicYearId?: number | null): Observable<ApiResult<AttendanceSummary>> {
    return this.api.post<AttendanceSummary>('Attendance/AttendanceGetStudentSummary', { studentId, academicYearId });
  }

  /* ---------- Parent portal (ownership-checked server-side) ---------- */
  getChildHistory(studentId: number, academicYearId?: number | null): Observable<ApiResult<AttendancePeriod[]>> {
    return this.api.post<AttendancePeriod[]>('Parent/ParentGetChildAttendanceHistory', { studentId, academicYearId });
  }
  getChildSummary(studentId: number, academicYearId?: number | null): Observable<ApiResult<AttendanceSummary>> {
    return this.api.post<AttendanceSummary>('Parent/ParentGetChildAttendanceSummary', { studentId, academicYearId });
  }
}
