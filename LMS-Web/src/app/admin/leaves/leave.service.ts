import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService } from '../../core/services/api.service';
import { ApiResult } from '../../core/models/api-result';
import { environment } from '../../../environments/environment';

export interface LeaveType {
  leaveTypeId: number;
  code: string;
  name: string;
  defaultAnnualQuota: number;
  isPaid: boolean;
  requiresAttachment: boolean;
  isActive: boolean;
  displayOrder: number;
}

export interface LeavePolicyRow {
  leavePolicyId: number;
  employeeId: number;
  employeeName?: string;
  leaveTypeId: number;
  leaveTypeName?: string;
  leaveTypeCode?: string;
  academicYearId: number;
  academicYearName?: string;
  annualQuota: number;
  notes?: string;
}

export interface LeaveRouteRow {
  leaveApprovalRouteId: number;
  applicantRoleId: number;
  applicantRoleName?: string;
  approverUserId: number;
  approverName?: string;
  approverUserName?: string;
  notes?: string;
}

export interface LeaveBalanceRow {
  leaveTypeId: number;
  code: string;
  name: string;
  isPaid: boolean;
  requiresAttachment: boolean;
  allocated: number;
  used: number;
  pending: number;
  available: number;
}

export interface LeaveApplicationRow {
  leaveApplicationId: number;
  leaveCode: string;
  applicantUserId: number;
  applicantName?: string;
  applicantUserName?: string;
  leaveTypeId: number;
  leaveTypeName?: string;
  leaveTypeCode?: string;
  startDate: string;
  endDate: string;
  halfDayFrom?: boolean;
  halfDayTo?: boolean;
  totalDays: number;
  reason?: string;
  attachmentPath?: string;
  attachmentOriginalName?: string;
  status: 'Pending' | 'Approved' | 'Rejected' | 'Cancelled';
  approverUserId?: number;
  approverName?: string;
  decidedByUserId?: number;
  decidedByName?: string;
  decidedAt?: string;
  decisionReason?: string;
  submittedAt: string;
}

export interface LeaveDashboardBundle {
  totals: {
    pending: number;
    approvedThisMonth: number;
    rejectedThisMonth: number;
    upcomingIn30Days: number;
  };
  pendingByType: Array<{ leaveTypeName?: string; pendingCount: number; pendingDays: number }>;
  recent: LeaveApplicationRow[];
}

@Injectable({ providedIn: 'root' })
export class LeaveService {
  private api = inject(ApiService);
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  /* Types */
  getTypes(activeOnly = false): Observable<ApiResult<LeaveType[]>> {
    return this.api.post<LeaveType[]>('Leave/LeaveGetTypes', { activeOnly });
  }
  upsertType(payload: Partial<LeaveType>) { return this.api.post('Leave/LeaveUpsertType', payload); }
  deleteType(leaveTypeId: number) { return this.api.post('Leave/LeaveDeleteType', { leaveTypeId }); }

  /* Policy */
  getPolicies(academicYearId: number | null, employeeId: number | null) {
    return this.api.post<LeavePolicyRow[]>('Leave/LeaveGetPolicies', { academicYearId, employeeId });
  }
  upsertPolicy(payload: { employeeId: number; leaveTypeId: number; academicYearId: number; annualQuota: number; notes?: string }) {
    return this.api.post('Leave/LeaveUpsertPolicy', payload);
  }

  /* Routing */
  getRoutes() { return this.api.post<LeaveRouteRow[]>('Leave/LeaveGetRoutes', {}); }
  upsertRoute(payload: { applicantRoleId: number; approverUserId: number; notes?: string }) {
    return this.api.post('Leave/LeaveUpsertRoute', payload);
  }

  /* Balance + Applications */
  getMyBalance(academicYearId: number | null = null) {
    return this.api.post<LeaveBalanceRow[]>('Leave/LeaveGetMyBalance', { academicYearId });
  }
  getApplications(payload: {
    scope: 'Mine' | 'Pending' | 'All';
    status?: string | null;
    fromDate?: string | null;
    toDate?: string | null;
    leaveTypeId?: number | null;
  }) {
    return this.api.post<LeaveApplicationRow[]>('Leave/LeaveGetApplications', payload);
  }

  submit(fields: {
    leaveTypeId: number; startDate: string; endDate: string;
    halfDayFrom: boolean; halfDayTo: boolean;
    reason?: string; attachment?: File | null;
  }): Observable<ApiResult<unknown>> {
    const form = new FormData();
    form.append('LeaveTypeId', String(fields.leaveTypeId));
    form.append('StartDate', fields.startDate);
    form.append('EndDate', fields.endDate);
    form.append('HalfDayFrom', String(fields.halfDayFrom));
    form.append('HalfDayTo', String(fields.halfDayTo));
    if (fields.reason) form.append('Reason', fields.reason);
    if (fields.attachment) form.append('Attachment', fields.attachment, fields.attachment.name);
    return this.http.post<any>(`${this.base}/Leave/LeaveSubmit`, form).pipe(map(r => this.normalise(r)));
  }

  /** Server-side calc — skips weekends + holidays + applies half-day deductions. */
  previewWorkingDays(startDate: string, endDate: string, halfDayFrom: boolean, halfDayTo: boolean) {
    return this.api.post<number>('Leave/LeavePreviewWorkingDays', { startDate, endDate, halfDayFrom, halfDayTo });
  }

  decide(leaveApplicationId: number, decision: 'Approved' | 'Rejected', reason?: string) {
    return this.api.post('Leave/LeaveDecide', { leaveApplicationId, decision, reason });
  }
  cancel(leaveApplicationId: number, reason?: string) {
    return this.api.post('Leave/LeaveCancel', { leaveApplicationId, reason });
  }
  getDashboard() { return this.api.post<LeaveDashboardBundle>('Leave/LeaveGetDashboard', {}); }

  attachmentUrl(path: string, name?: string): string {
    const qp = new URLSearchParams({ path });
    if (name) qp.set('name', name);
    return `${this.base}/Leave/LeaveDownloadAttachment?${qp.toString()}`;
  }

  downloadAttachment(path: string, name?: string): Observable<Blob> {
    return this.http.get(this.attachmentUrl(path, name), { responseType: 'blob' });
  }

  private normalise(r: any): ApiResult<any> {
    return {
      data: r?.Data ?? r?.data,
      message: r?.Message ?? r?.message,
      statusCode: r?.StatusCode ?? r?.statusCode,
      isSuccess: r?.IsSuccess ?? r?.isSuccess,
      totalCount: r?.TotalCount ?? r?.totalCount
    };
  }
}
