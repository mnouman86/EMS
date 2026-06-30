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
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { StudentService } from './student.service';
import { StudentListItem } from './student.models';
import { SchoolClassService } from '../admin/classes/school-class.service';
import { defaultSearch, FilterParameter } from '../core/models/search-request';
import { ToastService } from '../core/services/toast.service';
import { HasPermissionDirective } from '../core/directives/has-permission.directive';

@Component({
  selector: 'app-students',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, InputTextModule, SelectModule, TagModule, TooltipModule,
    DialogModule, ConfirmDialogModule, HasPermissionDirective
  ],
  providers: [ConfirmationService],
  templateUrl: './students.html'
})
export class Students implements OnInit {
  private svc = inject(StudentService);
  private classSvc = inject(SchoolClassService);
  private router = inject(Router);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<StudentListItem[]>([]);
  loading = signal(false);

  search = '';
  statusFilter: string | null = null;
  classFilter: number | null = null;

  classOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All classes', value: null }]);
  statusOptions = [
    { label: 'All statuses', value: null },
    { label: 'Applied', value: 'Applied' },
    { label: 'Admitted', value: 'Admitted' },
    { label: 'Active', value: 'Active' },
    { label: 'Left', value: 'Left' },
    { label: 'Alumni', value: 'Alumni' },
    { label: 'Withdrawn', value: 'Withdrawn' }
  ];

  // Change-status dialog
  statusDialog = signal(false);
  statusSaving = signal(false);
  statusTarget = signal<StudentListItem | null>(null);
  targetStatus: string | null = null;
  targetClassId: number | null = null;
  lifecycleReason = '';

  // statuses a user can transition into
  transitionOptions = [
    { label: 'Admitted', value: 'Admitted' },
    { label: 'Active', value: 'Active' },
    { label: 'Left', value: 'Left' },
    { label: 'Alumni', value: 'Alumni' },
    { label: 'Withdrawn', value: 'Withdrawn' }
  ];

  ngOnInit(): void {
    this.loadClasses();
    this.load();
  }

  private loadClasses(): void {
    this.classSvc.getAll(defaultSearch()).subscribe({
      next: res => {
        const active = (res.data ?? []).filter(c => c.isActive);
        this.classOptions.set([{ label: 'All classes', value: null }, ...active.map(c => ({ label: c.levelName, value: c.id as number | null }))]);
      }
    });
  }

  load(): void {
    this.loading.set(true);
    const filters: FilterParameter[] = [];
    if (this.search.trim()) filters.push({ parameterName: 'Search', parameterValue: this.search.trim() });
    if (this.statusFilter) filters.push({ parameterName: 'Status', parameterValue: this.statusFilter });
    if (this.classFilter) filters.push({ parameterName: 'ClassId', parameterValue: String(this.classFilter) });

    this.svc.getAll(defaultSearch({ filterArray: filters })).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  clearFilters(): void {
    this.search = '';
    this.statusFilter = null;
    this.classFilter = null;
    this.load();
  }

  statusSeverity(status?: string): 'success' | 'info' | 'warn' | 'secondary' | 'danger' {
    switch (status) {
      case 'Active': return 'success';
      case 'Admitted': return 'info';
      case 'Applied': return 'warn';
      case 'Withdrawn': return 'danger';
      default: return 'secondary'; // Left / Alumni
    }
  }

  view(row: StudentListItem): void { this.router.navigate(['/admin/students', row.id]); }
  edit(row: StudentListItem): void { this.router.navigate(['/admin/students', row.id, 'edit']); }
  create(): void { this.router.navigate(['/admin/students/new']); }
  goImport(): void { this.router.navigate(['/admin/students/import']); }
  goPromote(): void { this.router.navigate(['/admin/students/promote']); }

  openStatus(row: StudentListItem): void {
    this.statusTarget.set(row);
    this.targetStatus = null;
    this.targetClassId = row.admittedClassId ?? null;
    this.lifecycleReason = '';
    this.statusDialog.set(true);
  }

  confirmStatus(): void {
    const target = this.statusTarget();
    if (!target || !this.targetStatus) { this.toast.warn('Select a target status.'); return; }
    if ((this.targetStatus === 'Admitted' || this.targetStatus === 'Active') && !this.targetClassId) {
      this.toast.warn('Pick the class to admit the student into.'); return;
    }
    this.statusSaving.set(true);
    this.svc.changeStatus({
      id: target.id,
      targetStatus: this.targetStatus,
      admittedClassId: this.targetClassId,
      lifecycleReason: this.lifecycleReason || null
    }).subscribe({
      next: () => { this.toast.success(`Student moved to ${this.targetStatus}`); this.statusSaving.set(false); this.statusDialog.set(false); this.load(); },
      error: () => this.statusSaving.set(false)
    });
  }

  archive(row: StudentListItem): void {
    this.confirm.confirm({
      header: 'Archive student',
      message: `Archive ${row.fullName}? This soft-deletes the record.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.svc.archive(row.id).subscribe({
          next: () => { this.toast.success('Student archived'); this.load(); }
        });
      }
    });
  }
}
