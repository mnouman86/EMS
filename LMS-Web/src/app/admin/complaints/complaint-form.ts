import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { FileUploadModule } from 'primeng/fileupload';
import { TagModule } from 'primeng/tag';
import { ComplaintService, ComplaintNature } from '../../core/services/complaint.service';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-complaint-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonModule, InputTextModule, TextareaModule, SelectModule, FileUploadModule, TagModule],
  templateUrl: './complaint-form.html'
})
export class ComplaintForm implements OnInit {
  private svc = inject(ComplaintService);
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  natures = signal<ComplaintNature[]>([]);
  natureOptions = computed(() => this.natures()
    .filter(n => n.isActive)
    .map(n => ({ label: `${n.name} · ${n.complaintType}`, value: n.complaintNatureId })));

  // Form model
  model = {
    complaintId: 0 as number,
    complaintNatureId: null as number | null,
    complainantName: '',
    contactNumber: '',
    complaintAgainst: '',
    description: '',
    clearAttachment: false
  };
  selectedFile: File | null = null;
  existingAttachment: string | null = null;
  existingAttachmentName: string | null = null;

  saving = signal(false);
  isEdit = computed(() => this.model.complaintId > 0);

  ngOnInit(): void {
    const u = this.auth.currentUser();
    // Auto-populate from currently logged-in user (editable per requirement)
    this.model.complainantName = u?.userName || u?.email || '';
    this.model.contactNumber = ''; // we don't carry phone in the JWT today; user fills in or edits

    this.svc.getNatures(true).subscribe({
      next: r => this.natures.set(r.data ?? []),
      error: () => this.toast.error('Failed to load complaint natures.')
    });

    // Edit mode: load existing complaint
    const id = Number(this.route.snapshot.paramMap.get('id') || 0);
    if (id > 0) this.loadForEdit(id);
  }

  private loadForEdit(id: number): void {
    this.svc.getDetail(id).subscribe({
      next: r => {
        const h = r.data?.header;
        if (!h) { this.toast.error('Complaint not found.'); return; }
        if (h.status !== 'New') { this.toast.error('This complaint is past the New stage and cannot be edited.'); this.router.navigate(['/admin/complaints/mine']); return; }
        this.model = {
          complaintId: h.complaintId,
          complaintNatureId: h.complaintNatureId,
          complainantName: h.complainantName,
          contactNumber: h.contactNumber,
          complaintAgainst: h.complaintAgainst ?? '',
          description: h.description,
          clearAttachment: false
        };
        this.existingAttachment = h.attachmentPath ?? null;
        this.existingAttachmentName = h.attachmentOriginalName ?? null;
      },
      error: () => this.toast.error('Failed to load complaint.')
    });
  }

  onFileSelect(ev: any): void {
    const f: File | undefined = ev?.files?.[0];
    if (!f) return;
    if (f.size > 10 * 1024 * 1024) { this.toast.error('Attachment exceeds 10 MB.'); return; }
    this.selectedFile = f;
  }

  clearFile(): void {
    this.selectedFile = null;
    this.model.clearAttachment = true;
  }

  attachmentUrl(): string | null {
    return this.existingAttachment ? this.svc.attachmentUrl(this.existingAttachment, this.existingAttachmentName ?? undefined) : null;
  }

  save(): void {
    if (!this.model.complaintNatureId) { this.toast.error('Pick a complaint / suggestion nature.'); return; }
    if (!this.model.complainantName?.trim()) { this.toast.error('Complainant name is required.'); return; }
    if (!this.model.contactNumber?.trim()) { this.toast.error('Contact number is required.'); return; }
    if (!this.model.description || this.model.description.trim().length < 10) {
      this.toast.error('Description must be at least 10 characters.'); return;
    }
    this.saving.set(true);

    const ok = (msg: string) => {
      this.toast.success(msg);
      this.saving.set(false);
      this.router.navigate(['/admin/complaints/mine']);
    };
    const err = (msg: string) => { this.toast.error(msg); this.saving.set(false); };

    if (this.isEdit()) {
      this.svc.updateComplaint({
        complaintId: this.model.complaintId,
        complaintNatureId: this.model.complaintNatureId!,
        complainantName: this.model.complainantName,
        contactNumber: this.model.contactNumber,
        complaintAgainst: this.model.complaintAgainst,
        description: this.model.description,
        clearAttachment: this.model.clearAttachment,
        attachment: this.selectedFile
      }).subscribe({
        next: r => r.isSuccess ? ok(r.message || 'Updated.') : err(r.message || 'Save failed.'),
        error: () => err('Save failed.')
      });
    } else {
      this.svc.createComplaint({
        complaintNatureId: this.model.complaintNatureId!,
        complainantName: this.model.complainantName,
        contactNumber: this.model.contactNumber,
        complaintAgainst: this.model.complaintAgainst,
        description: this.model.description,
        attachment: this.selectedFile
      }).subscribe({
        next: r => r.isSuccess ? ok(r.message || 'Submitted.') : err(r.message || 'Submit failed.'),
        error: () => err('Submit failed.')
      });
    }
  }

  cancel(): void { this.router.navigate(['/admin/complaints/mine']); }
}
