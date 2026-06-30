import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ApiResult } from '../../core/models/api-result';
import { MyTeachingBundle } from './my-teaching.models';

@Injectable({ providedIn: 'root' })
export class MyTeachingService {
  private api = inject(ApiService);

  get(): Observable<ApiResult<MyTeachingBundle>> {
    return this.api.post<MyTeachingBundle>('MyTeaching/MyTeachingGet', {});
  }
}
