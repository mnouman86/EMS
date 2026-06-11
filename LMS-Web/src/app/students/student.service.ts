import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../core/models/search-request';
import { ApiResult } from '../core/models/api-result';
import {
  StudentListItem, StudentDetail, SubmitAdmission, UpdateStudent,
  ChangeStudentStatus, StudentImportRow, PromoteStudents
} from './student.models';

@Injectable({ providedIn: 'root' })
export class StudentService {
  private api = inject(ApiService);

  getAll(search: SearchRequest = defaultSearch()): Observable<ApiResult<StudentListItem[]>> {
    return this.api.post<StudentListItem[]>('Student/StudentGetAll', { searchRequest: search });
  }

  getById(id: number): Observable<ApiResult<StudentDetail>> {
    return this.api.post<StudentDetail>('Student/StudentGetById', { searchRequestById: { id, cultureId: null } });
  }

  /** STU-01 (public admission). */
  submit(dto: SubmitAdmission): Observable<ApiResult<unknown>> {
    return this.api.post('Student/StudentCreate', dto);
  }

  update(dto: UpdateStudent): Observable<ApiResult<unknown>> {
    return this.api.post('Student/StudentUpdate', dto);
  }

  changeStatus(dto: ChangeStudentStatus): Observable<ApiResult<unknown>> {
    return this.api.post('Student/StudentChangeStatus', dto);
  }

  bulkImport(rows: StudentImportRow[], updateExisting: boolean): Observable<ApiResult<unknown>> {
    return this.api.post('Student/StudentBulkImport', { rows, updateExisting });
  }

  promote(dto: PromoteStudents): Observable<ApiResult<unknown>> {
    return this.api.post('Student/StudentPromote', dto);
  }

  archive(id: number): Observable<ApiResult<unknown>> {
    const deleteRequest: DeleteRequest = { selectedIds: String(id), isDeleted: true, forceHard: false };
    return this.api.post('Student/StudentDelete', { deleteRequest });
  }
}
