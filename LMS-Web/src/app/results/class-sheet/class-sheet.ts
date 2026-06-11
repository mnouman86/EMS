import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { MessageModule } from 'primeng/message';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ResultService } from '../result.service';
import { ClassSheetRow, PreLockMissingRow, StudentResultCard } from '../result.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-class-sheet',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputTextModule, TagModule, DialogModule, MessageModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './class-sheet.html'
})
export class ClassSheet implements OnInit {
  private svc = inject(ResultService);
  private classSvc = inject(SchoolClassService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  sessionOptions = signal<{ label: string; value: number }[]>([]);
  classOptions = signal<{ label: string; value: number }[]>([]);
  sessionId: number | null = null;
  classId: number | null = null;

  loading = signal(false);
  loaded = signal(false);
  rows = signal<ClassSheetRow[]>([]);
  busy = signal(false);

  // pre-lock dialog
  preLockDialog = signal(false);
  missing = signal<PreLockMissingRow[]>([]);

  // result card dialog
  cardDialog = signal(false);
  card = signal<StudentResultCard | null>(null);

  ngOnInit(): void {
    this.svc.getSessions(defaultSearch()).subscribe(res => {
      this.sessionOptions.set((res.data ?? []).map(s => ({ label: `${s.name} (${s.status})`, value: s.id })));
    });
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      this.classOptions.set((res.data ?? []).filter(c => c.isActive).map(c => ({ label: c.levelName, value: c.id })));
    });
  }

  load(): void {
    if (!this.sessionId || !this.classId) { this.toast.warn('Select a session and class.'); return; }
    this.loading.set(true);
    this.svc.getClassSheet(this.sessionId, this.classId).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loaded.set(true); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  preLock(): void {
    if (!this.sessionId || !this.classId) return;
    this.busy.set(true);
    this.svc.getPreLockReport(this.sessionId, this.classId).subscribe({
      next: res => { this.missing.set(res.data ?? []); this.busy.set(false); this.preLockDialog.set(true); },
      error: () => this.busy.set(false)
    });
  }

  lock(): void {
    if (!this.sessionId || !this.classId) return;
    this.confirm.confirm({
      header: 'Lock class results',
      message: 'Lock results for this class & session? Marks become read-only until unlocked.',
      icon: 'pi pi-lock',
      accept: () => {
        this.busy.set(true);
        this.svc.lockClass({ resultSessionId: this.sessionId!, schoolClassId: this.classId!, reason: 'Locked from class sheet' }).subscribe({
          next: () => { this.toast.success('Class locked'); this.busy.set(false); this.load(); },
          error: () => this.busy.set(false)
        });
      }
    });
  }

  unlockReason = '';
  unlockDialog = signal(false);
  openUnlock(): void { this.unlockReason = ''; this.unlockDialog.set(true); }
  unlock(): void {
    if (!this.sessionId || !this.classId) return;
    if (!this.unlockReason.trim()) { this.toast.warn('A reason is required to unlock.'); return; }
    this.busy.set(true);
    this.svc.unlockClass({ resultSessionId: this.sessionId, schoolClassId: this.classId, reason: this.unlockReason }).subscribe({
      next: () => { this.toast.success('Class unlocked'); this.busy.set(false); this.unlockDialog.set(false); this.load(); },
      error: () => this.busy.set(false)
    });
  }

  viewCard(row: ClassSheetRow): void {
    if (!this.sessionId) return;
    this.card.set(null);
    this.cardDialog.set(true);
    this.svc.getStudentCard(this.sessionId, row.studentId).subscribe({ next: res => this.card.set(res.data ?? null) });
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
