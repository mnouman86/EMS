import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { ChartModule } from 'primeng/chart';
import { FeeService } from '../fee.service';
import { CollectionSummary, CollectionByClass, CollectionByDay } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';

interface FeeLink { label: string; icon: string; route: string; desc: string; }

@Component({
  selector: 'app-fee-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, DatePickerModule, ChartModule],
  templateUrl: './fee-dashboard.html'
})
export class FeeDashboard implements OnInit {
  private svc = inject(FeeService);
  private classSvc = inject(SchoolClassService);

  loading = signal(false);
  summary = signal<CollectionSummary | null>(null);
  byClass = signal<CollectionByClass[]>([]);
  byDay = signal<CollectionByDay[]>([]);

  classOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All classes', value: null }]);
  classId: number | null = null;
  range: Date[] = [];

  byClassChart = signal<unknown>(null);
  chartOptions: unknown = {
    responsive: true, maintainAspectRatio: false,
    plugins: { legend: { position: 'top' } },
    scales: { y: { beginAtZero: true, ticks: { callback: (v: number) => 'Rs. ' + v.toLocaleString() } } }
  };

  links: FeeLink[] = [
    { label: 'Collect Payment', icon: 'pi pi-wallet', route: '/admin/fees/collect', desc: 'Record a fee payment & print receipt' },
    { label: 'Student Ledger', icon: 'pi pi-book', route: '/admin/fees/ledger', desc: 'Invoices, payments & running balance' },
    { label: 'Generate Invoices', icon: 'pi pi-file-plus', route: '/admin/fees/generate', desc: 'Monthly billing run with preview' },
    { label: 'Reports', icon: 'pi pi-chart-bar', route: '/admin/fees/reports', desc: 'Defaulters, non-submitted, ageing' },
    { label: 'Concessions', icon: 'pi pi-percentage', route: '/admin/fees/concessions', desc: 'Waivers & scholarships' },
    { label: 'Reminders', icon: 'pi pi-send', route: '/admin/fees/reminders', desc: 'WhatsApp / SMS fee reminders' },
    { label: 'Arrears', icon: 'pi pi-history', route: '/admin/fees/arrears', desc: 'Carry forward & write off' },
    { label: 'Fee Setup', icon: 'pi pi-cog', route: '/admin/fees/setup', desc: 'Types, structure & calendar' }
  ];

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.classSvc.getAll(defaultSearch()).subscribe({
      next: res => {
        const active = (res.data ?? []).filter(c => c.isActive);
        this.classOptions.set([{ label: 'All classes', value: null }, ...active.map(c => ({ label: c.levelName, value: c.id as number | null }))]);
      }
    });
    this.load();
  }

  private rangeIso(): { from: string; to: string } | null {
    if (!this.range?.[0] || !this.range?.[1]) return null;
    return { from: this.range[0].toISOString(), to: this.range[1].toISOString() };
  }

  load(): void {
    const r = this.rangeIso();
    if (!r) return;
    this.loading.set(true);
    forkJoin({
      summary: this.svc.getCollectionSummary(r.from, r.to, this.classId),
      byClass: this.svc.getCollectionByClass(r.from, r.to),
      byDay: this.svc.getCollectionByDay(r.from, r.to)
    }).subscribe({
      next: ({ summary, byClass, byDay }) => {
        this.summary.set(summary.data ?? null);
        const classes = byClass.data ?? [];
        this.byClass.set(classes);
        this.byDay.set(byDay.data ?? []);
        this.byClassChart.set({
          labels: classes.map(c => c.className),
          datasets: [
            { label: 'Collected', data: classes.map(c => c.collected), backgroundColor: '#16a34a', borderRadius: 4 },
            { label: 'Pending', data: classes.map(c => c.pending), backgroundColor: '#dc2626', borderRadius: 4 }
          ]
        });
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }
}
