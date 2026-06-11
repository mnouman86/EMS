import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { MessageModule } from 'primeng/message';
import { ResultService } from '../result.service';
import { MarksEntryGridRow, ResultSession } from '../result.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { SubjectService } from '../../admin/subjects/subject.service';
import { EmployeeService } from '../../admin/employees/employee.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-marks-entry',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, InputNumberModule, TagModule, MessageModule],
  templateUrl: './marks-entry.html'
})
export class MarksEntry implements OnInit {
  private svc = inject(ResultService);
  private classSvc = inject(SchoolClassService);
  private subjectSvc = inject(SubjectService);
  private employeeSvc = inject(EmployeeService);
  private toast = inject(ToastService);

  sessionOptions = signal<{ label: string; value: number }[]>([]);
  classOptions = signal<{ label: string; value: number }[]>([]);
  subjectOptions = signal<{ label: string; value: number }[]>([]);
  teacherOptions = signal<{ label: string; value: number }[]>([]);
  private sessions = new Map<number, ResultSession>();

  sessionId: number | null = null;
  classId: number | null = null;
  subjectId: number | null = null;
  teacherId: number | null = null;

  loading = signal(false);
  saving = signal(false);
  loaded = signal(false);
  rows = signal<MarksEntryGridRow[]>([]);

  get maxWritten(): number { return this.sessionId ? (this.sessions.get(this.sessionId)?.maxWritten ?? 100) : 100; }
  get maxOral(): number { return this.sessionId ? (this.sessions.get(this.sessionId)?.maxOral ?? 100) : 100; }
  get maxAttr(): number { return this.sessionId ? (this.sessions.get(this.sessionId)?.maxAttribute ?? 100) : 100; }
  get locked(): boolean { return this.rows().some(r => r.isLocked); }

  ngOnInit(): void {
    forkJoin({
      sessions: this.svc.getSessions(defaultSearch()),
      classes: this.classSvc.getAll(defaultSearch()),
      subjects: this.subjectSvc.getAll(defaultSearch()),
      employees: this.employeeSvc.getAll(defaultSearch())
    }).subscribe(({ sessions, classes, subjects, employees }) => {
      (sessions.data ?? []).forEach(s => this.sessions.set(s.id, s));
      this.sessionOptions.set((sessions.data ?? []).map(s => ({ label: `${s.name} (${s.status})`, value: s.id })));
      this.classOptions.set((classes.data ?? []).filter(c => c.isActive).map(c => ({ label: c.levelName, value: c.id })));
      this.subjectOptions.set((subjects.data ?? []).filter(s => s.isActive).map(s => ({ label: s.name, value: s.id })));
      this.teacherOptions.set((employees.data ?? []).filter(e => e.status !== 'Left').map(e => ({ label: `${e.fullName} (${e.employeeCode})`, value: e.id })));
    });
  }

  load(): void {
    if (!this.sessionId || !this.classId || !this.subjectId || !this.teacherId) {
      this.toast.warn('Select session, class, subject and teacher.'); return;
    }
    this.loading.set(true);
    this.svc.getMarksGrid({ resultSessionId: this.sessionId, schoolClassId: this.classId, subjectId: this.subjectId, teacherEmployeeId: this.teacherId }).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loaded.set(true); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  save(): void {
    if (!this.sessionId || !this.classId || !this.subjectId || !this.teacherId) return;
    if (this.locked) { this.toast.warn('This class is locked for the session — unlock before editing.'); return; }
    this.saving.set(true);
    const entries = this.rows().map(r => ({
      studentId: r.studentId,
      written: r.written ?? null,
      oral: r.oral ?? null,
      attrPunctuality: r.attrPunctuality ?? null,
      attrDiscipline: r.attrDiscipline ?? null,
      attrClassParticipation: r.attrClassParticipation ?? null,
      attrCreativity: r.attrCreativity ?? null,
      attrBehaviorWithPeers: r.attrBehaviorWithPeers ?? null
    }));
    this.svc.enterMarks({ resultSessionId: this.sessionId, schoolClassId: this.classId, subjectId: this.subjectId, teacherEmployeeId: this.teacherId, entries }).subscribe({
      next: () => { this.toast.success('Marks saved'); this.saving.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }
}
