import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { SubjectService } from './subject.service';
import { Subject } from './subject.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-subjects',
  standalone: true,
  imports: [
    CommonModule, RouterLink, ReactiveFormsModule,
    TableModule, ButtonModule, DialogModule, InputTextModule, InputNumberModule,
    ToggleSwitchModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './subjects.html'
})
export class Subjects implements OnInit {
  private svc = inject(SubjectService);
  private fb = inject(FormBuilder);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<Subject[]>([]);
  loading = signal(false);
  dialogOpen = signal(false);
  saving = signal(false);
  editingId = signal<number | null>(null);

  form = this.fb.nonNullable.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    shortCode: ['' as string | null, [Validators.maxLength(10)]],
    displayOrder: [0, [Validators.required, Validators.min(0)]],
    isRTL: [false],
    isActive: [true]
  });

  get isEditing(): boolean {
    return this.editingId() !== null;
  }

  ngOnInit(): void {
    this.load();
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
    this.form.reset({ name: '', shortCode: '', displayOrder: 0, isRTL: false, isActive: true });
    this.dialogOpen.set(true);
  }

  openEdit(row: Subject): void {
    this.editingId.set(row.id);
    this.form.reset({
      name: row.name, shortCode: row.shortCode ?? '', displayOrder: row.displayOrder,
      isRTL: row.isRTL, isActive: row.isActive
    });
    this.dialogOpen.set(true);
  }

  save(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving.set(true);
    const v = this.form.getRawValue();
    const done = (msg: string) => { this.toast.success(msg); this.saving.set(false); this.dialogOpen.set(false); this.load(); };
    const fail = () => this.saving.set(false);

    if (this.isEditing) {
      this.svc.update({ id: this.editingId()!, ...v }).subscribe({ next: () => done('Subject updated'), error: fail });
    } else {
      this.svc.create({ ...v }).subscribe({ next: () => done('Subject created'), error: fail });
    }
  }

  deactivate(row: Subject): void {
    this.confirm.confirm({
      message: `Deactivate "${row.name}"? It is removed from class mappings and new result entry, but past results are kept.`,
      header: 'Deactivate subject',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.deactivate(row.id).subscribe({ next: () => { this.toast.success('Subject deactivated'); this.load(); } })
    });
  }
}
