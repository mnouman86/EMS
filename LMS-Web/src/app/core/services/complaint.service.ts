import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { ApiResult } from '../models/api-result';
import { environment } from '../../../environments/environment';

/* ---------- Models (kept here to avoid a one-off models file) ---------- */

export interface ComplaintNature {
  complaintNatureId: number;
  name: string;
  complaintType: 'Complaint' | 'Suggestion' | 'Either';
  displayOrder: number;
  isActive: boolean;
}

export interface ComplaintRow {
  complaintId: number;
  complaintCode: string;
  complaintNatureId: number;
  natureName: string;
  natureType: string;
  logonUserId?: number;
  logonUserFullName?: string;
  complainantName: string;
  contactNumber: string;
  complaintAgainst?: string;
  description: string;
  attachmentPath?: string;
  attachmentOriginalName?: string;
  status: 'New' | 'InReview' | 'Resolved' | 'Closed' | 'Rejected';
  isDeleted: boolean;
  deletedAt?: string;
  deletedReason?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface ComplaintAuditEntry {
  complaintAuditId: number;
  complaintId: number;
  action: string;
  fromStatus?: string;
  toStatus?: string;
  note?: string;
  actorUserId: number;
  actorFullName?: string;
  actorAt: string;
}

export interface ComplaintDetail {
  header: ComplaintRow;
  auditTrail: ComplaintAuditEntry[];
}

export interface ComplaintReportFilter {
  fromDate?: string | null;
  toDate?: string | null;
  complaintType?: string | null;
  complaintNatureId?: number | null;
  status?: string | null;
  complainantLike?: string | null;
  logonUserId?: number | null;
  hasAttachment?: boolean | null;
  includeDeleted?: boolean;
}

@Injectable({ providedIn: 'root' })
export class ComplaintService {
  private api = inject(ApiService);
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  /* ---- Nature catalogue ---- */
  getNatures(activeOnly = false): Observable<ApiResult<ComplaintNature[]>> {
    return this.api.post<ComplaintNature[]>('Complaint/ComplaintGetNatures', { activeOnly });
  }
  upsertNature(payload: Partial<ComplaintNature>): Observable<ApiResult<unknown>> {
    return this.api.post<unknown>('Complaint/ComplaintUpsertNature', payload);
  }
  deactivateNature(complaintNatureId: number): Observable<ApiResult<unknown>> {
    return this.api.post<unknown>('Complaint/ComplaintDeactivateNature', { complaintNatureId });
  }

  /* ---- Log / edit complaint (multipart) ---- */
  createComplaint(fields: {
    complaintNatureId: number;
    complainantName: string;
    contactNumber: string;
    description: string;
    complaintAgainst?: string;
    attachment?: File | null;
  }): Observable<ApiResult<unknown>> {
    const form = this.toMultipart({
      ComplaintNatureId: fields.complaintNatureId,
      ComplainantName: fields.complainantName,
      ContactNumber: fields.contactNumber,
      Description: fields.description,
      ComplaintAgainst: fields.complaintAgainst ?? ''
    }, fields.attachment ?? null);
    return this.http
      .post<any>(this.url('Complaint/ComplaintCreate'), form)
      .pipe(map(r => this.normalise(r)));
  }

  updateComplaint(fields: {
    complaintId: number;
    complaintNatureId: number;
    complainantName: string;
    contactNumber: string;
    description: string;
    complaintAgainst?: string;
    clearAttachment: boolean;
    attachment?: File | null;
  }): Observable<ApiResult<unknown>> {
    const form = this.toMultipart({
      ComplaintId: fields.complaintId,
      ComplaintNatureId: fields.complaintNatureId,
      ComplainantName: fields.complainantName,
      ContactNumber: fields.contactNumber,
      Description: fields.description,
      ComplaintAgainst: fields.complaintAgainst ?? '',
      ClearAttachment: fields.clearAttachment
    }, fields.attachment ?? null);
    return this.http
      .post<any>(this.url('Complaint/ComplaintUpdate'), form)
      .pipe(map(r => this.normalise(r)));
  }

  /* ---- Read ---- */
  getMine(): Observable<ApiResult<ComplaintRow[]>> {
    return this.api.post<ComplaintRow[]>('Complaint/ComplaintGetMine', {});
  }
  getAll(filter: ComplaintReportFilter): Observable<ApiResult<ComplaintRow[]>> {
    return this.api.post<ComplaintRow[]>('Complaint/ComplaintGetAll', filter);
  }
  getDetail(complaintId: number): Observable<ApiResult<ComplaintDetail>> {
    return this.api.post<ComplaintDetail>('Complaint/ComplaintGetDetail', { complaintId });
  }

  /* ---- Admin actions ---- */
  changeStatus(complaintId: number, toStatus: string, note: string) {
    return this.api.post<unknown>('Complaint/ComplaintChangeStatus', { complaintId, toStatus, note });
  }
  addNote(complaintId: number, note: string) {
    return this.api.post<unknown>('Complaint/ComplaintAddNote', { complaintId, note });
  }
  softDelete(complaintId: number, reason: string) {
    return this.api.post<unknown>('Complaint/ComplaintSoftDelete', { complaintId, reason });
  }
  restore(complaintId: number) {
    return this.api.post<unknown>('Complaint/ComplaintRestore', { complaintId });
  }

  /** Build a download URL for the attachment (token attached via interceptor as Bearer header). */
  attachmentUrl(path: string, name?: string): string {
    const qp = new URLSearchParams({ path });
    if (name) qp.set('name', name);
    return `${this.base}/Complaint/ComplaintDownloadAttachment?${qp.toString()}`;
  }

  /* ---- helpers ---- */

  private toMultipart(fields: Record<string, unknown>, file: File | null): FormData {
    const form = new FormData();
    Object.entries(fields).forEach(([k, v]) => {
      if (v === null || v === undefined) return;
      form.append(k, String(v));
    });
    if (file) form.append('Attachment', file, file.name);
    return form;
  }

  private url(relative: string): string {
    const t = relative.startsWith('/') ? relative.slice(1) : relative;
    return `${this.base}/${t}`;
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
