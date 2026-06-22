import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { InventoryService } from '../inventory.service';
import {
  PurchaseRegisterRow, IssueRegisterRow, ItemLedgerRow, InventoryCategory
} from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';

type Preset = 'today' | 'week' | 'month' | 'year';

@Component({
  selector: 'app-inventory-reports',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, SelectModule, DatePickerModule, InputTextModule, TagModule
  ],
  templateUrl: './inventory-reports.html'
})
export class InventoryReports implements OnInit {
  private svc = inject(InventoryService);

  itemOptions = signal<{ label: string; value: number }[]>([]);
  categoryOptions = signal<{ label: string; value: number }[]>([]);
  issuedToTypeOptions = [
    { label: 'Any', value: null }, { label: 'Class', value: 'Class' },
    { label: 'Teacher', value: 'Teacher' }, { label: 'Department', value: 'Department' },
    { label: 'Student', value: 'Student' }
  ];

  /* Purchase tab */
  purchaseRange: Date[] = [];
  purchaseCategoryId: number | null = null;
  purchaseVendorName: string | null = null;

  /* Issue tab */
  issueRange: Date[] = [];
  issueCategoryId: number | null = null;
  issueItemId: number | null = null;
  issueIssuedToType: string | null = null;
  issueIssuedToId: number | null = null;

  /* Ledger tab */
  ledgerRange: Date[] = [];
  ledgerItemId: number | null = null;

  purchases = signal<PurchaseRegisterRow[]>([]);
  purchaseLoading = signal(false);
  issues = signal<IssueRegisterRow[]>([]);
  issueLoading = signal(false);
  ledger = signal<ItemLedgerRow[]>([]);
  ledgerLoading = signal(false);

  ngOnInit(): void {
    this.applyPreset('purchaseRange', 'month');
    this.applyPreset('issueRange', 'month');
    this.applyPreset('ledgerRange', 'month');

    this.svc.getItems(defaultSearch()).subscribe(res => {
      this.itemOptions.set((res.data ?? []).map(i => ({ label: `${i.name} (${i.code || '—'})`, value: i.id })));
    });
    this.svc.getCategories().subscribe(res => {
      this.categoryOptions.set((res.data ?? []).map((c: InventoryCategory) => ({ label: c.name, value: c.id })));
    });
    this.loadPurchases();
  }

  /** Sets a range field to a preset window. Auto-reloads the active tab. */
  applyPreset(field: 'purchaseRange' | 'issueRange' | 'ledgerRange', preset: Preset): void {
    const now = new Date();
    let from: Date;
    switch (preset) {
      case 'today':
        from = new Date(now.getFullYear(), now.getMonth(), now.getDate());
        break;
      case 'week':
        // Monday of current week
        const dow = (now.getDay() + 6) % 7;
        from = new Date(now.getFullYear(), now.getMonth(), now.getDate() - dow);
        break;
      case 'month':
        from = new Date(now.getFullYear(), now.getMonth(), 1);
        break;
      case 'year':
        from = new Date(now.getFullYear(), 0, 1);
        break;
    }
    (this as any)[field] = [from, now];
    if (field === 'purchaseRange') this.loadPurchases();
    if (field === 'issueRange') this.loadIssues();
    if (field === 'ledgerRange' && this.ledgerItemId) this.loadLedger();
  }

  private iso(range: Date[]): { from: string; to: string } {
    const from = range?.[0] ?? new Date(new Date().getFullYear(), new Date().getMonth(), 1);
    const to = range?.[1] ?? new Date();
    return { from: from.toISOString(), to: to.toISOString() };
  }

  loadPurchases(): void {
    const r = this.iso(this.purchaseRange);
    this.purchaseLoading.set(true);
    this.svc.getPurchaseRegister({
      fromDate: r.from, toDate: r.to,
      categoryId: this.purchaseCategoryId,
      vendorName: this.purchaseVendorName?.trim() || null
    }).subscribe({
      next: res => { this.purchases.set(res.data ?? []); this.purchaseLoading.set(false); },
      error: () => this.purchaseLoading.set(false)
    });
  }

  loadIssues(): void {
    const r = this.iso(this.issueRange);
    this.issueLoading.set(true);
    this.svc.getIssueRegister({
      fromDate: r.from, toDate: r.to,
      categoryId: this.issueCategoryId,
      itemId: this.issueItemId,
      issuedToType: this.issueIssuedToType,
      issuedToId: this.issueIssuedToId,
      issuedByUserId: null
    }).subscribe({
      next: res => { this.issues.set(res.data ?? []); this.issueLoading.set(false); },
      error: () => this.issueLoading.set(false)
    });
  }

  loadLedger(): void {
    if (!this.ledgerItemId) return;
    const r = this.iso(this.ledgerRange);
    this.ledgerLoading.set(true);
    this.svc.getItemLedger(this.ledgerItemId, r.from, r.to).subscribe({
      next: res => { this.ledger.set(res.data ?? []); this.ledgerLoading.set(false); },
      error: () => this.ledgerLoading.set(false)
    });
  }

  movementSeverity(type?: string): 'success' | 'danger' | 'info' | 'warn' | 'secondary' {
    switch (type) {
      case 'Purchase': return 'success';
      case 'Issue': return 'danger';
      case 'Return': return 'info';
      case 'Adjustment': return 'warn';
      default: return 'secondary';
    }
  }
}
