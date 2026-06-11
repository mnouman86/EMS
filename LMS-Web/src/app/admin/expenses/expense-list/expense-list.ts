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
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ExpenseService } from '../expense.service';
import { ExpenseRow, ExpenseCategory } from '../expense.models';
import { defaultSearch, FilterParameter } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { ApiService } from '../../../core/services/api.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

interface ExpLink { label: string; icon: string; route: string; desc: string; }

@Component({
  selector: 'app-expense-list',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule,
    DatePickerModule, DialogModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './expense-list.html'
})
export class ExpenseList implements OnInit {
  private svc = inject(ExpenseService);
  private toast = inject(ToastService);
  private api = inject(ApiService);
  private auth = inject(AuthService);
  private confirm = inject(ConfirmationService);

  canEdit = this.auth.hasAnyRole([Roles.Admin, Roles.Accountant]);
  canDelete = this.auth.hasAnyRole([Roles.Admin]);

  modeOptions = ['Cash', 'Cheque', 'BankTransfer', 'Online', 'Credit'].map(v => ({ label: v, value: v }));

  rows = signal<ExpenseRow[]>([]);
  loading = signal(false);
  categoryOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All categories', value: null }]);
  private editCategoryOptions = signal<{ label: string; value: number }[]>([]);
  catOptionsForEdit = this.editCategoryOptions;

  range: Date[] = [];
  categoryId: number | null = null;
  paymentMode: string | null = null;

  links: ExpLink[] = [
    { label: 'Categories', icon: 'pi pi-tags', route: '/admin/expenses/categories', desc: 'Heads & monthly budgets' },
    { label: 'Recurring', icon: 'pi pi-replay', route: '/admin/expenses/recurring', desc: 'Templates & monthly generation' },
    { label: 'Budget Monitor', icon: 'pi pi-chart-pie', route: '/admin/expenses/budget', desc: 'Budget vs actual' },
    { label: 'Payroll', icon: 'pi pi-money-bill', route: '/admin/expenses/payroll', desc: 'Salary runs & slips' }
  ];

  // record dialog
  dialog = signal(false);
  saving = signal(false);
  uploading = signal(false);
  form: { expenseDate: Date; categoryId: number | null; description: string; amount: number; paymentMode: string; referenceNo: string; paidTo: string; attachmentPath: string | null } =
    { expenseDate: new Date(), categoryId: null, description: '', amount: 0, paymentMode: 'Cash', referenceNo: '', paidTo: '', attachmentPath: null };

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.svc.getCategories().subscribe(res => {
      const cats = (res.data ?? []) as ExpenseCategory[];
      this.categoryOptions.set([{ label: 'All categories', value: null }, ...cats.map(c => ({ label: c.name, value: c.id as number | null }))]);
      this.editCategoryOptions.set(cats.filter(c => c.isActive).map(c => ({ label: c.name, value: c.id })));
    });
    this.load();
  }

  load(): void {
    this.loading.set(true);
    const filters: FilterParameter[] = [];
    if (this.range?.[0]) filters.push({ parameterName: 'From', parameterValue: this.range[0].toISOString() });
    if (this.range?.[1]) filters.push({ parameterName: 'To', parameterValue: this.range[1].toISOString() });
    if (this.categoryId) filters.push({ parameterName: 'CategoryId', parameterValue: String(this.categoryId) });
    if (this.paymentMode) filters.push({ parameterName: 'PaymentMode', parameterValue: this.paymentMode });
    this.svc.getExpenses(defaultSearch({ filterArray: filters })).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  clearFilters(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.categoryId = null;
    this.paymentMode = null;
    this.load();
  }

  open(): void {
    this.form = { expenseDate: new Date(), categoryId: null, description: '', amount: 0, paymentMode: 'Cash', referenceNo: '', paidTo: '', attachmentPath: null };
    this.dialog.set(true);
  }

  onFileSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.uploading.set(true);
    this.api.uploadFile(file).subscribe({
      next: res => { this.form.attachmentPath = res.dbPath; this.uploading.set(false); this.toast.success('Attachment uploaded'); },
      error: () => { this.uploading.set(false); this.toast.error('Upload failed'); }
    });
  }

  save(): void {
    if (!this.form.categoryId) { this.toast.warn('Pick a category.'); return; }
    if (this.form.amount <= 0) { this.toast.warn('Amount must be greater than 0.'); return; }
    this.saving.set(true);
    this.svc.recordExpense({
      expenseDate: this.form.expenseDate.toISOString(),
      categoryId: this.form.categoryId,
      description: this.form.description || null,
      amount: this.form.amount,
      paymentMode: this.form.paymentMode,
      referenceNo: this.form.referenceNo || null,
      paidTo: this.form.paidTo || null,
      attachmentPath: this.form.attachmentPath
    }).subscribe({
      next: () => { this.toast.success('Expense recorded'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  delete(row: ExpenseRow): void {
    this.confirm.confirm({
      header: 'Delete expense',
      message: `Delete ${row.expenseCode} (Rs. ${row.amount.toLocaleString()})?`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.deleteExpense(row.id).subscribe({ next: () => { this.toast.success('Expense deleted'); this.load(); } })
    });
  }

  total(): number { return this.rows().reduce((s, r) => s + r.amount, 0); }
}
