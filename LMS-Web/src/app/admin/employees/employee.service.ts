import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import { environment } from '../../../environments/environment';
import {
  EmployeeListItem, EmployeeDetail, MarkEmployeeLeft, ClassSubjectPair, TeacherAssignment,
  EmployeeDocument, EmployeeSalary, UpsertEmployeeSalary, EmployeeAdvance
} from './employee.models';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private api = inject(ApiService);
  private http = inject(HttpClient);

  /**
   * Fetch a document as a Blob (Bearer token attached by the auth interceptor)
   * and either open it in a new tab (view) or push a download prompt.
   * We can't just use a plain <a href> because the download endpoint requires
   * auth and the browser won't attach the token to a native navigation.
   */
  viewDocument(filePath: string, fileName?: string): Observable<Blob> {
    const url = this.documentUrl(filePath, fileName);
    return this.http.get(url, { responseType: 'blob' }).pipe(
      tap(blob => {
        const objUrl = URL.createObjectURL(blob);
        // Open in a new tab — the browser renders PDFs / images inline
        // and shows a save dialog for other types. Revoke after a short
        // delay so the tab has time to load.
        window.open(objUrl, '_blank');
        setTimeout(() => URL.revokeObjectURL(objUrl), 60_000);
      })
    );
  }

  downloadDocument(filePath: string, fileName?: string): Observable<Blob> {
    const url = this.documentUrl(filePath, fileName);
    return this.http.get(url, { responseType: 'blob' }).pipe(
      tap(blob => {
        const objUrl = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = objUrl;
        a.download = fileName || 'document';
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(objUrl);
      })
    );
  }

  private documentUrl(filePath: string, fileName?: string): string {
    const qp = new URLSearchParams({ path: filePath });
    if (fileName) qp.set('name', fileName);
    return `${environment.apiBaseUrl}/Employee/EmployeeDownloadDocument?${qp.toString()}`;
  }

  getAll(search: SearchRequest = defaultSearch()): Observable<ApiResult<EmployeeListItem[]>> {
    return this.api.post<EmployeeListItem[]>('Employee/EmployeeGetAll', { searchRequest: search });
  }

  getById(id: number): Observable<ApiResult<EmployeeDetail>> {
    return this.api.post<EmployeeDetail>('Employee/EmployeeGetById', { searchRequestById: { id, cultureId: null } });
  }

  create(dto: EmployeeDetail): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeCreate', dto);
  }

  update(dto: EmployeeDetail): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeUpdate', dto);
  }

  markLeft(dto: MarkEmployeeLeft): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeMarkLeft', dto);
  }

  archive(id: number): Observable<ApiResult<unknown>> {
    const deleteRequest: DeleteRequest = { selectedIds: String(id), isDeleted: true, forceHard: false };
    return this.api.post('Employee/EmployeeDelete', { deleteRequest });
  }

  // Teacher assignments (EMP-06)
  getTeacherAssignments(employeeId: number): Observable<ApiResult<TeacherAssignment[]>> {
    return this.api.post<TeacherAssignment[]>('Employee/EmployeeGetTeacherAssignments', {
      searchRequestById: { id: employeeId, cultureId: null }
    });
  }

  assignTeacherSubjects(employeeId: number, assignments: ClassSubjectPair[]): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeAssignTeacherSubjects', { employeeId, assignments });
  }

  // Documents (EMP-08)
  getDocuments(employeeId: number): Observable<ApiResult<EmployeeDocument[]>> {
    return this.api.post<EmployeeDocument[]>('Employee/EmployeeGetDocuments', {
      searchRequestById: { id: employeeId, cultureId: null }
    });
  }

  uploadDocument(payload: {
    employeeId: number; documentType: string; fileName: string; filePath: string;
    contentType?: string; fileSizeBytes?: number; isPhoto: boolean;
  }): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeUploadDocument', payload);
  }

  // Salary (foundational)
  getCurrentSalary(employeeId: number): Observable<ApiResult<EmployeeSalary>> {
    return this.api.post<EmployeeSalary>('Employee/EmployeeGetCurrentSalary', {
      searchRequestById: { id: employeeId, cultureId: null }
    });
  }

  getSalaryHistory(employeeId: number): Observable<ApiResult<EmployeeSalary[]>> {
    return this.api.post<EmployeeSalary[]>('Employee/EmployeeGetSalaryHistory', {
      searchRequestById: { id: employeeId, cultureId: null }
    });
  }

  upsertSalary(dto: UpsertEmployeeSalary): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeUpsertSalary', dto);
  }

  // Advances (foundational)
  getAdvances(employeeId: number): Observable<ApiResult<EmployeeAdvance[]>> {
    return this.api.post<EmployeeAdvance[]>('Employee/EmployeeGetAdvances', {
      searchRequestById: { id: employeeId, cultureId: null }
    });
  }

  issueAdvance(employeeId: number, amount: number, reason?: string): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeIssueAdvance', { employeeId, amount, reason });
  }

  adjustAdvance(advanceId: number, amountAdjusted: number, reason?: string): Observable<ApiResult<unknown>> {
    return this.api.post('Employee/EmployeeAdjustAdvance', { advanceId, amountAdjusted, reason });
  }
}
