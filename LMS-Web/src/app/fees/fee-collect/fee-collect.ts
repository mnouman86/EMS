import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { MessageModule } from 'primeng/message';
import { FeeService } from '../fee.service';
import { StudentLedgerEntry, AdvanceBalance } from '../fee.models';
import { StudentService } from '../../students/student.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-fee-collect',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule, DatePickerModule, MessageModule
  ],
  templateUrl: './fee-collect.html'
})
export class FeeCollect implements OnInit {
  private svc = inject(FeeService);
  private studentSvc = inject(StudentService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  studentOptions = signal<{ label: string; value: number }[]>([]);
  studentId: number | null = null;

  loadingContext = signal(false);
  outstanding = signal<number | null>(null);
  advance = signal<AdvanceBalance | null>(null);

  modeOptions = ['Cash', 'Cheque', 'BankTransfer', 'Online'].map(v => ({ label: v, value: v }));

  paymentDate: Date = new Date();
  amount = 0;
  paymentMode = 'Cash';
  referenceNo = '';
  remarks = '';

  saving = signal(false);
  lastPaymentId = signal<number | null>(null);
  downloading = signal(false);

  ngOnInit(): void {
    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      this.studentOptions.set((res.data ?? []).map(s => ({
        label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`,
        value: s.id
      })));
    });
  }

  onStudentChange(): void {
    this.lastPaymentId.set(null);
    this.outstanding.set(null);
    this.advance.set(null);
    if (!this.studentId) return;
    this.loadingContext.set(true);
    this.svc.getStudentLedger(this.studentId).subscribe({
      next: res => {
        const entries = res.data ?? [];
        const last = entries.length ? entries[entries.length - 1] : null;
        this.outstanding.set(last ? last.runningBalance : 0);
        this.loadingContext.set(false);
      },
      error: () => this.loadingContext.set(false)
    });
    this.svc.getAdvanceBalance(this.studentId).subscribe({ next: res => this.advance.set(res.data ?? null) });
  }

  get needsReference(): boolean {
    return this.paymentMode === 'Cheque' || this.paymentMode === 'BankTransfer' || this.paymentMode === 'Online';
  }

  record(): void {
    if (!this.studentId) { this.toast.warn('Select a student.'); return; }
    if (this.amount <= 0) { this.toast.warn('Amount must be greater than 0.'); return; }
    if (this.needsReference && !this.referenceNo.trim()) { this.toast.warn('Reference number is required for this payment mode.'); return; }

    this.saving.set(true);
    this.svc.recordPayment({
      studentId: this.studentId,
      paymentDate: this.paymentDate.toISOString(),
      amount: this.amount,
      paymentMode: this.paymentMode,
      referenceNo: this.referenceNo || null,
      remarks: this.remarks || null,
      collectingStaffId: this.auth.currentUser()?.id ?? 0
    }).subscribe({
      next: res => {
        this.toast.success('Payment recorded');
        this.saving.set(false);
        const pid = res.data?.recordID ?? null;
        this.lastPaymentId.set(pid);
        // reset entry fields, keep student for context refresh
        this.amount = 0; this.referenceNo = ''; this.remarks = '';
        this.onStudentChange();
      },
      error: () => this.saving.set(false)
    });
  }

  downloadReceipt(duplicate = false): void {
    const pid = this.lastPaymentId();
    if (!pid) return;
    this.downloading.set(true);
    this.svc.getReceiptPdf(pid, duplicate).subscribe({
      next: blob => {
        this.downloading.set(false);
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `TSSS_Receipt_${pid}.pdf`;
        a.click();
        URL.revokeObjectURL(url);
      },
      error: () => { this.downloading.set(false); this.toast.error('Could not generate receipt.'); }
    });
  }
}
