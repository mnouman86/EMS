import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { ChartModule } from 'primeng/chart';
import { FinanceService } from '../finance.service';
import { IncomeSummary, ExpenseSummary, MonthAmount, CategoryAmount } from '../finance.models';
import { AcademicYearService } from '../../academic-years/academic-year.service';
import { defaultSearch } from '../../../core/models/search-request';

interface FinLink { label: string; icon: string; route: string; desc: string; }
interface MonthRow { key: string; label: string; income: number; expense: number; net: number; }

const DONUT_COLORS = ['#3b82f6', '#16a34a', '#f59e0b', '#dc2626', '#8b5cf6', '#0ea5e9', '#ec4899', '#64748b', '#14b8a6', '#f97316'];

@Component({
  selector: 'app-finance-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, ChartModule],
  templateUrl: './finance-dashboard.html'
})
export class FinanceDashboard implements OnInit {
  private svc = inject(FinanceService);
  private yearSvc = inject(AcademicYearService);

  private monthNames = ['', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

  loading = signal(false);
  yearOptions = signal<{ label: string; value: number }[]>([]);
  yearId: number | null = null;

  income = signal<IncomeSummary | null>(null);
  expense = signal<ExpenseSummary | null>(null);
  byMonth = signal<MonthRow[]>([]);
  incomeByClass = signal<CategoryAmount[]>([]);
  incomeByFeeType = signal<CategoryAmount[]>([]);
  expenseByCategory = signal<CategoryAmount[]>([]);

  netSurplus = computed(() => (this.income()?.revenueThisYear ?? 0) - (this.expense()?.expensesThisYear ?? 0));
  maxMonth = computed(() => Math.max(1, ...this.byMonth().map(m => Math.max(m.income, m.expense))));

  monthlyChart = signal<unknown>(null);
  expenseDonut = signal<unknown>(null);
  barOptions: unknown = {
    responsive: true, maintainAspectRatio: false,
    plugins: { legend: { position: 'top' } },
    scales: { y: { beginAtZero: true, ticks: { callback: (v: number) => 'Rs. ' + v.toLocaleString() } } }
  };
  donutOptions: unknown = {
    responsive: true, maintainAspectRatio: false,
    plugins: { legend: { position: 'right' } }
  };

  links: FinLink[] = [
    { label: 'Profit & Loss', icon: 'pi pi-file', route: '/admin/finance/pnl', desc: 'P&L statement + PDF export' },
    { label: 'Reports', icon: 'pi pi-chart-bar', route: '/admin/finance/reports', desc: 'Monthly, annual, fee vs target' },
    { label: 'Cash Flow', icon: 'pi pi-wallet', route: '/admin/finance/cashflow', desc: 'Opening balance & ledger' }
  ];

  ngOnInit(): void {
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      const years = res.data ?? [];
      this.yearOptions.set(years.map(y => ({ label: y.displayName, value: y.id })));
      const open = years.find(y => y.isOpen) ?? years[0];
      if (open) { this.yearId = open.id; this.load(); }
    });
  }

  catTotal(list: CategoryAmount[]): number { return list.reduce((s, c) => s + c.amount, 0); }
  pct(amount: number, total: number): number { return total > 0 ? (amount / total) * 100 : 0; }

  load(): void {
    if (!this.yearId) return;
    const y = this.yearId;
    this.loading.set(true);
    forkJoin({
      income: this.svc.getIncomeSummary(y),
      expense: this.svc.getExpenseSummary(y),
      incMonth: this.svc.getIncomeByMonth(y),
      expMonth: this.svc.getExpenseByMonth(y),
      byClass: this.svc.getIncomeByClass(y),
      byFeeType: this.svc.getIncomeByFeeType(y),
      byCategory: this.svc.getExpenseByCategory(y)
    }).subscribe({
      next: r => {
        this.income.set(r.income.data ?? null);
        this.expense.set(r.expense.data ?? null);
        this.incomeByClass.set(r.byClass.data ?? []);
        this.incomeByFeeType.set(r.byFeeType.data ?? []);
        this.expenseByCategory.set(r.byCategory.data ?? []);
        this.byMonth.set(this.mergeMonths(r.incMonth.data ?? [], r.expMonth.data ?? []));
        this.buildCharts();
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  private buildCharts(): void {
    const rows = this.byMonth();
    this.monthlyChart.set({
      labels: rows.map(r => r.label),
      datasets: [
        { label: 'Income', data: rows.map(r => r.income), backgroundColor: '#16a34a', borderRadius: 4 },
        { label: 'Expense', data: rows.map(r => r.expense), backgroundColor: '#dc2626', borderRadius: 4 }
      ]
    });
    const cats = this.expenseByCategory();
    this.expenseDonut.set({
      labels: cats.map(c => c.name),
      datasets: [{ data: cats.map(c => c.amount), backgroundColor: cats.map((_, i) => DONUT_COLORS[i % DONUT_COLORS.length]) }]
    });
  }

  private mergeMonths(inc: MonthAmount[], exp: MonthAmount[]): MonthRow[] {
    const map = new Map<string, MonthRow>();
    const key = (m: MonthAmount) => `${m.year}-${m.month}`;
    for (const i of inc) {
      const k = key(i);
      map.set(k, { key: k, label: `${this.monthNames[i.month]} ${i.year}`, income: i.amount, expense: 0, net: i.amount });
    }
    for (const e of exp) {
      const k = key(e);
      const row = map.get(k) ?? { key: k, label: `${this.monthNames[e.month]} ${e.year}`, income: 0, expense: 0, net: 0 };
      row.expense = e.amount;
      row.net = row.income - row.expense;
      map.set(k, row);
    }
    return Array.from(map.values()).sort((a, b) => a.key.localeCompare(b.key, undefined, { numeric: true }));
  }
}
