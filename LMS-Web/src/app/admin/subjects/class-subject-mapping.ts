import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SelectModule } from 'primeng/select';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { forkJoin } from 'rxjs';
import { SubjectService } from './subject.service';
import { Subject } from './subject.models';
import { SchoolClassService } from '../classes/school-class.service';
import { SchoolClass } from '../classes/school-class.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-class-subject-mapping',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, SelectModule, CheckboxModule, ButtonModule, TagModule],
  templateUrl: './class-subject-mapping.html'
})
export class ClassSubjectMapping implements OnInit {
  private subjectSvc = inject(SubjectService);
  private classSvc = inject(SchoolClassService);
  private toast = inject(ToastService);

  classes = signal<SchoolClass[]>([]);
  subjects = signal<Subject[]>([]);
  selectedClassId = signal<number | null>(null);
  /** subjectId -> checked */
  selection = signal<Record<number, boolean>>({});
  loading = signal(false);
  saving = signal(false);

  selectedCount = computed(() => Object.values(this.selection()).filter(Boolean).length);

  ngOnInit(): void {
    this.loading.set(true);
    forkJoin({
      classes: this.classSvc.getAll(),
      subjects: this.subjectSvc.getAll()
    }).subscribe({
      next: ({ classes, subjects }) => {
        this.classes.set((classes.data ?? []).filter(c => c.isActive));
        this.subjects.set((subjects.data ?? []).filter(s => s.isActive));
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  onClassChange(): void {
    const classId = this.selectedClassId();
    if (!classId) { this.selection.set({}); return; }
    this.loading.set(true);
    this.subjectSvc.getByClass(classId).subscribe({
      next: res => {
        const map: Record<number, boolean> = {};
        (res.data ?? []).forEach(cs => (map[cs.subjectId] = true));
        this.selection.set(map);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  toggle(subjectId: number, checked: boolean): void {
    this.selection.update(m => ({ ...m, [subjectId]: checked }));
  }

  isChecked(subjectId: number): boolean {
    return !!this.selection()[subjectId];
  }

  save(): void {
    const classId = this.selectedClassId();
    if (!classId) { this.toast.warn('Select a class first.'); return; }
    const subjectIds = Object.entries(this.selection())
      .filter(([, v]) => v)
      .map(([k]) => Number(k));

    this.saving.set(true);
    this.subjectSvc.mapToClass(classId, subjectIds).subscribe({
      next: () => { this.toast.success('Subjects mapped to class'); this.saving.set(false); },
      error: () => this.saving.set(false)
    });
  }
}
