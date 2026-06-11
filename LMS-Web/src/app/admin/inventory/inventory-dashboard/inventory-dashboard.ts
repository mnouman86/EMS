import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { InventoryService } from '../inventory.service';
import { StockDashboardRow, InventoryCategory } from '../inventory.models';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

interface InvLink { label: string; icon: string; route: string; desc: string; }

@Component({
  selector: 'app-inventory-dashboard',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, DatePickerModule, InputNumberModule, InputTextModule, TagModule, DialogModule
  ],
  templateUrl: './inventory-dashboard.html'
})
export class InventoryDashboard implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  loading = signal(false);
  rows = signal<StockDashboardRow[]>([]);
  alertCount = signal(0);

  categoryOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All categories', value: null }]);
  statusOptions = [
    { label: 'All', value: null },
    { label: 'OK', value: 'OK' },
    { label: 'Low', value: 'Low' },
    { label: 'Out', value: 'Out' }
  ];
  categoryId: number | null = null;
  status: string | null = null;
  range: Date[] = [];

  canAdjust = this.auth.hasAnyRole([Roles.Admin]);

  // adjust dialog
  itemOptions = signal<{ label: string; value: number }[]>([]);
  adjustDialog = signal(false);
  adjustSaving = signal(false);
  adjust = { itemId: null as number | null, adjustmentType: 'Increase', quantity: 0, reason: '' };

  links: InvLink[] = [
    { label: 'Catalogue', icon: 'pi pi-tags', route: '/admin/inventory/catalogue', desc: 'Categories & items' },
    { label: 'Record Purchase', icon: 'pi pi-shopping-cart', route: '/admin/inventory/purchase', desc: 'Stock in from vendors' },
    { label: 'Issue Items', icon: 'pi pi-send', route: '/admin/inventory/issue', desc: 'Stock out to classes/staff' },
    { label: 'Record Return', icon: 'pi pi-undo', route: '/admin/inventory/returns', desc: 'Return issued items to stock' },
    { label: 'Low-Stock Alerts', icon: 'pi pi-exclamation-triangle', route: '/admin/inventory/alerts', desc: 'Reorder reminders' },
    { label: 'Reports', icon: 'pi pi-chart-bar', route: '/admin/inventory/reports', desc: 'Registers & item ledger' }
  ];

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.svc.getCategories().subscribe(res => {
      const cats = (res.data ?? []) as InventoryCategory[];
      this.categoryOptions.set([{ label: 'All categories', value: null }, ...cats.map(c => ({ label: c.name, value: c.id as number | null }))]);
    });
    this.svc.getItems().subscribe(res => {
      this.itemOptions.set((res.data ?? []).map(i => ({ label: `${i.name} (${i.code || '—'})`, value: i.id })));
    });
    this.svc.getLowStockAlerts().subscribe(res => this.alertCount.set((res.data ?? []).length));
    this.load();
  }

  private rangeIso(): { from: string; to: string } {
    const from = this.range?.[0] ?? new Date(new Date().getFullYear(), new Date().getMonth(), 1);
    const to = this.range?.[1] ?? new Date();
    return { from: from.toISOString(), to: to.toISOString() };
  }

  load(): void {
    this.loading.set(true);
    const r = this.rangeIso();
    this.svc.getStockDashboard(this.categoryId, this.status, r.from, r.to).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  statusSeverity(status?: string): 'success' | 'warn' | 'danger' | 'secondary' {
    if (status === 'OK') return 'success';
    if (status === 'Low') return 'warn';
    if (status === 'Out') return 'danger';
    return 'secondary';
  }

  openAdjust(): void {
    this.adjust = { itemId: null, adjustmentType: 'Increase', quantity: 0, reason: '' };
    this.adjustDialog.set(true);
  }

  saveAdjust(): void {
    if (!this.adjust.itemId) { this.toast.warn('Select an item.'); return; }
    if (this.adjust.quantity <= 0) { this.toast.warn('Quantity must be greater than 0.'); return; }
    if (!this.adjust.reason.trim()) { this.toast.warn('Reason is required.'); return; }
    this.adjustSaving.set(true);
    this.svc.recordAdjustment({
      itemId: this.adjust.itemId,
      adjustmentType: this.adjust.adjustmentType,
      quantity: this.adjust.quantity,
      reason: this.adjust.reason
    }).subscribe({
      next: () => { this.toast.success('Stock adjusted'); this.adjustSaving.set(false); this.adjustDialog.set(false); this.load(); },
      error: () => this.adjustSaving.set(false)
    });
  }
}
