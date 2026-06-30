import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ComplaintService, ComplaintNature } from '../../core/services/complaint.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-complaint-nature',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, InputTextModule, InputNumberModule,
    SelectModule, ToggleSwitchModule, TagModule, DialogModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './complaint-nature.html'
})
export class ComplaintNatureAdmin implements OnInit {
  private svc = inject(ComplaintService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<ComplaintNature[]>([]);
  loading = signal(false);

  typeOptions = [
    { label: 'Complaint', value: 'Complaint' },
    { label: 'Suggestion', value: 'Suggestion' },
    { label: 'Either', value: 'Either' }
  ];

  dialogOpen = signal(false);
  saving = signal(false);
  editing: Partial<ComplaintNature> = {};

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getNatures(false).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load natures.'); this.loading.set(false); }
    });
  }

  add(): void {
    this.editing = { name: '', complaintType: 'Complaint', displayOrder: 99, isActive: true };
    this.dialogOpen.set(true);
  }

  edit(r: ComplaintNature): void {
    this.editing = { ...r };
    this.dialogOpen.set(true);
  }

  save(): void {
    if (!this.editing.name?.trim()) { this.toast.error('Name is required.'); return; }
    this.saving.set(true);
    this.svc.upsertNature(this.editing).subscribe({
      next: r => {
        this.saving.set(false);
        if (r.isSuccess) { this.toast.success(r.message || 'Saved.'); this.dialogOpen.set(false); this.load(); }
        else this.toast.error(r.message || 'Save failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Save failed.'); }
    });
  }

  deactivate(r: ComplaintNature): void {
    this.confirm.confirm({
      message: `Deactivate "${r.name}"? Existing complaints stay; new ones won't be able to pick this nature.`,
      header: 'Deactivate nature',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.svc.deactivateNature(r.complaintNatureId).subscribe({
          next: res => { res.isSuccess ? this.toast.success(res.message || 'Deactivated.') : this.toast.error(res.message || 'Failed.'); this.load(); },
          error: () => this.toast.error('Failed.')
        });
      }
    });
  }
}
