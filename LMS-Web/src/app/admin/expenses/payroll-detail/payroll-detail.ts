import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ExpenseService } from '../expense.service';
import { PayrollRun, PayrollEntry } from '../expense.models';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

@Component({
  selector: 'app-payroll-detail',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, InputNumberModule, InputTextModule, TagModule, DialogModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './payroll-detail.html'
})
export class PayrollDetail implements OnInit {
  private svc = inject(ExpenseService);
  private route = inject(ActivatedRoute);
  private toast = inject(ToastService);
  private auth = inject(AuthService);
  private confirm = inject(ConfirmationService);

  private monthNames = ['', 'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

  canAdjust = this.auth.hasAnyRole([Roles.Admin, Roles.Accountant]);
  canConfirm = this.auth.hasAnyRole([Roles.Admin]);

  runId!: number;
  run = signal<PayrollRun | null>(null);
  entries = signal<PayrollEntry[]>([]);
  loading = signal(false);
  confirming = signal(false);
  downloadingId = signal<number | null>(null);

  isDraft = () => this.run()?.status === 'Draft';

  // adjust dialog
  dialog = signal(false);
  saving = signal(false);
  target = signal<PayrollEntry | null>(null);
  fine: number | null = null;
  other: number | null = null;
  notes = '';

  ngOnInit(): void {
    this.runId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadRun();
    this.loadEntries();
  }

  monthName(m?: number): string { return m ? this.monthNames[m] : ''; }

  loadRun(): void {
    this.svc.getPayrollRuns(null).subscribe(res => {
      this.run.set((res.data ?? []).find(r => r.id === this.runId) ?? null);
    });
  }

  loadEntries(): void {
    this.loading.set(true);
    this.svc.getPayrollEntries(this.runId).subscribe({
      next: res => { this.entries.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openAdjust(e: PayrollEntry): void {
    this.target.set(e);
    this.fine = e.fineDeduction || null;
    this.other = e.otherDeduction || null;
    this.notes = e.notes || '';
    this.dialog.set(true);
  }

  saveAdjust(): void {
    const t = this.target();
    if (!t) return;
    this.saving.set(true);
    this.svc.adjustPayrollEntry({
      payrollEntryId: t.id,
      fineDeduction: this.fine,
      otherDeduction: this.other,
      notes: this.notes || null
    }).subscribe({
      next: () => { this.toast.success('Entry adjusted'); this.saving.set(false); this.dialog.set(false); this.loadEntries(); this.loadRun(); },
      error: () => this.saving.set(false)
    });
  }

  confirmRun(): void {
    this.confirm.confirm({
      header: 'Confirm payroll',
      message: 'Confirm disbursement? This creates expense entries, reduces advance balances and locks the run. This cannot be undone.',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.confirming.set(true);
        this.svc.confirmPayroll(this.runId).subscribe({
          next: () => { this.toast.success('Payroll confirmed'); this.confirming.set(false); this.loadRun(); this.loadEntries(); },
          error: () => this.confirming.set(false)
        });
      }
    });
  }

  slip(e: PayrollEntry): void {
    this.downloadingId.set(e.id);
    this.svc.getSalarySlipPdf(e.id, true).subscribe({
      next: blob => {
        this.downloadingId.set(null);
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = `TSSS_Salary_${e.employeeCode}.pdf`; a.click();
        URL.revokeObjectURL(url);
      },
      error: () => { this.downloadingId.set(null); this.toast.error('Could not generate slip.'); }
    });
  }
}
