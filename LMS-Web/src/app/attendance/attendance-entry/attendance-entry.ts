import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { AttendanceService } from '../attendance.service';
import { AttendanceGridRow } from '../attendance.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

interface YearOption  { label: string; value: number }
interface ClassOption { label: string; value: number }

@Component({
  selector: 'app-attendance-entry',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, SelectModule, ButtonModule, InputNumberModule, InputTextModule, TagModule
  ],
  templateUrl: './attendance-entry.html'
})
export class AttendanceEntry implements OnInit {
  private svc = inject(AttendanceService);
  private classSvc = inject(SchoolClassService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);

  /* filters */
  yearOptions  = signal<YearOption[]>([]);
  classOptions = signal<ClassOption[]>([]);
  monthOptions: { label: string; value: number }[] = [
    { label: 'January',   value: 1 }, { label: 'February', value: 2 },
    { label: 'March',     value: 3 }, { label: 'April',    value: 4 },
    { label: 'May',       value: 5 }, { label: 'June',     value: 6 },
    { label: 'July',      value: 7 }, { label: 'August',   value: 8 },
    { label: 'September', value: 9 }, { label: 'October',  value: 10 },
    { label: 'November',  value: 11 }, { label: 'December',value: 12 }
  ];
  academicYearId: number | null = null;
  schoolClassId: number | null = null;
  periodYear: number = new Date().getFullYear();
  periodMonth: number = new Date().getMonth() + 1;

  loading = signal(false);
  saving  = signal(false);
  rows    = signal<AttendanceGridRow[]>([]);

  /* live totals from the grid */
  totals = computed(() => {
    const r = this.rows();
    const wd = r.reduce((s, x) => s + (x.workingDays || 0), 0);
    const pd = r.reduce((s, x) => s + (x.presentDays || 0), 0);
    return { count: r.length, workingDays: wd, presentDays: pd };
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
  }

  load(): void {
    if (!this.schoolClassId) { this.toast.warn('Select a class.'); return; }
    if (!this.periodYear || !this.periodMonth) { this.toast.warn('Select year and month.'); return; }
    this.loading.set(true);
    this.svc.getClassGrid(this.schoolClassId, this.periodYear, this.periodMonth).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  fillAllPresent(): void {
    // Convenience: copy WorkingDays into PresentDays for every row that has working days set.
    const r = this.rows().map(x => ({ ...x, presentDays: x.workingDays || x.presentDays }));
    this.rows.set(r);
  }

  setWorkingDaysForAll(wd: number | null): void {
    if (wd == null || wd < 0) return;
    const r = this.rows().map(x => ({ ...x, workingDays: wd }));
    this.rows.set(r);
  }

  save(): void {
    if (!this.academicYearId) { this.toast.warn('Select an academic year.'); return; }
    if (!this.schoolClassId)  { this.toast.warn('Select a class.'); return; }
    const r = this.rows();
    if (!r.length) { this.toast.warn('Load a class grid first.'); return; }
    const invalid = r.find(x => (x.presentDays ?? 0) < 0 || (x.workingDays ?? 0) < 0 || (x.presentDays ?? 0) > (x.workingDays ?? 0));
    if (invalid) { this.toast.warn(`${invalid.fullName}: Present days must be 0..Working days.`); return; }

    this.saving.set(true);
    this.svc.bulkSave({
      academicYearId: this.academicYearId,
      schoolClassId : this.schoolClassId,
      periodYear    : this.periodYear,
      periodMonth   : this.periodMonth,
      entries: r.map(x => ({
        studentId   : x.studentId,
        workingDays : x.workingDays ?? 0,
        presentDays : x.presentDays ?? 0,
        remarks     : x.remarks ?? null
      }))
    }).subscribe({
      next: res => {
        this.saving.set(false);
        if (res.isSuccess) { this.toast.success(res.message || 'Attendance saved.'); this.load(); }
        else this.toast.error(res.message || 'Could not save attendance.');
      },
      error: () => { this.saving.set(false); this.toast.error('Could not save attendance.'); }
    });
  }

  percent(row: AttendanceGridRow): number | null {
    if (!row.workingDays) return null;
    return Math.round((row.presentDays / row.workingDays) * 1000) / 10;
  }
}
