import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ApiResult, RawApiResult } from '../models/api-result';

/**
 * Thin wrapper over HttpClient that:
 *  - prefixes every call with the configured apiBaseUrl
 *  - normalises the PascalCase {Data,Message,StatusCode,IsSuccess,TotalCount}
 *    envelope into a camelCase ApiResult<T>
 *
 * Most EMS endpoints are POST (commands AND queries), so `post` is the workhorse.
 */
@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  post<T>(relativeUrl: string, body: unknown = {}): Observable<ApiResult<T>> {
    return this.http
      .post<RawApiResult<T>>(this.url(relativeUrl), body)
      .pipe(map(r => this.normalise<T>(r)));
  }

  get<T>(relativeUrl: string, params?: Record<string, string | number | boolean>): Observable<ApiResult<T>> {
    let httpParams = new HttpParams();
    if (params) {
      Object.entries(params).forEach(([k, v]) => (httpParams = httpParams.set(k, String(v))));
    }
    return this.http
      .get<RawApiResult<T>>(this.url(relativeUrl), { params: httpParams })
      .pipe(map(r => this.normalise<T>(r)));
  }

  /** POST that returns a binary file (PDF download endpoints). */
  postBlob(relativeUrl: string, body: unknown = {}): Observable<Blob> {
    return this.http.post(this.url(relativeUrl), body, { responseType: 'blob' });
  }

  /** Multipart file upload to the API's /uploadFile endpoint. Returns { dbPath }. */
  uploadFile(file: File): Observable<{ dbPath: string }> {
    const form = new FormData();
    form.append('file', file, file.name);
    return this.http.post<{ dbPath: string }>(this.url('uploadFile'), form);
  }

  private url(relative: string): string {
    const trimmed = relative.startsWith('/') ? relative.slice(1) : relative;
    return `${this.base}/${trimmed}`;
  }

  private normalise<T>(r: RawApiResult<T>): ApiResult<T> {
    // Some servers may already camelCase via serializer settings; tolerate both.
    const anyR = r as any;
    return {
      data: anyR.Data ?? anyR.data,
      message: anyR.Message ?? anyR.message,
      statusCode: anyR.StatusCode ?? anyR.statusCode,
      isSuccess: anyR.IsSuccess ?? anyR.isSuccess,
      totalCount: anyR.TotalCount ?? anyR.totalCount
    };
  }
}
