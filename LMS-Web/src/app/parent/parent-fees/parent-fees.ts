import { Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { ParentService } from '../parent.service';
import { StudentLedgerEntry } from '../../fees/fee.models';
import { StudentPayment } from '../parent.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-parent-fees',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, TagModule],
  templateUrl: './parent-fees.html'
})
export class ParentFees {
  parent = inject(ParentService);
  private toast = inject(ToastService);

  loading = signal(false);
  entries = signal<StudentLedgerEntry[]>([]);
  payments = signal<StudentPayment[]>([]);
  downloadingId = signal<number | null>(null);
  private lastLoadedId: number | null = null;

  constructor() {
    this.parent.loadChildren().subscribe();
    effect(() => {
      const id = this.parent.selectedChildId();
      if (id && id !== this.lastLoadedId) this.load(id);
    });
  }

  private load(studentId: number): void {
    this.lastLoadedId = studentId;
    this.loading.set(true);
    this.entries.set([]);
    this.payments.set([]);
    this.parent.getChildFees(studentId).subscribe({
      next: res => { this.entries.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
    this.parent.getChildPayments(studentId).subscribe({
      next: res => this.payments.set(res.data ?? [])
    });
  }

  closingBalance(): number {
    const e = this.entries();
    return e.length ? e[e.length - 1].runningBalance : 0;
  }

  typeSeverity(type?: string): 'success' | 'info' | 'danger' | 'warn' | 'secondary' {
    switch (type) {
      case 'Payment': return 'success';
      case 'Invoice': return 'info';
      case 'Reversal': return 'danger';
      case 'Concession': return 'warn';
      default: return 'secondary';
    }
  }

  download(p: StudentPayment): void {
    const studentId = this.parent.selectedChildId();
    if (!studentId) return;
    this.downloadingId.set(p.paymentId);
    this.parent.getReceiptPdf(studentId, p.paymentId).subscribe({
      next: blob => {
        this.downloadingId.set(null);
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = `Receipt_${p.receiptNo || p.paymentId}.pdf`; a.click();
        URL.revokeObjectURL(url);
      },
      error: () => { this.downloadingId.set(null); this.toast.error('Could not download the receipt.'); }
    });
  }
}
