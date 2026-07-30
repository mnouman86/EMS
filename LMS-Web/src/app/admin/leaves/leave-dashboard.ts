import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { LeaveService, LeaveDashboardBundle } from './leave.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, TableModule, ButtonModule, TagModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Leaves Dashboard</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">Approvals, activity and upcoming leaves at a glance.</p>
      </div>
      <div style="display:flex;gap:8px;">
        <a routerLink="/admin/leaves/approvals" class="p-button p-component p-button-outlined" style="text-decoration:none;">
          <i class="pi pi-check-square" style="margin-right:6px;"></i> Approvals
        </a>
        <a routerLink="/admin/leaves/routing" class="p-button p-component p-button-outlined" style="text-decoration:none;">
          <i class="pi pi-sitemap" style="margin-right:6px;"></i> Routing
        </a>
        <a routerLink="/admin/leaves/types" class="p-button p-component p-button-outlined" style="text-decoration:none;">
          <i class="pi pi-tags" style="margin-right:6px;"></i> Types
        </a>
      </div>
    </div>

    @if (bundle(); as b) {
      <div style="display:grid;grid-template-columns:repeat(4, 1fr);gap:12px;margin-bottom:16px;">
        <div class="kpi warn"><div class="lbl">Pending</div><div class="val">{{ b.totals.pending }}</div></div>
        <div class="kpi ok"><div class="lbl">Approved (this month)</div><div class="val">{{ b.totals.approvedThisMonth }}</div></div>
        <div class="kpi danger"><div class="lbl">Rejected (this month)</div><div class="val">{{ b.totals.rejectedThisMonth }}</div></div>
        <div class="kpi info"><div class="lbl">Upcoming (30 days)</div><div class="val">{{ b.totals.upcomingIn30Days }}</div></div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 1fr;gap:16px;">
        <div style="background:#fff;border:1px solid #e2e8f0;border-radius:10px;padding:14px;">
          <h3 style="margin:0 0 10px;font-size:1rem;">Pending by leave type</h3>
          <p-table [value]="b.pendingByType" styleClass="p-datatable-sm p-datatable-gridlines">
            <ng-template pTemplate="header">
              <tr><th>Type</th><th style="width:120px;text-align:right;">Applications</th><th style="width:120px;text-align:right;">Days</th></tr>
            </ng-template>
            <ng-template pTemplate="body" let-row>
              <tr>
                <td>{{ row.leaveTypeName }}</td>
                <td style="text-align:right;">{{ row.pendingCount }}</td>
                <td style="text-align:right;">{{ row.pendingDays | number:'1.0-1' }}</td>
              </tr>
            </ng-template>
          </p-table>
        </div>

        <div style="background:#fff;border:1px solid #e2e8f0;border-radius:10px;padding:14px;">
          <h3 style="margin:0 0 10px;font-size:1rem;">Recent activity</h3>
          <p-table [value]="b.recent" styleClass="p-datatable-sm p-datatable-gridlines">
            <ng-template pTemplate="header">
              <tr><th>Applicant</th><th>Type</th><th style="width:110px;">From</th><th style="width:110px;">To</th><th style="width:100px;">Status</th></tr>
            </ng-template>
            <ng-template pTemplate="body" let-row>
              <tr>
                <td>{{ row.applicantName }}</td>
                <td>{{ row.leaveTypeName }}</td>
                <td>{{ row.startDate | date:'dd MMM' }}</td>
                <td>{{ row.endDate | date:'dd MMM' }}</td>
                <td><p-tag [value]="row.status" [severity]="sev(row.status)"></p-tag></td>
              </tr>
            </ng-template>
            <ng-template pTemplate="emptymessage">
              <tr><td colspan="5" style="text-align:center;padding:12px;color:#94a3b8;">No recent activity.</td></tr>
            </ng-template>
          </p-table>
        </div>
      </div>
    }

    <style>
      .kpi { border:1px solid #e2e8f0; border-radius:10px; padding:14px; background:#fff; }
      .kpi .lbl { color:#64748b; font-size:0.78rem; text-transform:uppercase; letter-spacing:0.4px; }
      .kpi .val { font-size:1.7rem; font-weight:600; color:#0f172a; margin-top:4px; }
      .kpi.warn   { border-left:4px solid #f59e0b; }
      .kpi.ok     { border-left:4px solid #16a34a; }
      .kpi.danger { border-left:4px solid #dc2626; }
      .kpi.info   { border-left:4px solid #0ea5e9; }
    </style>
  `
})
export class LeaveDashboard implements OnInit {
  private svc = inject(LeaveService);
  private toast = inject(ToastService);

  bundle = signal<LeaveDashboardBundle | null>(null);
  ngOnInit(): void {
    this.svc.getDashboard().subscribe({
      next: r => this.bundle.set(r.data ?? null),
      error: () => this.toast.error('Failed to load dashboard.')
    });
  }
  sev(s: string): 'success' | 'warn' | 'danger' | 'secondary' {
    switch (s) { case 'Approved': return 'success'; case 'Pending': return 'warn'; case 'Rejected': return 'danger'; default: return 'secondary'; }
  }
}
