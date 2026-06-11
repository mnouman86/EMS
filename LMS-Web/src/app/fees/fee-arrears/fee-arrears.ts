import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FeeService } from '../fee.service';
import { FeeArrearRow } from '../fee.models';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { AuthService } from '../../core/services/auth.service';
import { Roles } from '../../core/models/roles';

@Component({
  selector: 'app-fee-arrears',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputTextModule, ToggleSwitchModule, TagModule, DialogModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './fee-arrears.html'
})
export class FeeArrears implements OnInit {
  private svc = inject(FeeService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);
  private confirm = inject(ConfirmationService);

  canCarryForward = this.auth.hasAnyRole([Roles.Admin]);
  canWriteOff = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);

  yearOptions = signal<{ label: string; value: number }[]>([]);
  fromYearId: number | null = null;
  toYearId: number | null = null;
  carrying = signal(false);

  rows = signal<FeeArrearRow[]>([]);
  loading = signal(false);
  includeWrittenOff = false;

  // write-off dialog
  dialog = signal(false);
  saving = signal(false);
  target = signal<FeeArrearRow | null>(null);
  reason = '';

  ngOnInit(): void {
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      this.yearOptions.set((res.data ?? []).map(y => ({ label: y.displayName, value: y.id })));
    });
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.svc.getArrears(null, this.includeWrittenOff).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  carryForward(): void {
    if (!this.fromYearId || !this.toYearId) { this.toast.warn('Pick both academic years.'); return; }
    if (this.fromYearId === this.toYearId) { this.toast.warn('From and To years must differ.'); return; }
    this.confirm.confirm({
      header: 'Carry forward arrears',
      message: 'Carry forward outstanding balances from the source year into the target year? Existing arrears for the same pair are skipped.',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.carrying.set(true);
        this.svc.carryForwardArrears({ fromAcademicYearId: this.fromYearId!, toAcademicYearId: this.toYearId! }).subscribe({
          next: () => { this.toast.success('Arrears carried forward'); this.carrying.set(false); this.load(); },
          error: () => this.carrying.set(false)
        });
      }
    });
  }

  openWriteOff(a: FeeArrearRow): void {
    this.target.set(a);
    this.reason = '';
    this.dialog.set(true);
  }

  writeOff(): void {
    const a = this.target();
    if (!a) return;
    if (!this.reason.trim()) { this.toast.warn('A reason is required.'); return; }
    this.saving.set(true);
    this.svc.writeOffArrear({ arrearId: a.id, reason: this.reason, approvedByUserId: this.auth.currentUser()?.id ?? null }).subscribe({
      next: () => { this.toast.success('Arrear written off'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }
}
