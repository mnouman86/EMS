import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { LeaveService, LeaveApplicationRow } from './leave.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-mine',
  standalone: true,
  imports: [CommonModule, RouterLink, TableModule, ButtonModule, TagModule, TooltipModule, ConfirmDialogModule],
  providers: [ConfirmationService],
  template: `
    <p-confirmDialog></p-confirmDialog>
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">My Leave Applications</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">Everything you've submitted, with current status.</p>
      </div>
      <a routerLink="/admin/leaves/apply" class="p-button p-component" style="text-decoration:none;">
        <i class="pi pi-plus" style="margin-right:6px;"></i> Apply
      </a>
    </div>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines"
             [paginator]="rows().length > 20" [rows]="20">
      <ng-template pTemplate="header">
        <tr>
          <th style="width:130px;">Code</th>
          <th style="width:130px;">Submitted</th>
          <th>Type</th>
          <th style="width:120px;">From</th>
          <th style="width:120px;">To</th>
          <th style="width:70px;text-align:right;">Days</th>
          <th style="width:110px;">Status</th>
          <th>Approver / decision</th>
          <th style="width:80px;text-align:center;">Attach.</th>
          <th style="width:110px;text-align:center;">Actions</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td><strong>{{ r.leaveCode }}</strong></td>
          <td>{{ r.submittedAt | date:'dd MMM yyyy' }}</td>
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
          <td><p-tag [value]="r.status" [severity]="sev(r.status)"></p-tag></td>
          <td>
            {{ r.approverName || '—' }}
            @if (r.decidedAt) { <div style="color:#64748b;font-size:0.78rem;">{{ r.decisionReason }}</div> }
          </td>
          <td style="text-align:center;">
            @if (r.attachmentPath) {
              <a [href]="attUrl(r)" target="_blank" rel="noopener" pTooltip="Download attachment">
                <i class="pi pi-paperclip" style="color:#0f766e;"></i>
              </a>
            } @else { <span style="color:#cbd5e1;">—</span> }
          </td>
          <td style="text-align:center;">
            @if (r.status === 'Pending') {
              <p-button icon="pi pi-times" [text]="true" size="small" severity="danger"
                        (onClick)="cancel(r)" pTooltip="Cancel application"></p-button>
            }
          </td>
        </tr>
      </ng-template>
      <ng-template pTemplate="emptymessage">
        <tr><td colspan="10" style="text-align:center;padding:18px;color:#94a3b8;">No leave applications yet.</td></tr>
      </ng-template>
    </p-table>
  `
})
export class LeaveMine implements OnInit {
  private svc = inject(LeaveService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<LeaveApplicationRow[]>([]);
  loading = signal(false);
  ngOnInit(): void { this.load(); }
  load(): void {
    this.loading.set(true);
    this.svc.getApplications({ scope: 'Mine' }).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load.'); this.loading.set(false); }
    });
  }
  cancel(r: LeaveApplicationRow): void {
    this.confirm.confirm({
      message: `Cancel leave ${r.leaveCode}?`,
      header: 'Cancel leave', icon: 'pi pi-exclamation-triangle',
      accept: () => this.svc.cancel(r.leaveApplicationId).subscribe({
        next: (res: any) => { res.isSuccess ? this.toast.success(res.message) : this.toast.error(res.message); this.load(); },
        error: () => this.toast.error('Cancel failed.')
      })
    });
  }
  sev(s: string): 'success' | 'warn' | 'danger' | 'secondary' {
    switch (s) { case 'Approved': return 'success'; case 'Pending': return 'warn'; case 'Rejected': return 'danger'; default: return 'secondary'; }
  }
  attUrl(r: LeaveApplicationRow): string { return this.svc.attachmentUrl(r.attachmentPath!, r.attachmentOriginalName); }
}
