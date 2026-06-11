import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import { Subject, CreateSubject, UpdateSubject, ClassSubject } from './subject.models';

@Injectable({ providedIn: 'root' })
export class SubjectService {
  private api = inject(ApiService);

  getAll(search: SearchRequest = defaultSearch()): Observable<ApiResult<Subject[]>> {
    return this.api.post<Subject[]>('Subject/SubjectGetAll', { searchRequest: search });
  }

  create(dto: CreateSubject): Observable<ApiResult<unknown>> {
    return this.api.post('Subject/SubjectCreate', dto);
  }

  update(dto: UpdateSubject): Observable<ApiResult<unknown>> {
    return this.api.post('Subject/SubjectUpdate', dto);
  }

  deactivate(id: number): Observable<ApiResult<unknown>> {
    const deleteRequest: DeleteRequest = { selectedIds: String(id), isDeleted: true, forceHard: false };
    return this.api.post('Subject/SubjectDelete', { deleteRequest });
  }

  // SUB-05: class-subject mapping
  getByClass(schoolClassId: number): Observable<ApiResult<ClassSubject[]>> {
    return this.api.post<ClassSubject[]>('Subject/SubjectGetByClass', {
      searchRequestById: { id: schoolClassId, cultureId: null }
    });
  }

  mapToClass(schoolClassId: number, subjectIds: number[]): Observable<ApiResult<unknown>> {
    return this.api.post('Subject/SubjectMapToClass', { schoolClassId, subjectIds });
  }
}
