import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { ResultService } from '../result.service';
import { ParentResultSummary } from '../result.models';

@Component({
  selector: 'app-parent-result',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, ButtonModule, MessageModule, TagModule],
  templateUrl: './parent-result.html',
  styleUrl: './parent-result.scss'
})
export class ParentResult {
  private svc = inject(ResultService);

  studentCode = '';
  secondFactor = '';

  loading = signal(false);
  error = signal<string | null>(null);
  result = signal<ParentResultSummary | null>(null);

  search(): void {
    if (!this.studentCode.trim() || !this.secondFactor.trim()) {
      this.error.set('Enter your student ID and date of birth / PIN.');
      return;
    }
    this.loading.set(true);
    this.error.set(null);
    this.result.set(null);
    this.svc.parentSearch(this.studentCode.trim(), this.secondFactor.trim()).subscribe({
      next: res => {
        this.loading.set(false);
        if (res.isSuccess && res.data) this.result.set(res.data);
        else this.error.set(res.message || 'No published result found for the details provided.');
      },
      error: () => { this.loading.set(false); this.error.set('No published result found for the details provided.'); }
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
