import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { TextareaModule } from 'primeng/textarea';
import { LeaveService, LeaveApplicationRow } from './leave.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-approvals',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, ButtonModule, TagModule, TooltipModule, DialogModule, TextareaModule],
  template: `
    <div style="margin-bottom:16px;">
      <h2 style="margin:0;font-size:1.3rem;">Leave Approvals</h2>
      <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">Pending applications routed to you as approver.</p>
    </div>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines"
             [paginator]="rows().length > 20" [rows]="20">
      <ng-template pTemplate="header">
        <tr>
          <th style="width:130px;">Code</th>
          <th>Applicant</th>
          <th>Type</th>
          <th style="width:120px;">From</th>
          <th style="width:120px;">To</th>
          <th style="width:70px;text-align:right;">Days</th>
          <th>Reason</th>
          <th style="width:70px;text-align:center;">Attach.</th>
          <th style="width:200px;text-align:center;">Actions</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td><strong>{{ r.leaveCode }}</strong></td>
          <td>{{ r.applicantName }}<div style="font-size:0.78rem;color:#94a3b8;">{{ r.applicantUserName }}</div></td>
          <td>{{ r.leaveTypeName }}</td>
          <td>
            {{ r.startDate | date:'dd MMM yyyy' }}
            @if (r.halfDayFrom) { <span style="color:#b45309;font-size:0.7rem;margin-left:4px;">½</span> }
          </td>
          <td>
            {{ r.endDate | date:'dd MMM yyyy' }}
            @if (r.halfDayTo) { <span style="color:#b45309;font-size:0.7rem;margin-left:4px;">½</span> }
          </td>
          <td style="text-align:right;">{{ r.totalDays | number:'1.0-1' }}</td>
          <td>{{ r.reason || '—' }}</td>
          <td style="text-align:center;">
            @if (r.attachmentPath) {
              <a [href]="attUrl(r)" target="_blank" rel="noopener" pTooltip="Download attachment">
                <i class="pi pi-paperclip" style="color:#0f766e;"></i>
              </a>
            } @else { <span style="color:#cbd5e1;">—</span> }
          </td>
          <td style="text-align:center;">
            <p-button label="Approve" icon="pi pi-check" size="small" severity="success"
                      (onClick)="approve(r)"></p-button>
            <p-button label="Reject" icon="pi pi-times" size="small" severity="danger" [outlined]="true"
                      (onClick)="openReject(r)" styleClass="ml-1"></p-button>
          </td>
        </tr>
      </ng-template>
      <ng-template pTemplate="emptymessage">
        <tr><td colspan="9" style="text-align:center;padding:18px;color:#94a3b8;">No pending approvals for you.</td></tr>
      </ng-template>
    </p-table>

    <p-dialog [(visible)]="rejectOpen" [modal]="true" [style]="{ width: '480px' }" [draggable]="false" [resizable]="false"
              header="Reject leave — reason required">
      <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Reason *</label>
      <textarea pTextarea [(ngModel)]="rejectReason" rows="4" style="width:100%;"
                placeholder="Explain the reason (visible to the applicant)"></textarea>
      <ng-template pTemplate="footer">
        <p-button label="Cancel" severity="secondary" [outlined]="true" (onClick)="rejectOpen.set(false)"></p-button>
        <p-button label="Reject" icon="pi pi-times" severity="danger" [loading]="saving()" (onClick)="submitReject()"></p-button>
      </ng-template>
    </p-dialog>
  `
})
export class LeaveApprovals implements OnInit {
  private svc = inject(LeaveService);
  private toast = inject(ToastService);

  rows = signal<LeaveApplicationRow[]>([]);
  loading = signal(false);
  saving = signal(false);
  rejectOpen = signal(false);
  rejectReason = '';
  rejectTarget: LeaveApplicationRow | null = null;

  ngOnInit(): void { this.load(); }
  load(): void {
    this.loading.set(true);
    this.svc.getApplications({ scope: 'Pending' }).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load.'); this.loading.set(false); }
    });
  }
  approve(r: LeaveApplicationRow): void {
    this.svc.decide(r.leaveApplicationId, 'Approved').subscribe({
      next: (res: any) => {
        if (res.isSuccess) { this.toast.success(res.message || 'Approved.'); this.load(); }
        else this.toast.error(res.message || 'Approve failed.');
      },
      error: () => this.toast.error('Approve failed.')
    });
  }
  openReject(r: LeaveApplicationRow): void { this.rejectTarget = r; this.rejectReason = ''; this.rejectOpen.set(true); }
  submitReject(): void {
    if (!this.rejectTarget) return;
    if (this.rejectReason.trim().length < 3) { this.toast.error('Provide a reason (≥ 3 chars).'); return; }
    this.saving.set(true);
    this.svc.decide(this.rejectTarget.leaveApplicationId, 'Rejected', this.rejectReason).subscribe({
      next: (res: any) => {
        this.saving.set(false);
        if (res.isSuccess) { this.toast.success(res.message || 'Rejected.'); this.rejectOpen.set(false); this.load(); }
        else this.toast.error(res.message || 'Reject failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Reject failed.'); }
    });
  }
  attUrl(r: LeaveApplicationRow): string { return this.svc.attachmentUrl(r.attachmentPath!, r.attachmentOriginalName); }
}
