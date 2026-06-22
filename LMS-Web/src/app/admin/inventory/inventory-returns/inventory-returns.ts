import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { MessageModule } from 'primeng/message';
import { InventoryService } from '../inventory.service';
import { IssueDetailRow } from '../inventory.models';
import { ToastService } from '../../../core/services/toast.service';

interface ReturnLineVM extends IssueDetailRow { returnQty: number; condition: string; }

@Component({
  selector: 'app-inventory-returns',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, DatePickerModule, MessageModule
  ],
  templateUrl: './inventory-returns.html'
})
export class InventoryReturns implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);

  conditionOptions = ['Good', 'Damaged', 'Lost'].map(v => ({ label: v, value: v }));
  issueOptions = signal<{ label: string; value: number }[]>([]);
  issueId: number | null = null;

  loading = signal(false);
  saving = signal(false);
  lines = signal<ReturnLineVM[]>([]);
  returnDate: Date = new Date();
  notes = '';

  ngOnInit(): void {
    // Load the last 12 months of issues to choose from.
    const to = new Date();
    const from = new Date(to.getFullYear(), to.getMonth() - 12, 1);
    this.svc.getIssueRegister({ fromDate: from.toISOString(), toDate: to.toISOString() }).subscribe(res => {
      this.issueOptions.set((res.data ?? []).map(i => ({
        label: `${i.issueCode} · ${i.issuedToName} (${new Date(i.issueDate).toLocaleDateString()})`,
        value: i.id
      })));
    });
  }

  loadIssue(): void {
    if (!this.issueId) return;
    this.loading.set(true);
    this.lines.set([]);
    this.svc.getIssueDetail(this.issueId).subscribe({
      next: res => {
        this.lines.set((res.data ?? []).map(l => ({ ...l, returnQty: 0, condition: 'Good' })));
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  hasReturnable(): boolean { return this.lines().some(l => l.remainingQuantity > 0); }

  save(): void {
    if (!this.issueId) { this.toast.warn('Select an issue.'); return; }
    const toReturn = this.lines().filter(l => l.returnQty > 0);
    if (toReturn.length === 0) { this.toast.warn('Enter a return quantity on at least one line.'); return; }
    const over = toReturn.find(l => l.returnQty > l.remainingQuantity);
    if (over) { this.toast.warn(`Return qty for "${over.itemName}" exceeds the returnable balance.`); return; }

    this.saving.set(true);
    this.svc.recordReturn({
      issueId: this.issueId,
      returnDate: this.returnDate.toISOString(),
      notes: this.notes || null,
      lines: toReturn.map(l => ({ issueLineId: l.issueLineId, itemId: l.itemId, quantity: l.returnQty, condition: l.condition, notes: null }))
    }).subscribe({
      next: () => { this.toast.success('Return recorded'); this.saving.set(false); this.notes = ''; this.loadIssue(); },
      error: () => this.saving.set(false)
    });
  }
}
