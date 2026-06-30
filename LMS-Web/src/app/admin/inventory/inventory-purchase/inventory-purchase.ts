import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { TagModule } from 'primeng/tag';
import { InventoryService } from '../inventory.service';
import { InventoryItem, PurchaseRegisterRow, PurchaseDetailRow } from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { ApiService } from '../../../core/services/api.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

interface PurchaseLineVM { itemId: number | null; quantity: number; unitPrice: number; }

@Component({
  selector: 'app-inventory-purchase',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule, DatePickerModule,
    DialogModule, TooltipModule, TagModule
  ],
  templateUrl: './inventory-purchase.html'
})
export class InventoryPurchase implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private api = inject(ApiService);
  private auth = inject(AuthService);

  /** Cancel a recorded purchase — restricted to oversight roles. */
  canCancel = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);

  /* ---------- History grid (top half) ---------- */
  purchases = signal<PurchaseRegisterRow[]>([]);
  purchasesLoading = signal(false);
  /** Default range = last 90 days → today. */
  range: Date[] = (() => { const to = new Date(); const from = new Date(); from.setDate(to.getDate() - 90); return [from, to]; })();

  detailRow = signal<PurchaseRegisterRow | null>(null);
  detailLines = signal<PurchaseDetailRow[]>([]);
  detailLoading = signal(false);
  detailOpen = signal(false);

  /* Cancel reason prompt */
  cancelOpen = signal(false);
  cancelReason = '';
  cancelling = signal(false);

  /* ---------- Add-new dialog (existing form) ---------- */
  formOpen = signal(false);
  itemOptions = signal<{ label: string; value: number }[]>([]);
  modeOptions = ['Cash', 'Cheque', 'BankTransfer', 'Credit'].map(v => ({ label: v, value: v }));

  purchaseDate: Date = new Date();
  vendorName = '';
  vendorInvoiceNo = '';
  paymentMode = 'Cash';
  notes = '';
  attachmentPath = signal<string | null>(null);
  uploading = signal(false);

  lines = signal<PurchaseLineVM[]>([{ itemId: null, quantity: 1, unitPrice: 0 }]);
  saving = signal(false);

  grandTotal(): number { return this.lines().reduce((s, l) => s + (l.quantity || 0) * (l.unitPrice || 0), 0); }

  ngOnInit(): void {
    this.svc.getItems(defaultSearch()).subscribe(res => {
      this.itemOptions.set((res.data ?? []).filter((i: InventoryItem) => i.isActive)
        .map(i => ({ label: `${i.name} (${i.code || '—'})`, value: i.id })));
    });
    this.loadPurchases();
  }

  /* ---------- Grid actions ---------- */
  loadPurchases(): void {
    if (!this.range?.[0] || !this.range?.[1]) { this.toast.warn('Pick a date range.'); return; }
    const [from, to] = this.range;
    this.purchasesLoading.set(true);
    this.svc.getPurchaseRegister({
      fromDate: this.toIso(from),
      toDate: this.toIso(to)
    }).subscribe({
      next: r => { this.purchases.set(r.data ?? []); this.purchasesLoading.set(false); },
      error: () => this.purchasesLoading.set(false)
    });
  }

  view(row: PurchaseRegisterRow): void {
    this.detailRow.set(row);
    this.detailLines.set([]);
    this.detailOpen.set(true);
    this.detailLoading.set(true);
    this.svc.getPurchaseDetail(row.id).subscribe({
      next: r => { this.detailLines.set(r.data ?? []); this.detailLoading.set(false); },
      error: () => this.detailLoading.set(false)
    });
  }

  /* ---------- Add-new dialog ---------- */
  openForm(): void {
    /* Fresh form every time. */
    this.purchaseDate = new Date();
    this.vendorName = '';
    this.vendorInvoiceNo = '';
    this.paymentMode = 'Cash';
    this.notes = '';
    this.attachmentPath.set(null);
    this.lines.set([{ itemId: null, quantity: 1, unitPrice: 0 }]);
    this.formOpen.set(true);
  }

  addLine(): void { this.lines.update(ls => [...ls, { itemId: null, quantity: 1, unitPrice: 0 }]); }
  removeLine(idx: number): void { this.lines.update(ls => ls.filter((_, i) => i !== idx)); }
  lineTotal(l: PurchaseLineVM): number { return (l.quantity || 0) * (l.unitPrice || 0); }

  onFileSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.uploading.set(true);
    this.api.uploadFile(file).subscribe({
      next: res => { this.attachmentPath.set(res.dbPath); this.uploading.set(false); this.toast.success('Attachment uploaded'); },
      error: () => { this.uploading.set(false); this.toast.error('Upload failed'); }
    });
  }

  save(): void {
    if (!this.vendorName.trim()) { this.toast.warn('Vendor name is required.'); return; }
    const valid = this.lines().filter(l => l.itemId && l.quantity > 0);
    if (valid.length === 0) { this.toast.warn('Add at least one line with an item and quantity.'); return; }
    this.saving.set(true);
    this.svc.recordPurchase({
      purchaseDate: this.purchaseDate.toISOString(),
      vendorName: this.vendorName,
      vendorInvoiceNo: this.vendorInvoiceNo || null,
      paymentMode: this.paymentMode,
      notes: this.notes || null,
      attachmentPath: this.attachmentPath(),
      lines: valid.map(l => ({ itemId: l.itemId!, quantity: l.quantity, unitPrice: l.unitPrice }))
    }).subscribe({
      next: () => {
        this.toast.success('Purchase recorded');
        this.saving.set(false);
        this.formOpen.set(false);
        this.loadPurchases();
      },
      error: () => this.saving.set(false)
    });
  }

  /** Open the cancel-reason prompt for whichever purchase is currently in the detail dialog. */
  openCancel(): void {
    if (!this.detailRow()) return;
    this.cancelReason = '';
    this.cancelOpen.set(true);
  }

  submitCancel(): void {
    const row = this.detailRow();
    if (!row) return;
    if (!this.cancelReason.trim()) { this.toast.warn('A reason is required.'); return; }
    this.cancelling.set(true);
    this.svc.cancelPurchase(row.id, this.cancelReason.trim()).subscribe({
      next: r => {
        this.cancelling.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Purchase cancelled.');
          this.cancelOpen.set(false);
          this.detailOpen.set(false);
          this.loadPurchases();
        } else {
          this.toast.error(r.message || 'Could not cancel the purchase.');
        }
      },
      error: () => { this.cancelling.set(false); this.toast.error('Could not cancel the purchase.'); }
    });
  }

  private toIso(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
