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

interface IssueLineVM { itemId: number | null; quantity: number; stock: number; }

@Component({
  selector: 'app-inventory-issue',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule, DatePickerModule
  ],
  templateUrl: './inventory-issue.html'
})
export class InventoryIssue implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);

  private itemStock = new Map<number, number>();
  itemOptions = signal<{ label: string; value: number }[]>([]);
  typeOptions = ['Class', 'Teacher', 'Department', 'Student'].map(v => ({ label: v, value: v }));

  issueDate: Date = new Date();
  issuedToType = 'Class';
  issuedToName = '';
  issuedToId: number | null = null;
  purpose = '';

  lines = signal<IssueLineVM[]>([{ itemId: null, quantity: 1, stock: 0 }]);
  saving = signal(false);

  ngOnInit(): void {
    this.svc.getItems(defaultSearch()).subscribe(res => {
      const items = (res.data ?? []).filter((i: InventoryItem) => i.isActive);
      items.forEach(i => this.itemStock.set(i.id, i.currentStock));
      this.itemOptions.set(items.map(i => ({ label: `${i.name} (${i.code || '—'}) · stock ${i.currentStock}`, value: i.id })));
    });
  }

  addLine(): void { this.lines.update(ls => [...ls, { itemId: null, quantity: 1, stock: 0 }]); }
  removeLine(idx: number): void { this.lines.update(ls => ls.filter((_, i) => i !== idx)); }

  onItemChange(line: IssueLineVM): void {
    line.stock = line.itemId ? (this.itemStock.get(line.itemId) ?? 0) : 0;
    this.lines.update(ls => [...ls]);
  }

  save(): void {
    if (!this.issuedToName.trim()) { this.toast.warn('Recipient name is required.'); return; }
    const valid = this.lines().filter(l => l.itemId && l.quantity > 0);
    if (valid.length === 0) { this.toast.warn('Add at least one line with an item and quantity.'); return; }
    const over = valid.find(l => l.quantity > (this.itemStock.get(l.itemId!) ?? 0));
    if (over) { this.toast.warn('One or more lines exceed available stock.'); return; }

    this.saving.set(true);
    this.svc.issueItems({
      issueDate: this.issueDate.toISOString(),
      issuedToType: this.issuedToType,
      issuedToId: this.issuedToId,
      issuedToName: this.issuedToName,
      purpose: this.purpose || null,
      lines: valid.map(l => ({ itemId: l.itemId!, quantity: l.quantity }))
    }).subscribe({
      next: () => {
        this.toast.success('Items issued');
        this.saving.set(false);
        this.issuedToName = ''; this.issuedToId = null; this.purpose = '';
        this.lines.set([{ itemId: null, quantity: 1, stock: 0 }]);
      },
      error: () => this.saving.set(false)
    });
  }
}
