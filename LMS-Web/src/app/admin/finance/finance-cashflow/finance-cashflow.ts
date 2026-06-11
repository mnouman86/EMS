import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { FinanceService } from '../finance.service';
import { OpeningCashBalance, CashFlowLedgerRow } from '../finance.models';
import { AcademicYearService } from '../../academic-years/academic-year.service';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

@Component({
  selector: 'app-finance-cashflow',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, DatePickerModule, DialogModule, TagModule
  ],
  templateUrl: './finance-cashflow.html'
})
export class FinanceCashflow implements OnInit {
  private svc = inject(FinanceService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  canSetOpening = this.auth.hasAnyRole([Roles.Admin]);

  yearOptions = signal<{ label: string; value: number }[]>([]);
  yearId: number | null = null;
  range: Date[] = [];

  opening = signal<OpeningCashBalance | null>(null);
  rows = signal<CashFlowLedgerRow[]>([]);
  loading = signal(false);

  dialog = signal(false);
  saving = signal(false);
  openingAmount = 0;
  asOfDate: Date = new Date();

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      const years = res.data ?? [];
      this.yearOptions.set(years.map(y => ({ label: y.displayName, value: y.id })));
      const open = years.find(y => y.isOpen) ?? years[0];
      if (open) { this.yearId = open.id; this.loadOpening(); this.load(); }
    });
  }

  loadOpening(): void {
    if (!this.yearId) return;
    this.svc.getOpeningBalance(this.yearId).subscribe({ next: res => this.opening.set(res.data ?? null) });
  }

  load(): void {
    if (!this.yearId || !this.range?.[0] || !this.range?.[1]) return;
    this.loading.set(true);
    this.svc.getCashFlowLedger(this.range[0].toISOString(), this.range[1].toISOString(), this.yearId).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  onYearChange(): void { this.loadOpening(); this.load(); }

  openSet(): void {
    const o = this.opening();
    this.openingAmount = o?.openingAmount ?? 0;
    this.asOfDate = o?.asOfDate ? new Date(o.asOfDate) : new Date();
    this.dialog.set(true);
  }

  saveOpening(): void {
    if (!this.yearId) return;
    this.saving.set(true);
    this.svc.setOpeningBalance({ academicYearId: this.yearId, openingAmount: this.openingAmount, asOfDate: this.asOfDate.toISOString() }).subscribe({
      next: () => { this.toast.success('Opening balance saved'); this.saving.set(false); this.dialog.set(false); this.loadOpening(); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  closingBalance(): number {
    const r = this.rows();
    return r.length ? r[r.length - 1].runningBalance : (this.opening()?.openingAmount ?? 0);
  }

  totalIn(): number { return this.rows().reduce((s, r) => s + r.moneyIn, 0); }
  totalOut(): number { return this.rows().reduce((s, r) => s + r.moneyOut, 0); }
}
