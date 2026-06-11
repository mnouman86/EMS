import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { TableModule } from 'primeng/table';
import { FinanceService } from '../finance.service';
import { PnLRow } from '../finance.models';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

interface PnLSection { name: string; rows: PnLRow[]; total: number; }

@Component({
  selector: 'app-finance-pnl',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, ButtonModule, DatePickerModule, TableModule],
  templateUrl: './finance-pnl.html'
})
export class FinancePnl implements OnInit {
  private svc = inject(FinanceService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  canExport = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);

  range: Date[] = [];
  loading = signal(false);
  downloading = signal(false);
  loaded = signal(false);
  sections = signal<PnLSection[]>([]);

  income = computed(() => this.sections().find(s => s.name.toLowerCase() === 'income')?.total ?? 0);
  expense = computed(() => this.sections().find(s => s.name.toLowerCase() === 'expense')?.total ?? 0);
  net = computed(() => this.income() - this.expense());

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.load();
  }

  private iso(): { from: string; to: string } | null {
    if (!this.range?.[0] || !this.range?.[1]) return null;
    return { from: this.range[0].toISOString(), to: this.range[1].toISOString() };
  }

  load(): void {
    const r = this.iso();
    if (!r) { this.toast.warn('Pick a date range.'); return; }
    this.loading.set(true);
    this.svc.getPnL(r.from, r.to).subscribe({
      next: res => { this.sections.set(this.group(res.data ?? [])); this.loaded.set(true); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  private group(rows: PnLRow[]): PnLSection[] {
    const order = ['Income', 'Expense'];
    const map = new Map<string, PnLSection>();
    for (const row of rows) {
      const sec = map.get(row.section) ?? { name: row.section, rows: [], total: 0 };
      sec.rows.push(row);
      sec.total += row.amount;
      map.set(row.section, sec);
    }
    return Array.from(map.values()).sort((a, b) => {
      const ai = order.indexOf(a.name), bi = order.indexOf(b.name);
      return (ai === -1 ? 99 : ai) - (bi === -1 ? 99 : bi);
    });
  }

  exportPdf(): void {
    const r = this.iso();
    if (!r) return;
    this.downloading.set(true);
    const label = `${this.range[0].toLocaleDateString()} – ${this.range[1].toLocaleDateString()}`;
    this.svc.getPnLPdf(r.from, r.to, label, this.auth.currentUser()?.userName ?? null).subscribe({
      next: blob => {
        this.downloading.set(false);
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = 'TSSS_PnL.pdf'; a.click();
        URL.revokeObjectURL(url);
      },
      error: () => { this.downloading.set(false); this.toast.error('Could not generate the P&L PDF.'); }
    });
  }
}
