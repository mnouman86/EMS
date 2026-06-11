import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { ResultService } from '../result.service';
import { ResultSession } from '../result.models';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

interface ResLink { label: string; icon: string; route: string; desc: string; }

@Component({
  selector: 'app-result-sessions',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, InputTextModule, InputNumberModule, DialogModule, TagModule],
  templateUrl: './result-sessions.html'
})
export class ResultSessions implements OnInit {
  private svc = inject(ResultService);
  private toast = inject(ToastService);

  rows = signal<ResultSession[]>([]);
  loading = signal(false);

  dialog = signal(false);
  saving = signal(false);
  form = { name: '', maxWritten: 70, maxOral: 20, maxAttribute: 10, weightWritten: 60, weightOral: 20, weightPerformance: 20, roundingDecimals: 2 };

  links: ResLink[] = [
    { label: 'Grade Bands', icon: 'pi pi-sliders-h', route: '/admin/results/grade-bands', desc: 'Grade thresholds & remarks' },
    { label: 'Marks Entry', icon: 'pi pi-pencil', route: '/admin/results/marks', desc: 'Enter subject marks per class' },
    { label: 'Class Sheet', icon: 'pi pi-table', route: '/admin/results/class-sheet', desc: 'Results, lock & result cards' }
  ];

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getSessions(defaultSearch()).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  get weightTotal(): number { return this.form.weightWritten + this.form.weightOral + this.form.weightPerformance; }

  open(): void {
    this.form = { name: '', maxWritten: 70, maxOral: 20, maxAttribute: 10, weightWritten: 60, weightOral: 20, weightPerformance: 20, roundingDecimals: 2 };
    this.dialog.set(true);
  }

  save(): void {
    if (!this.form.name.trim()) { this.toast.warn('Session name is required.'); return; }
    if (this.weightTotal !== 100) { this.toast.warn('Weightages must sum to 100.'); return; }
    this.saving.set(true);
    this.svc.createSession({ ...this.form }).subscribe({
      next: () => { this.toast.success('Session created'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  statusSeverity(status?: string): 'success' | 'warn' | 'secondary' {
    if (status === 'Active' || status === 'Open') return 'success';
    if (status === 'Locked' || status === 'Closed') return 'secondary';
    return 'warn';
  }
}
