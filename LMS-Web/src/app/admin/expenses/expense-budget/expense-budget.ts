import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { ExpenseService } from '../expense.service';
import { BudgetMonitoringRow } from '../expense.models';

@Component({
  selector: 'app-expense-budget',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, InputNumberModule, TagModule],
  templateUrl: './expense-budget.html'
})
export class ExpenseBudget implements OnInit {
  private svc = inject(ExpenseService);

  monthOptions = [
    { label: 'January', value: 1 }, { label: 'February', value: 2 }, { label: 'March', value: 3 }, { label: 'April', value: 4 },
    { label: 'May', value: 5 }, { label: 'June', value: 6 }, { label: 'July', value: 7 }, { label: 'August', value: 8 },
    { label: 'September', value: 9 }, { label: 'October', value: 10 }, { label: 'November', value: 11 }, { label: 'December', value: 12 }
  ];
  month = new Date().getMonth() + 1;
  year = new Date().getFullYear();

  rows = signal<BudgetMonitoringRow[]>([]);
  loading = signal(false);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getBudgetMonitoring(this.month, this.year).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  severity(level?: string): 'success' | 'warn' | 'danger' | 'secondary' {
    switch (level) {
      case 'Green': return 'success';
      case 'Amber': return 'warn';
      case 'Red': return 'danger';
      default: return 'secondary';
    }
  }

  barColor(level?: string): string {
    switch (level) {
      case 'Red': return '#dc2626';
      case 'Amber': return '#f59e0b';
      default: return '#16a34a';
    }
  }

  totalBudget(): number { return this.rows().reduce((s, r) => s + (r.budget ?? 0), 0); }
  totalSpent(): number { return this.rows().reduce((s, r) => s + r.spent, 0); }
}
