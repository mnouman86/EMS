import { Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ParentService } from '../parent.service';
import { AttendanceService } from '../../attendance/attendance.service';
import { AttendancePeriod, AttendanceSummary } from '../../attendance/attendance.models';

@Component({
  selector: 'app-parent-attendance',
  standalone: true,
  imports: [CommonModule, TableModule, TagModule],
  templateUrl: './parent-attendance.html'
})
export class ParentAttendance {
  parent = inject(ParentService);
  private svc = inject(AttendanceService);

  loading = signal(false);
  history = signal<AttendancePeriod[]>([]);
  summary = signal<AttendanceSummary | null>(null);
  private lastLoadedId: number | null = null;

  private readonly months = [
    '', 'January','February','March','April','May','June',
    'July','August','September','October','November','December'
  ];
  monthName(m: number): string { return this.months[m] || String(m); }

  constructor() {
    this.parent.loadChildren().subscribe();
    effect(() => {
      const id = this.parent.selectedChildId();
      if (id && id !== this.lastLoadedId) this.load(id);
    });
  }

  private load(studentId: number): void {
    this.lastLoadedId = studentId;
    this.loading.set(true);
    this.history.set([]);
    this.summary.set(null);
    this.svc.getChildHistory(studentId, null).subscribe({
      next: res => { this.history.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
    this.svc.getChildSummary(studentId, null).subscribe({
      next: res => this.summary.set(res.data ?? null)
    });
  }

  pctSeverity(p?: number | null): 'success' | 'info' | 'warn' | 'danger' | 'secondary' {
    if (p == null) return 'secondary';
    if (p >= 90) return 'success';
    if (p >= 75) return 'info';
    if (p >= 50) return 'warn';
    return 'danger';
  }
}
