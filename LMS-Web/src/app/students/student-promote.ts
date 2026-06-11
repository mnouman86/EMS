import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { MessageModule } from 'primeng/message';
import { StudentService } from './student.service';
import { StudentListItem } from './student.models';
import { SchoolClassService } from '../admin/classes/school-class.service';
import { defaultSearch, FilterParameter } from '../core/models/search-request';
import { ToastService } from '../core/services/toast.service';

@Component({
  selector: 'app-student-promote',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, SelectModule, ToggleSwitchModule, MessageModule],
  templateUrl: './student-promote.html'
})
export class StudentPromote implements OnInit {
  private svc = inject(StudentService);
  private classSvc = inject(SchoolClassService);
  private router = inject(Router);
  private toast = inject(ToastService);

  classOptions = signal<{ label: string; value: number }[]>([]);
  sourceClassId: number | null = null;
  targetClassId: number | null = null;
  moveToAlumni = false;

  loading = signal(false);
  saving = signal(false);
  students = signal<StudentListItem[]>([]);
  selected = signal<StudentListItem[]>([]);

  ngOnInit(): void {
    this.classSvc.getAll(defaultSearch()).subscribe({
      next: res => {
        const active = (res.data ?? []).filter(c => c.isActive);
        this.classOptions.set(active.map(c => ({ label: c.levelName, value: c.id })));
      }
    });
  }

  onSourceChange(): void {
    this.students.set([]);
    this.selected.set([]);
    if (!this.sourceClassId) return;
    this.loading.set(true);
    const filters: FilterParameter[] = [
      { parameterName: 'AdmittedClassId', parameterValue: String(this.sourceClassId) },
      { parameterName: 'Status', parameterValue: 'Active' }
    ];
    this.svc.getAll(defaultSearch({ filterArray: filters })).subscribe({
      next: res => { this.students.set(res.data ?? []); this.selected.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  submit(): void {
    if (!this.sourceClassId) { this.toast.warn('Pick the source class.'); return; }
    if (!this.moveToAlumni && !this.targetClassId) { this.toast.warn('Pick a target class or enable "Move to Alumni".'); return; }
    if (!this.moveToAlumni && this.targetClassId === this.sourceClassId) { this.toast.warn('Target class must differ from source.'); return; }
    const ids = this.selected().map(s => s.id);
    if (ids.length === 0) { this.toast.warn('Select at least one student.'); return; }

    this.saving.set(true);
    this.svc.promote({
      sourceClassId: this.sourceClassId,
      targetClassId: this.moveToAlumni ? this.sourceClassId : this.targetClassId!,
      studentIds: ids,
      moveToAlumni: this.moveToAlumni
    }).subscribe({
      next: () => {
        this.toast.success(this.moveToAlumni ? `${ids.length} student(s) moved to Alumni` : `${ids.length} student(s) promoted`);
        this.saving.set(false);
        this.router.navigate(['/admin/students']);
      },
      error: () => this.saving.set(false)
    });
  }
}
