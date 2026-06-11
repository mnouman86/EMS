import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { ExpenseService } from '../expense.service';
import { PayrollRun } from '../expense.models';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-payroll-runs',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, InputNumberModule, TagModule, DialogModule],
  templateUrl: './payroll-runs.html'
})
export class PayrollRuns implements OnInit {
  private svc = inject(ExpenseService);
  private toast = inject(ToastService);
  private router = inject(Router);

  monthOptions = [
    { label: 'January', value: 1 }, { label: 'February', value: 2 }, { label: 'March', value: 3 }, { label: 'April', value: 4 },
    { label: 'May', value: 5 }, { label: 'June', value: 6 }, { label: 'July', value: 7 }, { label: 'August', value: 8 },
    { label: 'September', value: 9 }, { label: 'October', value: 10 }, { label: 'November', value: 11 }, { label: 'December', value: 12 }
  ];
  monthName(m: number): string { return this.monthOptions.find(o => o.value === m)?.label ?? String(m); }

  rows = signal<PayrollRun[]>([]);
  loading = signal(false);
  filterYear = new Date().getFullYear();

  dialog = signal(false);
  starting = signal(false);
  startMonth = new Date().getMonth() + 1;
  startYear = new Date().getFullYear();

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getPayrollRuns(this.filterYear).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  open(): void {
    this.startMonth = new Date().getMonth() + 1;
    this.startYear = new Date().getFullYear();
    this.dialog.set(true);
  }

  start(): void {
    this.starting.set(true);
    this.svc.startPayroll({ month: this.startMonth, year: this.startYear }).subscribe({
      next: () => { this.toast.success('Payroll run started'); this.starting.set(false); this.dialog.set(false); this.filterYear = this.startYear; this.load(); },
      error: () => this.starting.set(false)
    });
  }

  open_detail(run: PayrollRun): void { this.router.navigate(['/admin/expenses/payroll', run.id]); }

  statusSeverity(status?: string): 'success' | 'warn' | 'secondary' {
    if (status === 'Confirmed') return 'success';
    if (status === 'Draft') return 'warn';
    return 'secondary';
  }
}
