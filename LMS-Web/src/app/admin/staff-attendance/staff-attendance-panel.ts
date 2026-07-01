import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { AuthService } from '../../core/services/auth.service';
import { Roles } from '../../core/models/roles';
import { ToastService } from '../../core/services/toast.service';
import { StaffAttendanceService } from './staff-attendance.service';

/**
 * Compact Check-In / Check-Out panel. Embed anywhere.
 * Hidden entirely for admin (per spec).
 *
 * Optimizations:
 *   - Reads today's row once per session from the service (shared signal).
 *   - After each action, updates the local signal in place — no reload.
 *   - Buttons disable while in flight to prevent double clicks (belt + suspenders
 *     to the DB's UNIQUE constraint).
 */
@Component({
  selector: 'app-staff-attendance-panel',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    ButtonModule, InputTextModule, DatePickerModule, TagModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './staff-attendance-panel.html'
})
export class StaffAttendancePanel implements OnInit {
  private svc = inject(StaffAttendanceService);
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  today = this.svc.today;
  hasCheckedIn = this.svc.hasCheckedIn;
  hasCheckedOut = this.svc.hasCheckedOut;

  /* Spec: hide entirely for admin. */
  readonly hideForAdmin = computed(() => this.auth.hasAnyRole([Roles.Admin]));

  remarks = '';
  backdatedTime: Date | null = null;
  showBackdated = signal(false);
  busy = signal(false);

  ngOnInit(): void {
    if (!this.hideForAdmin()) this.svc.loadToday().subscribe();
  }

  clickCheckIn(): void {
    const isBack = this.showBackdated() && !!this.backdatedTime;
    if (isBack && !this.remarks.trim()) {
      this.toast.warn('Remarks are required when marking a previous time.');
      return;
    }
    this.confirm.confirm({
      header: 'Confirm check-in',
      message: isBack
        ? `Record check-in at ${this.backdatedTime!.toLocaleTimeString()}?`
        : 'Record check-in with the current time?',
      icon: 'pi pi-sign-in',
      accept: () => this.doCheckIn(isBack)
    });
  }

  private doCheckIn(isBack: boolean): void {
    this.busy.set(true);
    this.svc.checkIn({
      checkInTime: isBack ? this.backdatedTime!.toISOString() : null,
      remarks: this.remarks.trim() || null,
      isBackdated: isBack
    }).subscribe({
      next: res => {
        this.busy.set(false);
        if (res.isSuccess) {
          this.toast.success(res.message || 'Checked in');
          // Optimistic local update — one API call was already made; no refetch.
          this.svc.today.set({
            id: Number(res.data?.recordID ?? 0),
            userId: this.auth.currentUser()?.id ?? 0,
            attendanceDate: new Date().toISOString().slice(0, 10),
            checkInTime: (isBack ? this.backdatedTime! : new Date()).toISOString(),
            checkOutTime: null,
            remarks: this.remarks.trim() || null,
            isBackdated: isBack
          });
          this.remarks = '';
          this.backdatedTime = null;
          this.showBackdated.set(false);
        } else {
          this.toast.error(res.message || 'Check-in failed');
        }
      },
      error: () => { this.busy.set(false); this.toast.error('Check-in failed'); }
    });
  }

  clickCheckOut(): void {
    const isBack = this.showBackdated() && !!this.backdatedTime;
    if (isBack && !this.remarks.trim()) {
      this.toast.warn('Remarks are required when marking a previous time.');
      return;
    }
    this.confirm.confirm({
      header: 'Confirm check-out',
      message: isBack
        ? `Record check-out at ${this.backdatedTime!.toLocaleTimeString()}?`
        : 'Record check-out with the current time?',
      icon: 'pi pi-sign-out',
      accept: () => this.doCheckOut(isBack)
    });
  }

  private doCheckOut(isBack: boolean): void {
    this.busy.set(true);
    const outIso = isBack ? this.backdatedTime!.toISOString() : new Date().toISOString();
    this.svc.checkOut({
      checkOutTime: isBack ? outIso : null,
      remarks: this.remarks.trim() || null,
      isBackdated: isBack
    }).subscribe({
      next: res => {
        this.busy.set(false);
        if (res.isSuccess) {
          this.toast.success(res.message || 'Checked out');
          const cur = this.today();
          if (cur) this.svc.today.set({ ...cur, checkOutTime: outIso, remarks: this.remarks.trim() || cur.remarks });
          this.remarks = '';
          this.backdatedTime = null;
          this.showBackdated.set(false);
        } else {
          this.toast.error(res.message || 'Check-out failed');
        }
      },
      error: () => { this.busy.set(false); this.toast.error('Check-out failed'); }
    });
  }

  /* Only allow backdated times within TODAY (max = now, min = start of today). */
  todayMin = new Date(new Date().setHours(0, 0, 0, 0));
  todayMax = new Date();
}
