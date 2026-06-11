import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import { SchoolClass, CreateSchoolClass, UpdateSchoolClass } from './school-class.models';

@Injectable({ providedIn: 'root' })
export class SchoolClassService {
  private api = inject(ApiService);

  getAll(search: SearchRequest = defaultSearch()): Observable<ApiResult<SchoolClass[]>> {
    return this.api.post<SchoolClass[]>('SchoolClass/SchoolClassGetAll', { searchRequest: search });
  }

  create(dto: CreateSchoolClass): Observable<ApiResult<unknown>> {
    return this.api.post('SchoolClass/SchoolClassCreate', dto);
  }

  update(dto: UpdateSchoolClass): Observable<ApiResult<unknown>> {
    return this.api.post('SchoolClass/SchoolClassUpdate', dto);
  }

  archive(id: number): Observable<ApiResult<unknown>> {
    const deleteRequest: DeleteRequest = { selectedIds: String(id), isDeleted: true, forceHard: false };
    return this.api.post('SchoolClass/SchoolClassDelete', { deleteRequest });
  }

  assignTeacher(schoolClassId: number, classTeacherId: number | null): Observable<ApiResult<unknown>> {
    return this.api.post('SchoolClass/SchoolClassAssignTeacher', { schoolClassId, classTeacherId });
  }
}
