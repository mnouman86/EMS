import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { StaffAttendanceService } from './staff-attendance.service';
import { StaffAttendanceOverviewRow } from './staff-attendance.models';

@Component({
  selector: 'app-staff-attendance-overview',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, DatePickerModule, ButtonModule, TagModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:14px;gap:12px;flex-wrap:wrap;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Staff Attendance — Overview</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:0.85rem;">All staff attendance records in the selected range.</p>
      </div>
      <a routerLink="/admin/dashboard" class="p-button p-component p-button-outlined" style="text-decoration:none;">
        <i class="pi pi-arrow-left" style="margin-right:6px;"></i> Back
      </a>
    </div>

    <div style="display:flex;gap:12px;align-items:flex-end;margin-bottom:12px;">
      <div>
        <label style="display:block;font-size:0.78rem;color:#64748b;margin-bottom:4px;">Date range</label>
        <p-datepicker [(ngModel)]="range" selectionMode="range" dateFormat="dd M yy" [showIcon]="true" appendTo="body" [readonlyInput]="true"></p-datepicker>
      </div>
      <p-button label="Run" icon="pi pi-search" [loading]="loading()" (onClick)="load()"></p-button>
    </div>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines"
             [tableStyle]="{ 'min-width': '56rem' }" [paginator]="rows().length > 30" [rows]="30"
             [globalFilterFields]="['staffName','userEmail','roleName']" #dt>
      <ng-template pTemplate="caption">
        <div style="display:flex;justify-content:flex-end;">
          <input pInputText type="text" (input)="dt.filterGlobal($any($event.target).value, 'contains')" placeholder="Filter…"/>
        </div>
      </ng-template>
      <ng-template pTemplate="header">
        <tr>
          <th style="width:150px;">Date</th>
          <th>Staff</th>
          <th style="width:120px;">Role</th>
          <th style="width:120px;">Check-in</th>
          <th style="width:120px;">Check-out</th>
          <th>Remarks</th>
          <th style="width:100px;text-align:center;">Backdated?</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td>{{ r.attendanceDate | date:'dd MMM yyyy' }}</td>
          <td>
            <div style="font-weight:500;">{{ r.staffName || r.userEmail }}</div>
            <div style="color:#94a3b8;font-size:0.78rem;">{{ r.userEmail }}</div>
          </td>
          <td>{{ r.roleName || '—' }}</td>
          <td>{{ r.checkInTime | date:'shortTime' }}</td>
          <td>{{ r.checkOutTime ? (r.checkOutTime | date:'shortTime') : '—' }}</td>
          <td>{{ r.remarks || '—' }}</td>
          <td style="text-align:center;">
            @if (r.isBackdated) { <p-tag value="Yes" severity="warn"></p-tag> } @else { — }
          </td>
        </tr>
      </ng-template>
      <ng-template pTemplate="emptymessage">
        <tr><td colspan="7" style="text-align:center;padding:20px;color:#64748b;">No records in this range.</td></tr>
      </ng-template>
    </p-table>
  `
})
export class StaffAttendanceOverview implements OnInit {
  private svc = inject(StaffAttendanceService);
  range: Date[] = [];
  rows = signal<StaffAttendanceOverviewRow[]>([]);
  loading = signal(false);

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), now.getMonth(), 1), now];
    this.load();
  }

  load(): void {
    const from = this.range?.[0]?.toISOString() ?? new Date().toISOString();
    const to = this.range?.[1]?.toISOString() ?? new Date().toISOString();
    this.loading.set(true);
    this.svc.overview(from, to, null).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
