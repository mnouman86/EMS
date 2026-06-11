import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { SchoolClassService } from './school-class.service';
import { SchoolClass } from './school-class.models';
import { EmployeeService } from '../employees/employee.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-classes',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    TableModule, ButtonModule, DialogModule, InputTextModule, InputNumberModule,
    SelectModule, ToggleSwitchModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './classes.html'
})
export class Classes implements OnInit {
  private svc = inject(SchoolClassService);
  private employeeSvc = inject(EmployeeService);
  private fb = inject(FormBuilder);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<SchoolClass[]>([]);
  loading = signal(false);
  dialogOpen = signal(false);
  saving = signal(false);
  editingId = signal<number | null>(null);
  teacherOptions = signal<{ label: string; value: number }[]>([]);

  form = this.fb.nonNullable.group({
    levelName: ['', [Validators.required, Validators.maxLength(100)]],
    levelCode: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(4), Validators.pattern(/^[A-Za-z]+$/)]],
    gradeNumber: [null as number | null],
    displayOrder: [0, [Validators.required, Validators.min(0)]],
    capacity: [null as number | null],
    classTeacherId: [null as number | null],
    isActive: [true]
  });

  get isEditing(): boolean {
    return this.editingId() !== null;
  }

  ngOnInit(): void {
    this.load();
    this.employeeSvc.getAll(defaultSearch()).subscribe(res => {
      const active = (res.data ?? []).filter(e => e.status !== 'Left');
      this.teacherOptions.set(active.map(e => ({ label: `${e.fullName} (${e.employeeCode})`, value: e.id })));
    });
  }

  load(): void {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openCreate(): void {
    this.editingId.set(null);
    this.form.reset({ levelName: '', levelCode: '', gradeNumber: null, displayOrder: 0, capacity: null, classTeacherId: null, isActive: true });
    this.form.controls.levelCode.enable();
    this.dialogOpen.set(true);
  }

  openEdit(row: SchoolClass): void {
    this.editingId.set(row.id);
    this.form.reset({
      levelName: row.levelName,
      levelCode: row.levelCode,
      gradeNumber: row.gradeNumber ?? null,
      displayOrder: row.displayOrder,
      capacity: row.capacity ?? null,
      classTeacherId: row.classTeacherId ?? null,
      isActive: row.isActive
    });
    this.form.controls.levelCode.disable(); // immutable once the class exists
    this.dialogOpen.set(true);
  }

  save(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving.set(true);
    const v = this.form.getRawValue();
    const done = (msg: string) => { this.toast.success(msg); this.saving.set(false); this.dialogOpen.set(false); this.load(); };
    const fail = () => this.saving.set(false);

    if (this.isEditing) {
      this.svc.update({
        id: this.editingId()!, levelName: v.levelName, gradeNumber: v.gradeNumber,
        displayOrder: v.displayOrder, capacity: v.capacity, classTeacherId: v.classTeacherId, isActive: v.isActive
      }).subscribe({ next: () => done('Class updated'), error: fail });
    } else {
      this.svc.create({
        levelName: v.levelName, levelCode: v.levelCode.toUpperCase(), gradeNumber: v.gradeNumber,
        displayOrder: v.displayOrder, capacity: v.capacity, classTeacherId: v.classTeacherId, isActive: v.isActive
      }).subscribe({ next: () => done('Class created'), error: fail });
    }
  }

  archive(row: SchoolClass): void {
    this.confirm.confirm({
      message: `Archive "${row.levelName}"? It is hidden from new admissions but kept on historical records.`,
      header: 'Archive class',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.archive(row.id).subscribe({ next: () => { this.toast.success('Class archived'); this.load(); } })
    });
  }
}
