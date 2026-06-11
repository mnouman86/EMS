import { Component, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ParentService } from '../parent.service';
import { ResultSession, StudentResultCard } from '../../results/result.models';

@Component({
  selector: 'app-parent-results',
  standalone: true,
  imports: [CommonModule, FormsModule, SelectModule, TableModule, TagModule],
  templateUrl: './parent-results.html'
})
export class ParentResults {
  parent = inject(ParentService);

  sessionOptions = signal<{ label: string; value: number }[]>([]);
  sessionId: number | null = null;

  loading = signal(false);
  card = signal<StudentResultCard | null>(null);
  notFound = signal(false);
  private lastKey = '';

  constructor() {
    this.parent.loadChildren().subscribe();
    this.parent.getResultSessions().subscribe(res => {
      const sessions = (res.data ?? []) as ResultSession[];
      this.sessionOptions.set(sessions.map(s => ({ label: s.name, value: s.id })));
      if (sessions.length && this.sessionId === null) { this.sessionId = sessions[0].id; }
      this.maybeLoad();
    });
    // Reload when the selected child changes.
    effect(() => { this.parent.selectedChildId(); this.maybeLoad(); });
  }

  onSessionChange(): void { this.maybeLoad(); }

  private maybeLoad(): void {
    const studentId = this.parent.selectedChildId();
    if (!studentId || this.sessionId === null) return;
    const key = `${studentId}:${this.sessionId}`;
    if (key === this.lastKey) return;
    this.lastKey = key;

    this.loading.set(true);
    this.card.set(null);
    this.notFound.set(false);
    this.parent.getChildResultCard(studentId, this.sessionId).subscribe({
      next: res => {
        this.loading.set(false);
        if (res.isSuccess && res.data) this.card.set(res.data);
        else this.notFound.set(true);
      },
      error: () => { this.loading.set(false); this.notFound.set(true); }
    });
  }

  gradeSeverity(grade?: string | null): 'success' | 'info' | 'warn' | 'danger' | 'secondary' {
    if (!grade) return 'secondary';
    const g = grade.toUpperCase();
    if (g.startsWith('A')) return 'success';
    if (g.startsWith('B')) return 'info';
    if (g.startsWith('C')) return 'warn';
    return 'danger';
  }
}
