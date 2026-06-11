import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { AcademicYearService } from './academic-year.service';
import { AcademicYear } from './academic-year.models';
import { ToastService } from '../../core/services/toast.service';

function endAfterStart(group: AbstractControl): ValidationErrors | null {
  const s = group.get('startDate')?.value as Date | null;
  const e = group.get('endDate')?.value as Date | null;
  return s && e && e <= s ? { dateRange: true } : null;
}

@Component({
  selector: 'app-academic-years',
  standalone: true,
  imports: [
    CommonModule, DatePipe, ReactiveFormsModule,
    TableModule, ButtonModule, DialogModule, InputTextModule,
    DatePickerModule, ToggleSwitchModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './academic-years.html'
})
export class AcademicYears implements OnInit {
  private svc = inject(AcademicYearService);
  private fb = inject(FormBuilder);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<AcademicYear[]>([]);
  loading = signal(false);
  dialogOpen = signal(false);
  saving = signal(false);
  editingId = signal<number | null>(null);

  form = this.fb.nonNullable.group(
    {
      code: ['', [Validators.required, Validators.maxLength(20)]],
      displayName: ['', [Validators.required, Validators.maxLength(100)]],
      startDate: [null as Date | null, [Validators.required]],
      endDate: [null as Date | null, [Validators.required]],
      isOpen: [false]
    },
    { validators: endAfterStart }
  );

  get isEditing(): boolean {
    return this.editingId() !== null;
  }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: res => {
        this.rows.set(res.data ?? []);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openCreate(): void {
    this.editingId.set(null);
    this.form.reset({ code: '', displayName: '', startDate: null, endDate: null, isOpen: false });
    this.form.controls.code.enable();
    this.dialogOpen.set(true);
  }

  openEdit(row: AcademicYear): void {
    this.editingId.set(row.id);
    this.form.reset({
      code: row.code,
      displayName: row.displayName,
      startDate: new Date(row.startDate),
      endDate: new Date(row.endDate),
      isOpen: row.isOpen
    });
    // Code is immutable on update (backend update command omits it).
    this.form.controls.code.disable();
    this.dialogOpen.set(true);
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.saving.set(true);
    const v = this.form.getRawValue();
    const startDate = (v.startDate as Date).toISOString();
    const endDate = (v.endDate as Date).toISOString();

    const done = (msg: string) => {
      this.toast.success(msg);
      this.saving.set(false);
      this.dialogOpen.set(false);
      this.load();
    };
    const fail = () => this.saving.set(false);

    if (this.isEditing) {
      this.svc.update({ id: this.editingId()!, displayName: v.displayName, startDate, endDate })
        .subscribe({ next: () => done('Academic year updated'), error: fail });
    } else {
      this.svc.create({ code: v.code, displayName: v.displayName, startDate, endDate, isOpen: v.isOpen })
        .subscribe({ next: () => done('Academic year created'), error: fail });
    }
  }

  setCurrent(row: AcademicYear): void {
    if (row.isOpen) return;
    this.confirm.confirm({
      message: `Make "${row.displayName}" the current open year? This closes any other open year.`,
      header: 'Set current year',
      icon: 'pi pi-calendar',
      accept: () => {
        this.svc.setCurrent(row.id).subscribe({
          next: () => { this.toast.success('Current academic year updated'); this.load(); }
        });
      }
    });
  }

  archive(row: AcademicYear): void {
    this.confirm.confirm({
      message: `Archive "${row.displayName}"? It will be hidden but historical records are preserved.`,
      header: 'Archive academic year',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.svc.archive(row.id).subscribe({
          next: () => { this.toast.success('Academic year archived'); this.load(); }
        });
      }
    });
  }
}
