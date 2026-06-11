import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ResultService } from '../result.service';
import { GradeBand } from '../result.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-grade-bands',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, InputTextModule, InputNumberModule],
  templateUrl: './grade-bands.html'
})
export class GradeBands implements OnInit {
  private svc = inject(ResultService);
  private toast = inject(ToastService);

  rows = signal<GradeBand[]>([]);
  loading = signal(false);
  saving = signal(false);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getGradeBands().subscribe({
      next: res => {
        const bands = (res.data ?? []).sort((a, b) => a.displayOrder - b.displayOrder);
        this.rows.set(bands.length ? bands : [this.blank(1)]);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  private blank(order: number): GradeBand {
    return { grade: '', minPercent: 0, maxPercent: 0, remarkTemplate: '', displayOrder: order };
  }

  addRow(): void { this.rows.update(r => [...r, this.blank(r.length + 1)]); }
  removeRow(idx: number): void { this.rows.update(r => r.filter((_, i) => i !== idx)); }

  save(): void {
    const bands = this.rows();
    if (bands.length === 0) { this.toast.warn('Add at least one band.'); return; }
    if (bands.some(b => !b.grade.trim())) { this.toast.warn('Every band needs a grade label.'); return; }
    if (bands.some(b => b.minPercent > b.maxPercent)) { this.toast.warn('Min % must be ≤ Max % in every band.'); return; }
    this.saving.set(true);
    const payload = bands.map((b, i) => ({
      grade: b.grade, minPercent: b.minPercent, maxPercent: b.maxPercent,
      remarkTemplate: b.remarkTemplate || null, displayOrder: i + 1
    }));
    this.svc.configureGradeBands({ bands: payload }).subscribe({
      next: () => { this.toast.success('Grade bands saved'); this.saving.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }
}
