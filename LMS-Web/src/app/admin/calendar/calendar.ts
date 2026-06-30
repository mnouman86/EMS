import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DatePickerModule } from 'primeng/datepicker';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { CalendarService } from './calendar.service';
import { SchoolHoliday, ALL_WEEKDAYS, Weekday } from './calendar.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-calendar-admin',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, CheckboxModule, DatePickerModule, InputTextModule, TagModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './calendar.html'
})
export class CalendarAdmin implements OnInit {
  private svc = inject(CalendarService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  weekdays = [...ALL_WEEKDAYS];
  weekendSet = signal<Set<Weekday>>(new Set(['Saturday', 'Sunday']));
  configSaving = signal(false);

  holidays = signal<SchoolHoliday[]>([]);
  loadingHolidays = signal(false);

  // Add-holiday form
  newDate: Date | null = null;
  newDescription = '';
  adding = signal(false);

  ngOnInit(): void {
    this.loadConfig();
    this.loadHolidays();
  }

  private loadConfig(): void {
    this.svc.getConfig().subscribe(res => {
      const csv = res.data?.weekendDays || 'Saturday,Sunday';
      const set = new Set(
        csv.split(',').map(s => s.trim()).filter(Boolean) as Weekday[]
      );
      this.weekendSet.set(set);
    });
  }

  private loadHolidays(): void {
    this.loadingHolidays.set(true);
    this.svc.getHolidays(null, null).subscribe({
      next: res => { this.holidays.set(res.data ?? []); this.loadingHolidays.set(false); },
      error: () => this.loadingHolidays.set(false)
    });
  }

  isWeekend(day: Weekday): boolean { return this.weekendSet().has(day); }
  toggleWeekend(day: Weekday, on: boolean): void {
    const next = new Set(this.weekendSet());
    if (on) next.add(day); else next.delete(day);
    this.weekendSet.set(next);
  }

  saveConfig(): void {
    const csv = [...this.weekendSet()].join(',');
    if (!csv) { this.toast.warn('Pick at least one weekend day.'); return; }
    this.configSaving.set(true);
    this.svc.setConfig(csv).subscribe({
      next: r => {
        this.configSaving.set(false);
        if (r.isSuccess) this.toast.success('Weekend days saved.');
        else this.toast.error(r.message || 'Could not save.');
      },
      error: () => { this.configSaving.set(false); this.toast.error('Could not save.'); }
    });
  }

  addHoliday(): void {
    if (!this.newDate) { this.toast.warn('Pick a date.'); return; }
    if (!this.newDescription.trim()) { this.toast.warn('Description is required.'); return; }
    const iso = this.toIsoDate(this.newDate);
    this.adding.set(true);
    this.svc.upsertHoliday(iso, this.newDescription.trim()).subscribe({
      next: r => {
        this.adding.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Holiday saved.');
          this.newDate = null; this.newDescription = '';
          this.loadHolidays();
        } else {
          this.toast.error(r.message || 'Could not save.');
        }
      },
      error: () => { this.adding.set(false); this.toast.error('Could not save.'); }
    });
  }

  removeHoliday(h: SchoolHoliday): void {
    this.confirm.confirm({
      header: 'Remove holiday',
      message: `Remove "${h.description}" on ${h.holidayDate?.slice(0, 10)}?`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.svc.deleteHoliday(h.id).subscribe(r => {
          if (r.isSuccess) { this.toast.success('Removed.'); this.loadHolidays(); }
          else this.toast.error(r.message || 'Could not remove.');
        });
      }
    });
  }

  private toIsoDate(d: Date): string {
    // YYYY-MM-DD (no TZ shift)
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
