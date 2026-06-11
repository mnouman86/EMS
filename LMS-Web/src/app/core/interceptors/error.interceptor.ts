import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';
import { ToastService } from '../services/toast.service';

/**
 * Global HTTP error handling:
 *  - 401  → clear session + redirect to /login
 *  - 403  → toast "not authorised"
 *  - 5xx  → toast generic server error
 *  - other → surface the API Message when present
 * Rethrows so component-level handlers can still react.
 */
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const storage = inject(TokenStorageService);
  const toast = inject(ToastService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      const apiMessage = err.error?.Message ?? err.error?.message;

      if (err.status === 401) {
        storage.clear();
        router.navigate(['/login']);
      } else if (err.status === 403) {
        toast.error('You are not authorised to perform this action.');
      } else if (err.status >= 500) {
        toast.error(apiMessage || 'A server error occurred. Please try again.');
      } else if (err.status === 0) {
        toast.error('Cannot reach the server. Check your connection.');
      } else if (apiMessage) {
        toast.error(apiMessage);
      }

      return throwError(() => err);
    })
  );
};
