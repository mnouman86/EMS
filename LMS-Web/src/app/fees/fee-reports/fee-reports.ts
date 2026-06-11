import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { FeeService } from '../fee.service';
import { PendingFeeRow, AgeingRow } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';

@Component({
  selector: 'app-fee-reports',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, SelectModule, InputNumberModule, TagModule
  ],
  templateUrl: './fee-reports.html'
})
export class FeeReports implements OnInit {
  private svc = inject(FeeService);
  private classSvc = inject(SchoolClassService);

  monthOptions = [
    { label: 'January', value: 1 }, { label: 'February', value: 2 }, { label: 'March', value: 3 }, { label: 'April', value: 4 },
    { label: 'May', value: 5 }, { label: 'June', value: 6 }, { label: 'July', value: 7 }, { label: 'August', value: 8 },
    { label: 'September', value: 9 }, { label: 'October', value: 10 }, { label: 'November', value: 11 }, { label: 'December', value: 12 }
  ];
  classOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All classes', value: null }]);

  // Pending
  pendingClassId: number | null = null;
  minOutstanding: number | null = null;
  minDaysOverdue: number | null = null;
  pending = signal<PendingFeeRow[]>([]);
  pendingLoading = signal(false);

  // Monthly non-submitted
  nsMonth = new Date().getMonth() + 1;
  nsYear = new Date().getFullYear();
  nsClassId: number | null = null;
  nonSubmitted = signal<PendingFeeRow[]>([]);
  nsLoading = signal(false);

  // Ageing
  ageingClassId: number | null = null;
  ageing = signal<AgeingRow[]>([]);
  ageingLoading = signal(false);

  ngOnInit(): void {
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      const active = (res.data ?? []).filter(c => c.isActive);
      this.classOptions.set([{ label: 'All classes', value: null }, ...active.map(c => ({ label: c.levelName, value: c.id as number | null }))]);
    });
    this.loadPending();
  }

  loadPending(): void {
    this.pendingLoading.set(true);
    this.svc.getPendingList(this.pendingClassId, this.minOutstanding, this.minDaysOverdue).subscribe({
      next: res => { this.pending.set(res.data ?? []); this.pendingLoading.set(false); },
      error: () => this.pendingLoading.set(false)
    });
  }

  loadNonSubmitted(): void {
    this.nsLoading.set(true);
    this.svc.getMonthlyNonSubmitted(this.nsMonth, this.nsYear, this.nsClassId).subscribe({
      next: res => { this.nonSubmitted.set(res.data ?? []); this.nsLoading.set(false); },
      error: () => this.nsLoading.set(false)
    });
  }

  loadAgeing(): void {
    this.ageingLoading.set(true);
    this.svc.getOutstandingAgeing(this.ageingClassId).subscribe({
      next: res => { this.ageing.set(res.data ?? []); this.ageingLoading.set(false); },
      error: () => this.ageingLoading.set(false)
    });
  }
}
