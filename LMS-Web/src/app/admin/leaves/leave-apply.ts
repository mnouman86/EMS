import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { TextareaModule } from 'primeng/textarea';
import { FileUploadModule } from 'primeng/fileupload';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { CheckboxModule } from 'primeng/checkbox';
import { LeaveService, LeaveType, LeaveBalanceRow } from './leave.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-apply',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, ButtonModule, SelectModule, DatePickerModule,
            TextareaModule, FileUploadModule, MessageModule, TagModule, CheckboxModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Apply for Leave</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">Submit a leave request. Your balance updates as approvals go through.</p>
      </div>
    </div>

    <div style="display:grid;grid-template-columns:1fr 380px;gap:16px;">
      <div style="background:#fff;border:1px solid #e2e8f0;border-radius:10px;padding:20px;">
        <div style="display:grid;grid-template-columns:1fr 1fr;gap:14px 18px;">
          <div style="grid-column:1 / -1;">
            <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Leave type *</label>
            <p-select [options]="typeOptions()" optionLabel="label" optionValue="value"
                      [(ngModel)]="model.leaveTypeId" (ngModelChange)="onTypePicked()"
                      [style]="{ width: '100%' }" appendTo="body" placeholder="Pick a leave type"></p-select>
            @if (selectedType(); as t) {
              <div style="margin-top:6px;font-size:0.8rem;color:#64748b;">
                @if (t.isPaid) { Paid · } @else { Unpaid · }
                Default quota {{ t.defaultAnnualQuota }} days
                @if (t.requiresAttachment) { · <span style="color:#b45309;">Attachment required</span> }
              </div>
            }
          </div>
          <div>
            <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">From date *</label>
            <p-datepicker [(ngModel)]="model.startDate" dateFormat="dd M yy" [showIcon]="true" [fluid]="true" appendTo="body"
                          (ngModelChange)="recalcDays()"></p-datepicker>
            <div style="display:flex;align-items:center;gap:6px;margin-top:6px;">
              <p-checkbox [(ngModel)]="model.halfDayFrom" [binary]="true" (onChange)="recalcDays()"></p-checkbox>
              <span style="font-size:0.82rem;">Half day (from)</span>
            </div>
          </div>
          <div>
            <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">To date *</label>
            <p-datepicker [(ngModel)]="model.endDate" dateFormat="dd M yy" [showIcon]="true" [fluid]="true" appendTo="body"
                          (ngModelChange)="recalcDays()"></p-datepicker>
            <div style="display:flex;align-items:center;gap:6px;margin-top:6px;">
              <p-checkbox [(ngModel)]="model.halfDayTo" [binary]="true" (onChange)="recalcDays()"></p-checkbox>
              <span style="font-size:0.82rem;">Half day (to)</span>
            </div>
          </div>
          <div style="grid-column:1 / -1;">
            <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Reason</label>
            <textarea pTextarea [(ngModel)]="model.reason" rows="3" style="width:100%;" maxlength="1000"
                      placeholder="Brief context for the approver"></textarea>
          </div>
          <div style="grid-column:1 / -1;">
            <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">
              Attachment
              @if (selectedType()?.requiresAttachment) { <span style="color:#dc2626;">(required)</span> }
              @else { <span style="color:#64748b;font-weight:400;">(optional)</span> }
            </label>
            <p-fileUpload mode="basic" chooseLabel="Choose file" [auto]="true" [maxFileSize]="10485760"
                          accept=".pdf,.png,.jpg,.jpeg,.doc,.docx,.xls,.xlsx,.txt"
                          (onSelect)="onFile($event)"></p-fileUpload>
            @if (selectedFile) {
              <div style="margin-top:6px;font-size:0.84rem;color:#0f172a;">
                <i class="pi pi-check" style="color:#16a34a;"></i> {{ selectedFile.name }}
              </div>
            }
          </div>
        </div>

        <div style="margin-top:14px;padding:10px 12px;background:#f0fdf4;border:1px solid #86efac;border-radius:6px;font-size:0.9rem;">
          Duration: <strong>{{ totalDays() }}</strong> working day(s)
          <span style="color:#64748b;">(weekends &amp; holidays excluded)</span>
        </div>

        <div style="margin-top:16px;display:flex;justify-content:flex-end;gap:8px;">
          <p-button label="Cancel" severity="secondary" [outlined]="true" (onClick)="cancel()"></p-button>
          <p-button label="Submit" icon="pi pi-check" [loading]="saving()" (onClick)="save()"></p-button>
        </div>
      </div>

      <div style="background:#fff;border:1px solid #e2e8f0;border-radius:10px;padding:14px;">
        <h3 style="margin:0 0 10px;font-size:1rem;">My leave balance</h3>
        @if (balance().length === 0) {
          <p style="color:#94a3b8;font-size:0.85rem;">No balance loaded.</p>
        } @else {
          <table style="width:100%;font-size:0.85rem;border-collapse:collapse;">
            <thead>
              <tr style="background:#f1f5f9;text-align:left;">
                <th style="padding:6px;">Type</th>
                <th style="padding:6px;text-align:right;">Allocated</th>
                <th style="padding:6px;text-align:right;">Used</th>
                <th style="padding:6px;text-align:right;">Pending</th>
                <th style="padding:6px;text-align:right;color:#0f766e;">Available</th>
              </tr>
            </thead>
            <tbody>
              @for (b of balance(); track b.leaveTypeId) {
                <tr style="border-top:1px solid #e2e8f0;">
                  <td style="padding:6px;">{{ b.name }}</td>
                  <td style="padding:6px;text-align:right;">{{ b.allocated | number:'1.0-1' }}</td>
                  <td style="padding:6px;text-align:right;">{{ b.used | number:'1.0-1' }}</td>
                  <td style="padding:6px;text-align:right;color:#b45309;">{{ b.pending | number:'1.0-1' }}</td>
                  <td style="padding:6px;text-align:right;color:#0f766e;font-weight:600;">{{ b.available | number:'1.0-1' }}</td>
                </tr>
              }
            </tbody>
          </table>
        }
      </div>
    </div>
  `
})
export class LeaveApply implements OnInit {
  private svc = inject(LeaveService);
  private toast = inject(ToastService);
  private router = inject(Router);

  types = signal<LeaveType[]>([]);
  typeOptions = computed(() => this.types().filter(t => t.isActive).map(t => ({ label: t.name, value: t.leaveTypeId })));
  balance = signal<LeaveBalanceRow[]>([]);
  saving = signal(false);

  model = {
    leaveTypeId: null as number | null,
    startDate: null as Date | null,
    endDate: null as Date | null,
    halfDayFrom: false,
    halfDayTo: false,
    reason: ''
  };
  selectedFile: File | null = null;

  selectedType = computed(() => this.types().find(t => t.leaveTypeId === this.model.leaveTypeId));
  totalDays = signal(0);
  private previewDebounce: any = null;

  ngOnInit(): void {
    forkJoin({ types: this.svc.getTypes(true), bal: this.svc.getMyBalance(null) }).subscribe(({ types, bal }) => {
      this.types.set(types.data ?? []);
      this.balance.set(bal.data ?? []);
    });
  }
  onTypePicked(): void {}
  onFile(ev: any): void {
    const f: File | undefined = ev?.files?.[0];
    if (!f) return;
    if (f.size > 10 * 1024 * 1024) { this.toast.error('Attachment exceeds 10 MB.'); return; }
    this.selectedFile = f;
  }
  /** Debounced call to the backend preview so the shown Duration matches what
   *  the server will save — weekends + holidays already netted out. */
  recalcDays(): void {
    if (!this.model.startDate || !this.model.endDate || this.model.endDate < this.model.startDate) {
      this.totalDays.set(0); return;
    }
    if (this.previewDebounce) clearTimeout(this.previewDebounce);
    this.previewDebounce = setTimeout(() => {
      this.svc.previewWorkingDays(
        this.toIsoDate(this.model.startDate!),
        this.toIsoDate(this.model.endDate!),
        this.model.halfDayFrom, this.model.halfDayTo
      ).subscribe({
        next: r => this.totalDays.set(Number(r.data ?? 0)),
        error: () => this.totalDays.set(0)
      });
    }, 250);
  }
  save(): void {
    if (!this.model.leaveTypeId) { this.toast.error('Pick a leave type.'); return; }
    if (!this.model.startDate || !this.model.endDate) { this.toast.error('Pick start and end dates.'); return; }
    if (this.model.endDate < this.model.startDate) { this.toast.error('End date must be on or after start date.'); return; }
    if (this.totalDays() <= 0) { this.toast.error('Selected range has no working days — pick different dates.'); return; }
    const t = this.selectedType();
    if (t?.requiresAttachment && !this.selectedFile) { this.toast.error(`${t.name} requires an attachment.`); return; }

    this.saving.set(true);
    this.svc.submit({
      leaveTypeId: this.model.leaveTypeId,
      startDate: this.toIsoDate(this.model.startDate),
      endDate: this.toIsoDate(this.model.endDate),
      halfDayFrom: this.model.halfDayFrom,
      halfDayTo: this.model.halfDayTo,
      reason: this.model.reason || undefined,
      attachment: this.selectedFile
    }).subscribe({
      next: r => {
        this.saving.set(false);
        if (r.isSuccess) { this.toast.success(r.message || 'Submitted.'); this.router.navigate(['/admin/leaves/mine']); }
        else this.toast.error(r.message || 'Submit failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Submit failed.'); }
    });
  }
  cancel(): void { this.router.navigate(['/admin/leaves/mine']); }

  private toIsoDate(d: Date): string {
    const y = d.getFullYear(), m = String(d.getMonth() + 1).padStart(2, '0'), day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
