import { Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { ParentService } from '../parent.service';
import { StudentDetail } from '../../students/student.models';

@Component({
  selector: 'app-parent-profile',
  standalone: true,
  imports: [CommonModule, TagModule],
  templateUrl: './parent-profile.html'
})
export class ParentProfile {
  parent = inject(ParentService);

  loading = signal(false);
  profile = signal<StudentDetail | null>(null);
  private lastLoadedId: number | null = null;

  constructor() {
    this.parent.loadChildren().subscribe();
    // Reload whenever the selected child changes.
    effect(() => {
      const id = this.parent.selectedChildId();
      if (id && id !== this.lastLoadedId) this.load(id);
    });
  }

  private load(studentId: number): void {
    this.lastLoadedId = studentId;
    this.loading.set(true);
    this.profile.set(null);
    this.parent.getChildProfile(studentId).subscribe({
      next: res => { this.profile.set(res.data ?? null); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
