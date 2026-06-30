import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { TextareaModule } from 'primeng/textarea';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ComplaintService, ComplaintRow, ComplaintNature, ComplaintDetail } from '../../core/services/complaint.service';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-complaints-report',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, InputTextModule, SelectModule, DatePickerModule,
    TagModule, TooltipModule, DialogModule, TextareaModule, CheckboxModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './complaints-report.html'
})
export class ComplaintsReport implements OnInit {
  private svc = inject(ComplaintService);
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<ComplaintRow[]>([]);
  loading = signal(false);
  natures = signal<ComplaintNature[]>([]);

  isAdmin = computed(() => (this.auth.currentUser()?.roles ?? []).some(r => r.toLowerCase() === 'admin'));

  // Filter model
  filter = {
    range: null as Date[] | null,
    complaintType: null as string | null,
    complaintNatureId: null as number | null,
    status: null as string | null,
    complainantLike: '',
    hasAttachment: null as boolean | null,
    includeDeleted: false
  };

  typeOptions = [
    { label: 'Any', value: null },
    { label: 'Complaint', value: 'Complaint' },
    { label: 'Suggestion', value: 'Suggestion' }
  ];
  statusOptions = [
    { label: 'Any', value: null },
    { label: 'New', value: 'New' },
    { label: 'In review', value: 'InReview' },
    { label: 'Resolved', value: 'Resolved' },
    { label: 'Closed', value: 'Closed' },
    { label: 'Rejected', value: 'Rejected' }
  ];
  attachmentOptions = [
    { label: 'Any', value: null },
    { label: 'With attachment', value: true },
    { label: 'No attachment', value: false }
  ];
  natureOptions = computed(() => [
    { label: 'Any', value: null },
    ...this.natures().map(n => ({ label: `${n.name} · ${n.complaintType}`, value: n.complaintNatureId }))
  ]);

  // Detail dialog
  detailOpen = signal(false);
  detail = signal<ComplaintDetail | null>(null);
  noteText = '';
  statusToChange: string | null = null;
  changeNote = '';
  deleteReason = '';
  deleteOpen = signal(false);

  ngOnInit(): void {
    this.svc.getNatures(false).subscribe({ next: r => this.natures.set(r.data ?? []) });
    this.run();
  }

  run(): void {
    this.loading.set(true);
    const f = this.filter;
    this.svc.getAll({
      fromDate: f.range?.[0]?.toISOString().slice(0, 10) ?? null,
      toDate: f.range?.[1]?.toISOString().slice(0, 10) ?? null,
      complaintType: f.complaintType,
      complaintNatureId: f.complaintNatureId,
      status: f.status,
      complainantLike: f.complainantLike?.trim() || null,
      hasAttachment: f.hasAttachment,
      includeDeleted: !!f.includeDeleted
    }).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load report.'); this.loading.set(false); }
    });
  }

  view(r: ComplaintRow): void {
    this.svc.getDetail(r.complaintId).subscribe({
      next: res => {
        this.detail.set(res.data ?? null);
        this.statusToChange = res.data?.header?.status ?? null;
        this.changeNote = '';
        this.noteText = '';
        this.detailOpen.set(true);
      },
      error: () => this.toast.error('Failed to load detail.')
    });
  }

  applyStatus(): void {
    const id = this.detail()?.header?.complaintId;
    if (!id || !this.statusToChange) return;
    if (this.changeNote.trim().length < 10) { this.toast.error('Provide a note (≥ 10 chars) for this status change.'); return; }
    this.svc.changeStatus(id, this.statusToChange, this.changeNote).subscribe({
      next: r => {
        if (r.isSuccess) { this.toast.success(r.message || 'Status updated.'); this.detailOpen.set(false); this.run(); }
        else this.toast.error(r.message || 'Status change failed.');
      },
      error: () => this.toast.error('Status change failed.')
    });
  }

  addNote(): void {
    const id = this.detail()?.header?.complaintId;
    if (!id) return;
    if (!this.noteText.trim()) { this.toast.error('Note is empty.'); return; }
    this.svc.addNote(id, this.noteText).subscribe({
      next: r => {
        if (r.isSuccess) { this.toast.success('Note added.'); this.view(this.detail()!.header); this.noteText = ''; }
        else this.toast.error(r.message || 'Note failed.');
      },
      error: () => this.toast.error('Note failed.')
    });
  }

  openDelete(): void { this.deleteReason = ''; this.deleteOpen.set(true); }

  submitDelete(): void {
    const id = this.detail()?.header?.complaintId;
    if (!id) return;
    if (this.deleteReason.trim().length < 10) { this.toast.error('Reason must be at least 10 characters.'); return; }
    this.svc.softDelete(id, this.deleteReason).subscribe({
      next: r => {
        if (r.isSuccess) {
          this.toast.success(r.message || 'Soft-deleted.');
          this.deleteOpen.set(false); this.detailOpen.set(false); this.run();
        } else this.toast.error(r.message || 'Delete failed.');
      },
      error: () => this.toast.error('Delete failed.')
    });
  }

  restore(r: ComplaintRow): void {
    this.confirm.confirm({
      message: `Restore ${r.complaintCode}?`,
      header: 'Restore complaint',
      accept: () => {
        this.svc.restore(r.complaintId).subscribe({
          next: res => {
            if (res.isSuccess) { this.toast.success(res.message || 'Restored.'); this.run(); }
            else this.toast.error(res.message || 'Restore failed.');
          },
          error: () => this.toast.error('Restore failed.')
        });
      }
    });
  }

  statusSeverity(s: string): 'info' | 'warn' | 'success' | 'danger' | 'secondary' {
    switch (s) {
      case 'New': return 'info';
      case 'InReview': return 'warn';
      case 'Resolved': return 'success';
      case 'Closed': return 'secondary';
      case 'Rejected': return 'danger';
      default: return 'secondary';
    }
  }

  attUrl(r: ComplaintRow): string | null {
    return r.attachmentPath ? this.svc.attachmentUrl(r.attachmentPath, r.attachmentOriginalName) : null;
  }
}
