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
import { InventoryService } from '../inventory.service';
import { InventoryItem } from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { ApiService } from '../../../core/services/api.service';

interface PurchaseLineVM { itemId: number | null; quantity: number; unitPrice: number; }

@Component({
  selector: 'app-inventory-purchase',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule, DatePickerModule
  ],
  templateUrl: './inventory-purchase.html'
})
export class InventoryPurchase implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private api = inject(ApiService);

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
      this.itemOptions.set((res.data ?? []).filter((i: InventoryItem) => i.isActive).map(i => ({ label: `${i.name} (${i.code || '—'})`, value: i.id })));
    });
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
        this.vendorName = ''; this.vendorInvoiceNo = ''; this.notes = '';
        this.attachmentPath.set(null);
        this.lines.set([{ itemId: null, quantity: 1, unitPrice: 0 }]);
      },
      error: () => this.saving.set(false)
    });
  }
}
