import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ApiResult } from '../../core/models/api-result';
import { CalendarConfig, NonWorkingDate, SchoolHoliday } from './calendar.models';

@Injectable({ providedIn: 'root' })
export class CalendarService {
  private api = inject(ApiService);

  getHolidays(fromDate?: string | null, toDate?: string | null): Observable<ApiResult<SchoolHoliday[]>> {
    return this.api.post<SchoolHoliday[]>('Calendar/CalendarGetHolidays', { fromDate, toDate });
  }
  upsertHoliday(holidayDate: string, description: string): Observable<ApiResult<unknown>> {
    return this.api.post('Calendar/CalendarUpsertHoliday', { holidayDate, description });
  }
  deleteHoliday(id: number): Observable<ApiResult<unknown>> {
    return this.api.post('Calendar/CalendarDeleteHoliday', { id });
  }

  getConfig(): Observable<ApiResult<CalendarConfig>> {
    return this.api.post<CalendarConfig>('Calendar/CalendarGetConfig', {});
  }
  setConfig(weekendDays: string): Observable<ApiResult<unknown>> {
    return this.api.post('Calendar/CalendarSetConfig', { weekendDays });
  }

  getNonWorkingDates(fromDate: string, toDate: string): Observable<ApiResult<NonWorkingDate[]>> {
    return this.api.post<NonWorkingDate[]>('Calendar/CalendarGetNonWorkingDates', { fromDate, toDate });
  }
}
