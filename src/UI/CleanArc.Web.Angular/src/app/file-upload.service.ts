import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpEvent, HttpEventType, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private apiUrl = 'https://localhost:7116/api/Upload';  // Replace with your API URL

  constructor(private http: HttpClient) { }

  uploadFile(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post(this.apiUrl, formData, {
      reportProgress: true,
      observe: 'events'
    }).pipe(
      map((event: HttpEvent<any>) => this.getEventMessage(event, formData)),
      catchError(this.handleError)
    );
  }

  private getEventMessage(event: HttpEvent<any>, formData: FormData) {
    switch (event.type) {
      case HttpEventType.UploadProgress:
        return this.progress(event);
      case HttpEventType.Response:
        return event.body;
      default:
        return `File "${formData.get('file')}" surprising upload event: ${event.type}.`;
    }
  }

  private progress(event: HttpEvent<any>) {
    if (event.type === HttpEventType.UploadProgress) {
      const percentDone = Math.round(100 * event.loaded / (event.total ?? 1));
      return { status: 'progress', message: `${percentDone}% done` };
    }
    return { status: 'progress', message: `Uploading...` };
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => new Error(`File upload failed: ${error.message}`));
  }
}
