import { Injectable, inject } from '@angular/core';
import { MessageService } from 'primeng/api';

/** Thin wrapper over PrimeNG MessageService for consistent toasts.
 *  A single <p-toast> placed in AppComponent/AdminLayout renders these. */
@Injectable({ providedIn: 'root' })
export class ToastService {
  private messages = inject(MessageService);

  success(detail: string, summary = 'Success'): void {
    this.messages.add({ severity: 'success', summary, detail, life: 3000 });
  }

  error(detail: string, summary = 'Error'): void {
    this.messages.add({ severity: 'error', summary, detail, life: 5000 });
  }

  info(detail: string, summary = 'Info'): void {
    this.messages.add({ severity: 'info', summary, detail, life: 3000 });
  }

  warn(detail: string, summary = 'Warning'): void {
    this.messages.add({ severity: 'warn', summary, detail, life: 4000 });
  }
}
