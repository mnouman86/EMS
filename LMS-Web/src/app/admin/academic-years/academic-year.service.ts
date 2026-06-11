import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, DeleteRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import { AcademicYear, CreateAcademicYear, UpdateAcademicYear } from './academic-year.models';

@Injectable({ providedIn: 'root' })
export class AcademicYearService {
  private api = inject(ApiService);

  getAll(search: SearchRequest = defaultSearch()): Observable<ApiResult<AcademicYear[]>> {
    return this.api.post<AcademicYear[]>('AcademicYear/AcademicYearGetAll', { searchRequest: search });
  }

  getCurrent(): Observable<ApiResult<AcademicYear>> {
    return this.api.post<AcademicYear>('AcademicYear/AcademicYearGetCurrent', {});
  }

  create(dto: CreateAcademicYear): Observable<ApiResult<unknown>> {
    return this.api.post('AcademicYear/AcademicYearCreate', dto);
  }

  update(dto: UpdateAcademicYear): Observable<ApiResult<unknown>> {
    return this.api.post('AcademicYear/AcademicYearUpdate', dto);
  }

  setCurrent(id: number): Observable<ApiResult<unknown>> {
    return this.api.post('AcademicYear/AcademicYearSetCurrent', { id });
  }

  archive(id: number): Observable<ApiResult<unknown>> {
    const deleteRequest: DeleteRequest = { selectedIds: String(id), isDeleted: true, forceHard: false };
    return this.api.post('AcademicYear/AcademicYearDelete', { deleteRequest });
  }
}
