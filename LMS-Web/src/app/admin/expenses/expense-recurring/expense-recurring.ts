import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ExpenseService } from '../expense.service';
import { RecurringTemplate, ExpenseCategory } from '../expense.models';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-expense-recurring',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, InputTextModule, InputNumberModule, TextareaModule, SelectModule,
    ToggleSwitchModule, DatePickerModule, DialogModule, TagModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './expense-recurring.html'
})
export class ExpenseRecurring implements OnInit {
  private svc = inject(ExpenseService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  modeOptions = ['Cash', 'Cheque', 'BankTransfer', 'Online', 'Credit'].map(v => ({ label: v, value: v }));

  rows = signal<RecurringTemplate[]>([]);
  loading = signal(false);
  categoryOptions = signal<{ label: string; value: number }[]>([]);
  private catNames = new Map<number, string>();

  dialog = signal(false);
  saving = signal(false);
  generating = signal(false);
  generateDate: Date = new Date();

  form: { id?: number | null; name: string; categoryId: number | null; amount: number; defaultPaymentMode: string; paidTo: string; description: string; isActive: boolean } =
    { id: null, name: '', categoryId: null, amount: 0, defaultPaymentMode: 'BankTransfer', paidTo: '', description: '', isActive: true };

  ngOnInit(): void {
    this.svc.getCategories().subscribe(res => {
      const cats = (res.data ?? []) as ExpenseCategory[];
      this.categoryOptions.set(cats.filter(c => c.isActive).map(c => ({ label: c.name, value: c.id })));
      cats.forEach(c => this.catNames.set(c.id, c.name));
    });
    this.load();
  }

  catName(id: number): string { return this.catNames.get(id) ?? `#${id}`; }

  load(): void {
    this.loading.set(true);
    this.svc.getRecurringTemplates().subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  open(t?: RecurringTemplate): void {
    this.form = t
      ? { id: t.id, name: t.name, categoryId: t.categoryId, amount: t.amount, defaultPaymentMode: t.defaultPaymentMode || 'BankTransfer', paidTo: t.paidTo || '', description: t.description || '', isActive: t.isActive }
      : { id: null, name: '', categoryId: null, amount: 0, defaultPaymentMode: 'BankTransfer', paidTo: '', description: '', isActive: true };
    this.dialog.set(true);
  }

  save(): void {
    if (!this.form.name.trim()) { this.toast.warn('Name is required.'); return; }
    if (!this.form.categoryId) { this.toast.warn('Pick a category.'); return; }
    if (this.form.amount <= 0) { this.toast.warn('Amount must be greater than 0.'); return; }
    this.saving.set(true);
    this.svc.upsertRecurringTemplate({
      id: this.form.id,
      name: this.form.name,
      categoryId: this.form.categoryId,
      amount: this.form.amount,
      defaultPaymentMode: this.form.defaultPaymentMode,
      paidTo: this.form.paidTo || null,
      description: this.form.description || null,
      isActive: this.form.isActive
    }).subscribe({
      next: () => { this.toast.success('Template saved'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  generate(): void {
    this.confirm.confirm({
      header: 'Generate recurring expenses',
      message: `Create expense entries from all active templates dated ${this.generateDate.toLocaleDateString()}? Duplicate runs for the same month are skipped.`,
      icon: 'pi pi-info-circle',
      accept: () => {
        this.generating.set(true);
        this.svc.generateRecurring({ expenseDate: this.generateDate.toISOString() }).subscribe({
          next: () => { this.toast.success('Recurring expenses generated'); this.generating.set(false); },
          error: () => this.generating.set(false)
        });
      }
    });
  }
}
