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
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { TextareaModule } from 'primeng/textarea';
import { InventoryService } from '../inventory.service';
import {
  PurchaseRegisterRow, IssueRegisterRow, ItemLedgerRow, InventoryCategory,
  PurchaseDetailRow, IssueDetailRow
} from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

type Preset = 'today' | 'week' | 'month' | 'year';

@Component({
  selector: 'app-inventory-reports',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, SelectModule, DatePickerModule, InputTextModule, TagModule,
    DialogModule, TooltipModule, TextareaModule
  ],
  templateUrl: './inventory-reports.html'
})
export class InventoryReports implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  canCancel = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);
  /** Show cancelled rows in the registers as well — off by default. */
  includeCancelled = false;

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
      vendorName: this.purchaseVendorName?.trim() || null,
      includeCancelled: this.includeCancelled
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
      issuedByUserId: null,
      includeCancelled: this.includeCancelled
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

  /* ---------- Detail dialogs (Purchase + Issue) ----------
   * Both reuse the existing per-screen pattern: open the dialog, fetch the
   * lines via the dedicated detail endpoint, render header + line table.
   * The Item Ledger drills into one of these based on movementType + sourceMovementId. */

  purchaseDetailOpen = signal(false);
  purchaseDetailHeader = signal<PurchaseDetailRow | null>(null);
  purchaseDetailLines = signal<PurchaseDetailRow[]>([]);
  purchaseDetailLoading = signal(false);

  issueDetailOpen = signal(false);
  issueDetailHeader = signal<IssueDetailRow | null>(null);
  issueDetailLines = signal<IssueDetailRow[]>([]);
  issueDetailLoading = signal(false);

  viewPurchase(purchaseId: number): void {
    this.purchaseDetailHeader.set(null);
    this.purchaseDetailLines.set([]);
    this.purchaseDetailOpen.set(true);
    this.purchaseDetailLoading.set(true);
    this.svc.getPurchaseDetail(purchaseId).subscribe({
      next: r => {
        const lines = r.data ?? [];
        // Header columns are repeated on every line — pull from the first row.
        this.purchaseDetailHeader.set(lines.length ? lines[0] : null);
        this.purchaseDetailLines.set(lines);
        this.purchaseDetailLoading.set(false);
      },
      error: () => this.purchaseDetailLoading.set(false)
    });
  }

  viewIssue(issueId: number): void {
    this.issueDetailHeader.set(null);
    this.issueDetailLines.set([]);
    this.issueDetailOpen.set(true);
    this.issueDetailLoading.set(true);
    this.svc.getIssueDetail(issueId).subscribe({
      next: r => {
        const lines = r.data ?? [];
        this.issueDetailHeader.set(lines.length ? lines[0] : null);
        this.issueDetailLines.set(lines);
        this.issueDetailLoading.set(false);
      },
      error: () => this.issueDetailLoading.set(false)
    });
  }

  /** Item Ledger drill-down — opens the appropriate dialog for Purchase / Issue
   *  rows. Return / Adjustment rows have no dedicated detail SP today, so they
   *  aren't drillable (the table cell shows no eye icon in that case). */
  viewLedgerSource(row: ItemLedgerRow): void {
    if (!row.sourceMovementId) return;
    if (row.movementType === 'Purchase') this.viewPurchase(row.sourceMovementId);
    else if (row.movementType === 'Issue') this.viewIssue(row.sourceMovementId);
  }

  isLedgerDrillable(type?: string): boolean {
    return type === 'Purchase' || type === 'Issue';
  }

  /* ---------- Cancel (admin/principal only) ----------
   * Uses one shared cancel-reason prompt; kind decides which API to hit. */
  cancelOpen = signal(false);
  cancelKind: 'purchase' | 'issue' | null = null;
  cancelTargetId: number | null = null;
  cancelReason = '';
  cancelling = signal(false);

  openCancelPurchase(): void {
    if (!this.purchaseDetailHeader()) return;
    this.cancelKind = 'purchase';
    this.cancelTargetId = this.purchaseDetailHeader()!.purchaseId;
    this.cancelReason = '';
    this.cancelOpen.set(true);
  }

  openCancelIssue(): void {
    if (!this.issueDetailHeader()) return;
    this.cancelKind = 'issue';
    this.cancelTargetId = this.issueDetailHeader()!.issueId;
    this.cancelReason = '';
    this.cancelOpen.set(true);
  }

  submitCancel(): void {
    if (!this.cancelKind || !this.cancelTargetId) return;
    if (!this.cancelReason.trim()) { this.toast.warn('A reason is required.'); return; }
    this.cancelling.set(true);
    const obs = this.cancelKind === 'purchase'
      ? this.svc.cancelPurchase(this.cancelTargetId, this.cancelReason.trim())
      : this.svc.cancelIssue(this.cancelTargetId, this.cancelReason.trim());
    obs.subscribe({
      next: r => {
        this.cancelling.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Cancelled.');
          this.cancelOpen.set(false);
          // close the detail dialog + reload the relevant register
          if (this.cancelKind === 'purchase') { this.purchaseDetailOpen.set(false); this.loadPurchases(); }
          else { this.issueDetailOpen.set(false); this.loadIssues(); }
          // If the ledger tab is showing this item, refresh it too.
          if (this.ledgerItemId) this.loadLedger();
        } else {
          this.toast.error(r.message || 'Could not cancel.');
        }
      },
      error: () => { this.cancelling.set(false); this.toast.error('Could not cancel.'); }
    });
  }
}
