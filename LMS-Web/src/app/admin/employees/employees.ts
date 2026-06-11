import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { DatePickerModule } from 'primeng/datepicker';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { EmployeeService } from './employee.service';
import { EmployeeListItem } from './employee.models';
import { defaultSearch, FilterParameter } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, InputTextModule, SelectModule, TagModule, TooltipModule,
    DialogModule, DatePickerModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './employees.html'
})
export class Employees implements OnInit {
  private svc = inject(EmployeeService);
  private router = inject(Router);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<EmployeeListItem[]>([]);
  loading = signal(false);

  search = '';
  statusFilter: string | null = null;
  employmentFilter: string | null = null;

  statusOptions = [
    { label: 'All statuses', value: null },
    { label: 'Active', value: 'Active' },
    { label: 'On Leave', value: 'OnLeave' },
    { label: 'Left', value: 'Left' }
  ];
  employmentOptions = [
    { label: 'All types', value: null },
    { label: 'Full-Time', value: 'Full-Time' },
    { label: 'Part-Time', value: 'Part-Time' },
    { label: 'Visiting', value: 'Visiting' }
  ];

  // Mark-left dialog
  leftDialog = signal(false);
  leftSaving = signal(false);
  leftTarget = signal<EmployeeListItem | null>(null);
  leftDate: Date | null = null;
  leftReason = '';

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    const filters: FilterParameter[] = [];
    if (this.search.trim()) filters.push({ parameterName: 'Search', parameterValue: this.search.trim() });
    if (this.statusFilter) filters.push({ parameterName: 'Status', parameterValue: this.statusFilter });
    if (this.employmentFilter) filters.push({ parameterName: 'EmploymentType', parameterValue: this.employmentFilter });

    this.svc.getAll(defaultSearch({ filterArray: filters })).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  clearFilters(): void {
    this.search = '';
    this.statusFilter = null;
    this.employmentFilter = null;
    this.load();
  }

  statusSeverity(status?: string): 'success' | 'warn' | 'secondary' {
    if (status === 'Active') return 'success';
    if (status === 'OnLeave') return 'warn';
    return 'secondary';
  }

  view(row: EmployeeListItem): void {
    this.router.navigate(['/admin/employees', row.id]);
  }

  edit(row: EmployeeListItem): void {
    this.router.navigate(['/admin/employees', row.id, 'edit']);
  }

  create(): void {
    this.router.navigate(['/admin/employees/new']);
  }

  openMarkLeft(row: EmployeeListItem): void {
    this.leftTarget.set(row);
    this.leftDate = new Date();
    this.leftReason = '';
    this.leftDialog.set(true);
  }

  confirmMarkLeft(): void {
    const target = this.leftTarget();
    if (!target || !this.leftDate) { this.toast.warn('Pick the last working day.'); return; }
    this.leftSaving.set(true);
    this.svc.markLeft({
      id: target.id,
      lastWorkingDay: this.leftDate.toISOString(),
      reasonForLeaving: this.leftReason || null
    }).subscribe({
      next: () => { this.toast.success('Employee marked as Left'); this.leftSaving.set(false); this.leftDialog.set(false); this.load(); },
      error: () => this.leftSaving.set(false)
    });
  }
}
