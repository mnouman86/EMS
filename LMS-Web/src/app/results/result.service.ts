import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { SearchRequest, defaultSearch } from '../core/models/search-request';
import { ApiResult } from '../core/models/api-result';
import {
  ResultSession, CreateResultSession, GradeBand, ConfigureGradeBands,
  MarksEntryGridRow, EnterMarks, MarksEntryGridRequest, PreLockMissingRow, LockClassResults,
  ClassSheetRow, StudentResultCard, ParentResultSummary
} from './result.models';

@Injectable({ providedIn: 'root' })
export class ResultService {
  private api = inject(ApiService);

  /* RES-01: sessions */
  getSessions(search: SearchRequest = defaultSearch()): Observable<ApiResult<ResultSession[]>> {
    return this.api.post<ResultSession[]>('Result/ResultGetSessions', { searchRequest: search });
  }
  createSession(dto: CreateResultSession): Observable<ApiResult<unknown>> {
    return this.api.post('Result/ResultCreateSession', dto);
  }

  /* RES-04: grade bands */
  getGradeBands(): Observable<ApiResult<GradeBand[]>> {
    return this.api.post<GradeBand[]>('Result/ResultGetGradeBands', {});
  }
  configureGradeBands(dto: ConfigureGradeBands): Observable<ApiResult<unknown>> {
    return this.api.post('Result/ResultConfigureGradeBands', dto);
  }

  /* RES-02 / RES-03: marks */
  getMarksGrid(request: MarksEntryGridRequest): Observable<ApiResult<MarksEntryGridRow[]>> {
    return this.api.post<MarksEntryGridRow[]>('Result/ResultGetMarksEntryGrid', { request });
  }
  enterMarks(dto: EnterMarks): Observable<ApiResult<unknown>> {
    return this.api.post('Result/ResultEnterMarks', dto);
  }

  /* RES-05: lock / unlock */
  getPreLockReport(resultSessionId: number, schoolClassId: number): Observable<ApiResult<PreLockMissingRow[]>> {
    return this.api.post<PreLockMissingRow[]>('Result/ResultGetPreLockReport', { resultSessionId, schoolClassId });
  }
  lockClass(dto: LockClassResults): Observable<ApiResult<unknown>> {
    return this.api.post('Result/ResultLockClass', dto);
  }
  unlockClass(dto: LockClassResults): Observable<ApiResult<unknown>> {
    return this.api.post('Result/ResultUnlockClass', dto);
  }

  /* RES-06: class sheet */
  getClassSheet(resultSessionId: number, schoolClassId: number): Observable<ApiResult<ClassSheetRow[]>> {
    return this.api.post<ClassSheetRow[]>('Result/ResultGetClassSheet', { resultSessionId, schoolClassId });
  }

  /* RES-08: student result card */
  getStudentCard(resultSessionId: number, studentId: number): Observable<ApiResult<StudentResultCard>> {
    return this.api.post<StudentResultCard>('Result/ResultGetStudentCard', { resultSessionId, studentId });
  }

  /* RES-07: parent search (anonymous) */
  parentSearch(studentCode: string, secondFactor: string): Observable<ApiResult<ParentResultSummary>> {
    return this.api.post<ParentResultSummary>('Result/ResultParentSearch', { studentCode, secondFactor });
  }
}
