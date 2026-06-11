import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { InventoryService } from '../inventory.service';
import { LowStockAlert } from '../inventory.models';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-inventory-alerts',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, InputNumberModule, InputTextModule, TagModule, DialogModule],
  templateUrl: './inventory-alerts.html'
})
export class InventoryAlerts implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);

  loading = signal(false);
  rows = signal<LowStockAlert[]>([]);

  dialog = signal(false);
  saving = signal(false);
  target = signal<LowStockAlert | null>(null);
  snoozeDays = 7;
  reason = '';

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getLowStockAlerts().subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  isSnoozed(a: LowStockAlert): boolean {
    return !!a.snoozedUntil && new Date(a.snoozedUntil).getTime() > Date.now();
  }

  openSnooze(a: LowStockAlert): void {
    this.target.set(a);
    this.snoozeDays = 7;
    this.reason = '';
    this.dialog.set(true);
  }

  save(): void {
    const t = this.target();
    if (!t) return;
    if (this.snoozeDays < 1 || this.snoozeDays > 60) { this.toast.warn('Snooze must be between 1 and 60 days.'); return; }
    if (!this.reason.trim()) { this.toast.warn('Reason is required.'); return; }
    this.saving.set(true);
    this.svc.snoozeAlert({ itemId: t.itemId, snoozeForDays: this.snoozeDays, reason: this.reason }).subscribe({
      next: () => { this.toast.success('Alert snoozed'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }
}
