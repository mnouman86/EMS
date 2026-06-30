import { Component, computed, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { AuthService } from '../../core/services/auth.service';
import { MyTeachingService } from './my-teaching.service';
import { MyTeachingBundle, MyClass, MySubject } from './my-teaching.models';

@Component({
  selector: 'app-my-teaching',
  standalone: true,
  imports: [CommonModule, RouterLink, TableModule, TagModule],
  templateUrl: './my-teaching.html'
})
export class MyTeaching implements OnInit {
  private auth = inject(AuthService);
  private svc = inject(MyTeachingService);

  user = this.auth.currentUser;
  loading = signal(false);
  bundle = signal<MyTeachingBundle | null>(null);

  classesIHead = computed<MyClass[]>(() => this.bundle()?.classesIHead ?? []);
  mySubjects = computed<MySubject[]>(() => this.bundle()?.mySubjects ?? []);

  /** Group the (class, subject) matrix by class for a readable view. */
  byClass = computed(() => {
    const map = new Map<number, { className: string; subjects: string[] }>();
    for (const r of this.bundle()?.classSubjectMap ?? []) {
      const e = map.get(r.schoolClassId) ?? { className: r.levelName, subjects: [] };
      e.subjects.push(r.subjectName);
      map.set(r.schoolClassId, e);
    }
    return Array.from(map.entries()).map(([classId, v]) => ({ classId, ...v }));
  });

  ngOnInit(): void {
    this.loading.set(true);
    this.svc.get().subscribe({
      next: r => { this.bundle.set(r.data ?? null); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
