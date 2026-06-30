import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { SelectButtonModule } from 'primeng/selectbutton';
import { AttendanceService } from '../attendance.service';
import { DailyAttendanceGridRow, DailyStatus } from '../attendance.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { CalendarService } from '../../admin/calendar/calendar.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-attendance-daily',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, SelectModule, ButtonModule, DatePickerModule, InputTextModule, TagModule, SelectButtonModule
  ],
  templateUrl: './attendance-daily.html'
})
export class AttendanceDaily implements OnInit {
  private svc = inject(AttendanceService);
  private classSvc = inject(SchoolClassService);
  private yearSvc = inject(AcademicYearService);
  private calendarSvc = inject(CalendarService);
  private toast = inject(ToastService);

  yearOptions = signal<{ label: string; value: number }[]>([]);
  classOptions = signal<{ label: string; value: number }[]>([]);

  academicYearId: number | null = null;
  schoolClassId: number | null = null;
  /** Default to today. */
  attendanceDate: Date = this.todayLocal();

  /** Disabled dates passed to <p-datepicker>. */
  disabledDates = signal<Date[]>([]);
  /** Disabled weekday numbers (0=Sun..6=Sat). */
  disabledDays = signal<number[]>([]);

  loading = signal(false);
  saving = signal(false);
  rows = signal<DailyAttendanceGridRow[]>([]);

  statusOptions: { label: string; value: DailyStatus }[] = [
    { label: 'Present', value: 'Present' },
    { label: 'Absent',  value: 'Absent'  },
    { label: 'Late',    value: 'Late'    }
  ];

  totals = computed(() => {
    const r = this.rows();
    return {
      count   : r.length,
      present : r.filter(x => x.dayStatus === 'Present').length,
      absent  : r.filter(x => x.dayStatus === 'Absent').length,
      late    : r.filter(x => x.dayStatus === 'Late').length,
      unmarked: r.filter(x => !x.dayStatus).length
    };
  });

  ngOnInit(): void {
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      this.classOptions.set((res.data ?? []).map(c => ({ label: c.levelName, value: c.id! })));
    });
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      const opts = (res.data ?? []).map(y => ({ label: y.displayName, value: y.id! }));
      this.yearOptions.set(opts);
      if (opts.length && this.academicYearId === null) this.academicYearId = opts[0].value;
    });

    /* Pull weekend config + holidays inside a wide window so the date picker
     * can disable them. The window covers ~2 years around today which is the
     * realistic "old date" range a teacher might back-date into. */
    const today = new Date();
    const from = new Date(today.getFullYear() - 1, 0, 1);
    const to   = new Date(today.getFullYear() + 1, 11, 31);
    this.calendarSvc.getConfig().subscribe(r => {
      const weekendCsv = r.data?.weekendDays || 'Saturday,Sunday';
      const dayMap: Record<string, number> = {
        Sunday: 0, Monday: 1, Tuesday: 2, Wednesday: 3, Thursday: 4, Friday: 5, Saturday: 6
      };
      this.disabledDays.set(
        weekendCsv.split(',').map(s => s.trim()).filter(Boolean).map(d => dayMap[d]).filter(n => n != null)
      );
    });
    this.calendarSvc.getNonWorkingDates(this.toIso(from), this.toIso(to)).subscribe(r => {
      const dates = (r.data ?? [])
        .filter(nw => !(nw.reason || '').startsWith('Weekend'))  // weekends already handled by disabledDays
        .map(nw => new Date(nw.nonWorkingDate));
      this.disabledDates.set(dates);
    });
  }

  load(): void {
    if (!this.schoolClassId) { this.toast.warn('Select a class.'); return; }
    if (!this.attendanceDate) { this.toast.warn('Pick a date.'); return; }

    this.loading.set(true);
    this.svc.getDailyGrid(this.schoolClassId, this.toIso(this.attendanceDate)).subscribe({
      next: r => {
        const data = (r.data ?? []).map(x => ({ ...x, dayStatus: x.dayStatus ?? 'Present' as DailyStatus }));
        this.rows.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  setAll(status: DailyStatus): void {
    this.rows.set(this.rows().map(r => ({ ...r, dayStatus: status })));
  }

  save(): void {
    if (!this.academicYearId) { this.toast.warn('Select an academic year.'); return; }
    if (!this.schoolClassId)  { this.toast.warn('Select a class.'); return; }
    const r = this.rows();
    if (!r.length) { this.toast.warn('Load a class grid first.'); return; }
    const noStatus = r.find(x => !x.dayStatus);
    if (noStatus) { this.toast.warn(`${noStatus.fullName}: pick Present / Absent / Late.`); return; }

    this.saving.set(true);
    this.svc.bulkSaveDaily({
      academicYearId: this.academicYearId,
      schoolClassId : this.schoolClassId,
      attendanceDate: this.toIso(this.attendanceDate),
      entries: r.map(x => ({ studentId: x.studentId, status: x.dayStatus!, remarks: x.remarks ?? null }))
    }).subscribe({
      next: res => {
        this.saving.set(false);
        if (res.isSuccess) { this.toast.success(res.message || 'Attendance saved.'); this.load(); }
        else this.toast.error(res.message || 'Could not save attendance.');
      },
      error: () => { this.saving.set(false); this.toast.error('Could not save attendance.'); }
    });
  }

  statusSeverity(s?: DailyStatus | null): 'success' | 'danger' | 'warn' | 'secondary' {
    switch (s) {
      case 'Present': return 'success';
      case 'Absent':  return 'danger';
      case 'Late':    return 'warn';
      default:        return 'secondary';
    }
  }

  private todayLocal(): Date { const d = new Date(); d.setHours(0,0,0,0); return d; }
  private toIso(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
