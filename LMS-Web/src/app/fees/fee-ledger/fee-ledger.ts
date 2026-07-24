import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { FieldsetModule } from 'primeng/fieldset';
import { FeeService } from '../fee.service';
import { StudentLedgerEntry, AdvanceBalance } from '../fee.models';
import { StudentService } from '../../students/student.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { AuthService } from '../../core/services/auth.service';
import { Roles } from '../../core/models/roles';

@Component({
  selector: 'app-fee-ledger',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TagModule, FieldsetModule
  ],
  templateUrl: './fee-ledger.html'
})
export class FeeLedger implements OnInit {
  private svc = inject(FeeService);
  private studentSvc = inject(StudentService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  studentOptions = signal<{ label: string; value: number }[]>([]);
  yearOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All years', value: null }]);
  studentId: number | null = null;
  yearId: number | null = null;

  loading = signal(false);
  entries = signal<StudentLedgerEntry[]>([]);
  advance = signal<AdvanceBalance | null>(null);

  canCorrect = this.auth.hasAnyRole([Roles.Admin]);

  // corrections
  reprintId: number | null = null;
  reverseId: number | null = null;
  reverseReason = '';
  busy = signal(false);

  ngOnInit(): void {
    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      this.studentOptions.set((res.data ?? []).map(s => ({
        label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`, value: s.id
      })));
    });
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      this.yearOptions.set([{ label: 'All years', value: null }, ...(res.data ?? []).map(y => ({ label: y.displayName, value: y.id as number | null }))]);
    });
  }

  load(): void {
    if (!this.studentId) { this.toast.warn('Select a student.'); return; }
    this.loading.set(true);
    this.svc.getStudentLedger(this.studentId, this.yearId).subscribe({
      next: res => { this.entries.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
    this.svc.getAdvanceBalance(this.studentId).subscribe({ next: res => this.advance.set(res.data ?? null) });
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

  closingBalance(): number {
    const e = this.entries();
    return e.length ? e[e.length - 1].runningBalance : 0;
  }

  reprint(): void {
    if (!this.reprintId) { this.toast.warn('Enter a payment #.'); return; }
    this.busy.set(true);
    this.svc.getReceiptPdf(this.reprintId, true).subscribe({
      next: blob => {
        this.busy.set(false);
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = `TSSS_Receipt_${this.reprintId}.pdf`; a.click();
        URL.revokeObjectURL(url);
      },
      error: () => { this.busy.set(false); this.toast.error('Could not generate receipt.'); }
    });
  }

  /** Download the invoice PDF for an Invoice-type ledger row. */
  downloadInvoice(e: { entityId?: number | null; reference?: string | null }): void {
    if (!e?.entityId) return;
    this.svc.getInvoicePdf(e.entityId).subscribe({
      next: blob => this.saveBlob(blob, `TSSS_Invoice_${(e.reference ?? String(e.entityId ?? '')).replace(/-/g, '_')}.pdf`),
      error: () => this.toast.error('Could not download the invoice.')
    });
  }

  /** Reprint the receipt PDF for a Payment-type ledger row (IsDuplicate=true so
   *  the DUPLICATE watermark is applied — the original receipt was already handed
   *  over at collection time). */
  downloadReceipt(e: { entityId?: number | null; reference?: string | null }): void {
    if (!e?.entityId) return;
    this.svc.getReceiptPdf(e.entityId, true).subscribe({
      next: blob => this.saveBlob(blob, `TSSS_Receipt_${(e.reference ?? String(e.entityId ?? '')).replace(/-/g, '_')}.pdf`),
      error: () => this.toast.error('Could not download the receipt.')
    });
  }

  private saveBlob(blob: Blob, fileName: string): void {
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    a.click();
    URL.revokeObjectURL(url);
  }

  reverse(): void {
    if (!this.reverseId) { this.toast.warn('Enter a payment #.'); return; }
    if (!this.reverseReason.trim()) { this.toast.warn('Reason is required for reversal.'); return; }
    this.busy.set(true);
    this.svc.reversePayment({ paymentId: this.reverseId, reason: this.reverseReason }).subscribe({
      next: () => { this.toast.success('Payment reversed'); this.busy.set(false); this.reverseId = null; this.reverseReason = ''; this.load(); },
      error: () => this.busy.set(false)
    });
  }
}
