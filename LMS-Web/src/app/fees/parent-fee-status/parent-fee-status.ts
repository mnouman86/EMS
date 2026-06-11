import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { FeeService } from '../fee.service';
import { ParentFeeSummary } from '../fee.models';

@Component({
  selector: 'app-parent-fee-status',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, ButtonModule, MessageModule, TagModule],
  templateUrl: './parent-fee-status.html',
  styleUrl: './parent-fee-status.scss'
})
export class ParentFeeStatus {
  private svc = inject(FeeService);

  studentCode = '';
  secondFactor = '';

  loading = signal(false);
  error = signal<string | null>(null);
  result = signal<ParentFeeSummary | null>(null);

  search(): void {
    if (!this.studentCode.trim() || !this.secondFactor.trim()) {
      this.error.set('Enter your student code and date of birth / PIN.');
      return;
    }
    this.loading.set(true);
    this.error.set(null);
    this.result.set(null);
    this.svc.parentSearch(this.studentCode.trim(), this.secondFactor.trim()).subscribe({
      next: res => {
        this.loading.set(false);
        if (res.isSuccess && res.data) this.result.set(res.data);
        else this.error.set(res.message || 'No fee record found for the details provided.');
      },
      error: () => { this.loading.set(false); this.error.set('No fee record found for the details provided.'); }
    });
  }

  statusSeverity(status?: string): 'success' | 'warn' | 'danger' | 'secondary' {
    if (!status) return 'secondary';
    const s = status.toLowerCase();
    if (s.includes('paid') || s.includes('clear')) return 'success';
    if (s.includes('partial') || s.includes('due')) return 'warn';
    if (s.includes('overdue') || s.includes('pending')) return 'danger';
    return 'secondary';
  }
}
