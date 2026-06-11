import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import {
  EmployeeListItem, EmployeeDetail, MarkEmployeeLeft, ClassSubjectPair, TeacherAssignment,
  EmployeeDocument, EmployeeSalary, UpsertEmployeeSalary, EmployeeAdvance
} from './employee.models';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private api = inject(ApiService);

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
