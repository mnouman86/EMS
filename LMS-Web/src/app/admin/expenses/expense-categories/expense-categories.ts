import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ExpenseService } from '../expense.service';
import { ExpenseCategory } from '../expense.models';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

@Component({
  selector: 'app-expense-categories',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, InputTextModule, InputNumberModule, SelectModule, ToggleSwitchModule,
    DialogModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './expense-categories.html'
})
export class ExpenseCategories implements OnInit {
  private svc = inject(ExpenseService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);
  private auth = inject(AuthService);

  canDelete = this.auth.hasAnyRole([Roles.Admin]);

  rows = signal<ExpenseCategory[]>([]);
  loading = signal(false);
  parentOptions = signal<{ label: string; value: number | null }[]>([{ label: 'None (top-level)', value: null }]);
  private names = new Map<number, string>();

  dialog = signal(false);
  saving = signal(false);
  form: { id?: number | null; name: string; code: string; parentCategoryId: number | null; monthlyBudget: number | null; isActive: boolean } =
    { id: null, name: '', code: '', parentCategoryId: null, monthlyBudget: null, isActive: true };

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getCategories(true).subscribe({
      next: res => {
        const cats = res.data ?? [];
        this.rows.set(cats);
        this.names.clear();
        cats.forEach(c => this.names.set(c.id, c.name));
        this.parentOptions.set([{ label: 'None (top-level)', value: null }, ...cats.map(c => ({ label: c.name, value: c.id as number | null }))]);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  parentName(id?: number | null): string { return id ? (this.names.get(id) ?? `#${id}`) : '—'; }

  open(c?: ExpenseCategory): void {
    this.form = c
      ? { id: c.id, name: c.name, code: c.code, parentCategoryId: c.parentCategoryId ?? null, monthlyBudget: c.monthlyBudget ?? null, isActive: c.isActive }
      : { id: null, name: '', code: '', parentCategoryId: null, monthlyBudget: null, isActive: true };
    this.dialog.set(true);
  }

  save(): void {
    if (!this.form.name.trim() || !this.form.code.trim()) { this.toast.warn('Name and code are required.'); return; }
    this.saving.set(true);
    this.svc.upsertCategory({
      id: this.form.id,
      name: this.form.name,
      code: this.form.code,
      parentCategoryId: this.form.parentCategoryId,
      monthlyBudget: this.form.monthlyBudget,
      isActive: this.form.isActive
    }).subscribe({
      next: () => { this.toast.success('Category saved'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  delete(c: ExpenseCategory): void {
    this.confirm.confirm({
      header: 'Delete category',
      message: `Delete ${c.name}? Categories in use are soft-deleted.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.deleteCategory(c.id).subscribe({ next: () => { this.toast.success('Category deleted'); this.load(); } })
    });
  }
}
