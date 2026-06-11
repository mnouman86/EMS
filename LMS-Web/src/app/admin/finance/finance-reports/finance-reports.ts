import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { DatePickerModule } from 'primeng/datepicker';
import { TagModule } from 'primeng/tag';
import { FinanceService } from '../finance.service';
import { PnLRow, CategoryAmount, FeeCollectionVsTarget } from '../finance.models';
import { AcademicYearService } from '../../academic-years/academic-year.service';
import { defaultSearch } from '../../../core/models/search-request';

@Component({
  selector: 'app-finance-reports',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, SelectModule, InputNumberModule, DatePickerModule, TagModule
  ],
  templateUrl: './finance-reports.html'
})
export class FinanceReports implements OnInit {
  private svc = inject(FinanceService);
  private yearSvc = inject(AcademicYearService);

  private monthNames = ['', 'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  monthOptions = this.monthNames.slice(1).map((label, i) => ({ label, value: i + 1 }));
  monthName(m: number): string { return this.monthNames[m] ?? String(m); }

  yearOptions = signal<{ label: string; value: number }[]>([]);

  // monthly
  mMonth = new Date().getMonth() + 1;
  mYear = new Date().getFullYear();
  monthly = signal<PnLRow[]>([]);
  monthlyLoading = signal(false);

  // annual
  annualYearId: number | null = null;
  annual = signal<PnLRow[]>([]);
  annualLoading = signal(false);

  // category-wise expense
  catRange: Date[] = [];
  catExpense = signal<CategoryAmount[]>([]);
  catLoading = signal(false);

  // fee vs target
  feeYearId: number | null = null;
  feeVsTarget = signal<FeeCollectionVsTarget[]>([]);
  feeLoading = signal(false);

  ngOnInit(): void {
    const now = new Date();
    this.catRange = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      const years = res.data ?? [];
      this.yearOptions.set(years.map(y => ({ label: y.displayName, value: y.id })));
      const open = years.find(y => y.isOpen) ?? years[0];
      if (open) { this.annualYearId = open.id; this.feeYearId = open.id; }
    });
    this.loadMonthly();
  }

  net(rows: PnLRow[]): number {
    const inc = rows.filter(r => r.section?.toLowerCase() === 'income').reduce((s, r) => s + r.amount, 0);
    const exp = rows.filter(r => r.section?.toLowerCase() === 'expense').reduce((s, r) => s + r.amount, 0);
    return inc - exp;
  }

  loadMonthly(): void {
    this.monthlyLoading.set(true);
    this.svc.getMonthlySummary(this.mMonth, this.mYear).subscribe({
      next: res => { this.monthly.set(res.data ?? []); this.monthlyLoading.set(false); },
      error: () => this.monthlyLoading.set(false)
    });
  }

  loadAnnual(): void {
    if (!this.annualYearId) return;
    this.annualLoading.set(true);
    this.svc.getAnnualSummary(this.annualYearId).subscribe({
      next: res => { this.annual.set(res.data ?? []); this.annualLoading.set(false); },
      error: () => this.annualLoading.set(false)
    });
  }

  loadCategory(): void {
    if (!this.catRange?.[0] || !this.catRange?.[1]) return;
    this.catLoading.set(true);
    this.svc.getCategoryWiseExpense(this.catRange[0].toISOString(), this.catRange[1].toISOString()).subscribe({
      next: res => { this.catExpense.set(res.data ?? []); this.catLoading.set(false); },
      error: () => this.catLoading.set(false)
    });
  }

  loadFeeVsTarget(): void {
    if (!this.feeYearId) return;
    this.feeLoading.set(true);
    this.svc.getFeeCollectionVsTarget(this.feeYearId).subscribe({
      next: res => { this.feeVsTarget.set(res.data ?? []); this.feeLoading.set(false); },
      error: () => this.feeLoading.set(false)
    });
  }

  catTotal(): number { return this.catExpense().reduce((s, c) => s + c.amount, 0); }

  achievementSeverity(pct: number): 'success' | 'warn' | 'danger' {
    if (pct >= 90) return 'success';
    if (pct >= 70) return 'warn';
    return 'danger';
  }
}
