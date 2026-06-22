import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { InventoryService } from '../inventory.service';
import { MyIssuedRow } from '../inventory.models';

type Preset = 'today' | 'week' | 'month' | 'year';

@Component({
  selector: 'app-inventory-my-issued',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, DatePickerModule, ButtonModule, TagModule],
  templateUrl: './inventory-my-issued.html'
})
export class InventoryMyIssued implements OnInit {
  private svc = inject(InventoryService);

  range: Date[] = [];
  rows = signal<MyIssuedRow[]>([]);
  loading = signal(false);

  ngOnInit(): void {
    this.applyPreset('year');
  }

  applyPreset(p: Preset): void {
    const now = new Date();
    let from: Date;
    switch (p) {
      case 'today': from = new Date(now.getFullYear(), now.getMonth(), now.getDate()); break;
      case 'week':  const dow = (now.getDay() + 6) % 7; from = new Date(now.getFullYear(), now.getMonth(), now.getDate() - dow); break;
      case 'month': from = new Date(now.getFullYear(), now.getMonth(), 1); break;
      case 'year':  from = new Date(now.getFullYear(), 0, 1); break;
    }
    this.range = [from, now];
    this.load();
  }

  load(): void {
    const from = this.range?.[0] ?? new Date(new Date().getFullYear(), 0, 1);
    const to = this.range?.[1] ?? new Date();
    this.loading.set(true);
    this.svc.getMyIssued(from.toISOString(), to.toISOString()).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  totalsForLine(r: MyIssuedRow): { remaining: number; pctReturned: number } {
    const remaining = r.remainingQuantity;
    const pctReturned = r.quantity > 0 ? Math.round((r.returnedQuantity / r.quantity) * 100) : 0;
    return { remaining, pctReturned };
  }
}
