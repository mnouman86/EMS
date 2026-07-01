import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { TagModule } from 'primeng/tag';
import { AuthService } from '../../core/services/auth.service';
import { Roles } from '../../core/models/roles';
import { StaffAttendanceService } from '../staff-attendance/staff-attendance.service';
import { StaffAttendanceRow, StaffAttendanceOverviewRow } from '../staff-attendance/staff-attendance.models';
import { AttendanceService } from '../../attendance/attendance.service';
import { SchoolClassService } from '../classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';

type Preset = 'today' | 'week' | 'month' | 'custom';

interface StudentBoardRow {
  id: number; classId: number; className: string;
  studentId: number; studentCode: string; studentName: string;
  periodYear: number; periodMonth: number;
  workingDays: number; presentDays: number; absentDays: number;
  attendancePercent: number | null;
  remarks?: string | null; updatedAt?: string | null;
}

@Component({
  selector: 'app-attendance-summary',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, SelectModule, DatePickerModule, TagModule
  ],
  templateUrl: './attendance-summary.html'
})
export class AttendanceSummary implements OnInit {
  private auth = inject(AuthService);
  private staffSvc = inject(StaffAttendanceService);
  private attSvc = inject(AttendanceService);
  private classSvc = inject(SchoolClassService);

  /* Role check — decides which staff endpoint to hit. Teachers see own only. */
  readonly isAdminOrPrincipal = computed(() =>
    this.auth.hasAnyRole([Roles.Admin, Roles.Principal]));

  /* Shared filter state */
  activeTab: 'staff' | 'student' = 'staff';
  preset: Preset = 'month';
  range: Date[] = [];
  classOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All classes', value: null }]);
  classId: number | null = null;

  /* Data signals */
  loading = signal(false);
  staffRows = signal<(StaffAttendanceRow | StaffAttendanceOverviewRow)[]>([]);
  studentRows = signal<StudentBoardRow[]>([]);
  readonly hasStaffData = computed(() => this.staffRows().length > 0);
  readonly hasStudentData = computed(() => this.studentRows().length > 0);

  ngOnInit(): void {
    this.applyPreset('month');
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      this.classOptions.set([
        { label: 'All classes', value: null },
        ...(res.data ?? []).map(c => ({ label: c.levelName, value: c.id as number | null }))
      ]);
    });
    this.load();
  }

  applyPreset(p: Preset): void {
    this.preset = p;
    if (p === 'custom') return;
    const now = new Date();
    let from: Date;
    switch (p) {
      case 'today': from = new Date(now.getFullYear(), now.getMonth(), now.getDate()); break;
      case 'week':  const dow = (now.getDay() + 6) % 7;
                    from = new Date(now.getFullYear(), now.getMonth(), now.getDate() - dow); break;
      case 'month': from = new Date(now.getFullYear(), now.getMonth(), 1); break;
    }
    this.range = [from, now];
  }

  load(): void {
    const from = this.range?.[0]?.toISOString() ?? new Date().toISOString();
    const to = this.range?.[1]?.toISOString() ?? new Date().toISOString();
    this.loading.set(true);
    if (this.activeTab === 'staff') {
      // Role-aware endpoint pick: admin/principal → overview, else → own history.
      const req = this.isAdminOrPrincipal()
        ? this.staffSvc.overview(from, to, null)
        : this.staffSvc.history(from, to);
      req.subscribe({
        next: res => { this.staffRows.set(res.data ?? []); this.loading.set(false); },
        error: () => this.loading.set(false)
      });
    } else {
      // Student board — teacher scoping happens server-side via TeacherScopeContext.
      this.attSvc.getStudentBoard(from, to, this.classId).subscribe({
        next: res => { this.studentRows.set(res.data ?? []); this.loading.set(false); },
        error: () => this.loading.set(false)
      });
    }
  }

  onTabChange(v: string | number | undefined): void {
    if (v === undefined) return;
    this.activeTab = (v as 'staff' | 'student');
    this.load();
  }

  duration(inTime: string, outTime?: string | null): string {
    if (!outTime) return '—';
    const ms = new Date(outTime).getTime() - new Date(inTime).getTime();
    const h = Math.floor(ms / 3.6e6);
    const m = Math.floor((ms % 3.6e6) / 60000);
    return `${h}h ${m.toString().padStart(2, '0')}m`;
  }

  monthName(m: number): string {
    return ['','Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'][m] || String(m);
  }

  percentSeverity(p: number | null): 'success' | 'info' | 'warn' | 'danger' | 'secondary' {
    if (p == null) return 'secondary';
    if (p >= 90) return 'success';
    if (p >= 75) return 'info';
    if (p >= 50) return 'warn';
    return 'danger';
  }

  /* ---------- Client-side CSV export ---------- */
  private csvEscape(v: unknown): string {
    if (v == null) return '';
    const s = String(v);
    return /[",\r\n]/.test(s) ? `"${s.replace(/"/g, '""')}"` : s;
  }
  private download(name: string, csv: string): void {
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url; a.download = name; a.click();
    URL.revokeObjectURL(url);
  }

  exportCsv(): void {
    const stamp = new Date().toISOString().slice(0, 10);
    if (this.activeTab === 'staff') {
      const rows = this.staffRows() as StaffAttendanceOverviewRow[];
      const headers = ['UserName', 'Email', 'Date', 'Check In', 'Check Out', 'Duration', 'Remarks'];
      const body = rows.map(r => [
        r.staffName ?? r.userEmail ?? String(r.userId),
        r.userEmail ?? '',
        new Date(r.attendanceDate).toLocaleDateString(),
        new Date(r.checkInTime).toLocaleTimeString(),
        r.checkOutTime ? new Date(r.checkOutTime).toLocaleTimeString() : '',
        this.duration(r.checkInTime, r.checkOutTime),
        r.remarks ?? ''
      ].map(v => this.csvEscape(v)).join(',')).join('\r\n');
      this.download(`staff-attendance-${stamp}.csv`, [headers.join(','), body].join('\r\n'));
    } else {
      const rows = this.studentRows();
      const headers = ['Class', 'Student Code', 'Student Name', 'Period', 'Working Days', 'Present Days', 'Absent Days', 'Attendance %', 'Remarks'];
      const body = rows.map(r => [
        r.className, r.studentCode, r.studentName,
        `${this.monthName(r.periodMonth)}/${r.periodYear}`,
        r.workingDays, r.presentDays, r.absentDays,
        r.attendancePercent ?? '',
        r.remarks ?? ''
      ].map(v => this.csvEscape(v)).join(',')).join('\r\n');
      this.download(`student-attendance-${stamp}.csv`, [headers.join(','), body].join('\r\n'));
    }
  }
}
