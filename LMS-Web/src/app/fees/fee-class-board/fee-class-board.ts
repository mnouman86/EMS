import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { TagModule } from 'primeng/tag';
import { ProgressBarModule } from 'primeng/progressbar';
import { TooltipModule } from 'primeng/tooltip';
import { FeeService } from '../fee.service';
import { FeeClassBoardRow, FeeType } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { AuthService } from '../../core/services/auth.service';

/* One row + UI state for the bulk-collection grid. */
interface BoardRow extends FeeClassBoardRow {
  selected: boolean;
  amountToCollect: number;
  rowReferenceNo: string;
  status?: 'pending' | 'ok' | 'fail';
  statusMessage?: string;
}

@Component({
  selector: 'app-fee-class-board',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, MultiSelectModule, CheckboxModule,
    InputNumberModule, InputTextModule, DatePickerModule, TagModule, ProgressBarModule, TooltipModule
  ],
  templateUrl: './fee-class-board.html'
})
export class FeeClassBoard implements OnInit {
  private feeSvc = inject(FeeService);
  private classSvc = inject(SchoolClassService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);

  /* Filter options */
  classOptions = signal<{ label: string; value: number }[]>([]);
  feeTypeOptions = signal<{ label: string; value: number; name: string; category: string }[]>([]);
  modeOptions = ['Cash', 'Cheque', 'BankTransfer', 'Online'].map(v => ({ label: v, value: v }));
  monthOptions = [
    { label: '(any month — lifetime)', value: null as number | null },
    ...['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'].map((m, i) => ({ label: m, value: i + 1 }))
  ];

  /* Filter state */
  classId: number | null = null;
  periodYear: number | null = new Date().getFullYear();
  periodMonth: number | null = null;

  /* Default payment fields */
  paymentDate: Date = new Date();
  defaultAmount: number | null = null;
  paymentMode = 'Cash';
  referenceNoPrefix = '';
  feeTypesCovered: number[] = [];
  remarks = '';

  /* Data */
  loading = signal(false);
  rows = signal<BoardRow[]>([]);

  /* Bulk record progress */
  recording = signal(false);
  recordedCount = signal(0);
  failedCount = signal(0);
  totalToRecord = signal(0);

  /* KPIs computed off the loaded rows */
  kpis = computed(() => {
    const r = this.rows();
    const studentsWithDue = r.filter(x => x.outstanding > 0).length;
    return {
      totalStudents: r.length,
      studentsWithDue,
      studentsClear: r.length - studentsWithDue,
      totalInvoiced: r.reduce((a, b) => a + (b.totalInvoiced || 0), 0),
      totalPaid: r.reduce((a, b) => a + (b.totalPaid || 0), 0),
      totalOutstanding: r.reduce((a, b) => a + (b.outstanding || 0), 0)
    };
  });

  selectionSummary = computed(() => {
    const sel = this.rows().filter(r => r.selected);
    return {
      count: sel.length,
      total: sel.reduce((a, b) => a + (b.amountToCollect || 0), 0)
    };
  });

  ngOnInit(): void {
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      this.classOptions.set((res.data ?? []).map(c => ({ label: c.levelName, value: c.id! })));
    });
    this.feeSvc.getTypes(defaultSearch()).subscribe(res => {
      this.feeTypeOptions.set((res.data ?? []).map((t: FeeType) => ({
        label: `${t.name} (${t.category})`,
        value: t.id,
        name: t.name,
        category: t.category
      })));
    });
  }

  load(): void {
    if (!this.classId) { this.toast.warn('Please pick a class first.'); return; }
    this.loading.set(true);
    this.recordedCount.set(0); this.failedCount.set(0); this.totalToRecord.set(0);
    this.feeSvc.getClassBoard(this.classId, this.periodYear, this.periodMonth).subscribe({
      next: res => {
        const rows = (res.data ?? []).map<BoardRow>(r => ({
          ...r,
          selected: false,
          // Pre-fill amount with outstanding so a single "Select all" + "Record" handles the common case.
          amountToCollect: r.outstanding > 0 ? r.outstanding : 0,
          rowReferenceNo: ''
        }));
        this.rows.set(rows);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  /* --- Selection helpers --- */
  toggleAll(checked: boolean): void {
    this.rows.update(rows => rows.map(r => ({ ...r, selected: checked && r.outstanding > 0 })));
  }
  selectAllWithDue(): void { this.toggleAll(true); }
  clearSelection(): void { this.toggleAll(false); }

  /** Apply the default amount to every selected row that hasn't been tweaked. */
  applyDefaultToSelected(): void {
    if (this.defaultAmount == null || this.defaultAmount <= 0) {
      this.toast.warn('Set a default amount first.'); return;
    }
    const amt = this.defaultAmount;
    this.rows.update(rows => rows.map(r => r.selected ? { ...r, amountToCollect: amt } : r));
  }

  /** Reset selected rows' amount back to each student's outstanding. */
  fillSelectedWithOutstanding(): void {
    this.rows.update(rows => rows.map(r => r.selected ? { ...r, amountToCollect: r.outstanding } : r));
  }

  outstandingSeverity(o: number): 'success' | 'warn' | 'danger' {
    if (o <= 0) return 'success';
    if (o < 5000) return 'warn';
    return 'danger';
  }

  /* --- Bulk record --- */
  private buildRemarks(): string {
    const parts: string[] = [];
    if (this.feeTypesCovered.length) {
      const names = this.feeTypeOptions().filter(o => this.feeTypesCovered.includes(o.value)).map(o => o.name);
      if (names.length) parts.push(`Covers: ${names.join(', ')}`);
    }
    if (this.periodYear && this.periodMonth) {
      const m = this.monthOptions.find(o => o.value === this.periodMonth)?.label;
      if (m) parts.push(`Period: ${m}/${this.periodYear}`);
    }
    if (this.remarks.trim()) parts.push(this.remarks.trim());
    return parts.join(' · ') || '';
  }

  recordAll(): void {
    const selected = this.rows().filter(r => r.selected);
    if (!selected.length) { this.toast.warn('Tick at least one student.'); return; }
    if (selected.some(r => (r.amountToCollect ?? 0) <= 0)) {
      this.toast.warn('Some selected rows have amount 0 — set an amount or untick them.'); return;
    }
    if ((this.paymentMode === 'Cheque' || this.paymentMode === 'BankTransfer' || this.paymentMode === 'Online')
        && !this.referenceNoPrefix.trim() && selected.some(r => !r.rowReferenceNo.trim())) {
      this.toast.warn('Reference no. is required for non-cash modes. Use the prefix field or fill per-row.');
      return;
    }

    const date = (this.paymentDate || new Date()).toISOString();
    const mode = this.paymentMode;
    const remarksBase = this.buildRemarks();
    const collectingStaffId = this.auth.currentUser()?.id ?? 0;

    this.recording.set(true);
    this.recordedCount.set(0);
    this.failedCount.set(0);
    this.totalToRecord.set(selected.length);

    // Mark all selected rows as pending so the user sees what's queued.
    this.rows.update(rows => rows.map(r =>
      r.selected ? { ...r, status: 'pending', statusMessage: undefined } : r));

    // Sequential to keep things simple + avoid hammering a per-receipt-no counter.
    const queue = [...selected];
    const next = () => {
      if (!queue.length) { this.recording.set(false); this.load(); return; }
      const row = queue.shift()!;
      const ref = row.rowReferenceNo.trim() ||
                  (this.referenceNoPrefix.trim() ? `${this.referenceNoPrefix.trim()}-${row.studentId}` : '');

      this.feeSvc.recordPayment({
        studentId: row.studentId,
        paymentDate: date,
        amount: row.amountToCollect,
        paymentMode: mode,
        referenceNo: ref || null,
        remarks: remarksBase || null,
        collectingStaffId
      }).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.recordedCount.update(n => n + 1);
            this.rows.update(rows => rows.map(r =>
              r.studentId === row.studentId ? { ...r, status: 'ok', statusMessage: res.message ?? 'Recorded' } : r));
          } else {
            this.failedCount.update(n => n + 1);
            this.rows.update(rows => rows.map(r =>
              r.studentId === row.studentId ? { ...r, status: 'fail', statusMessage: res.message ?? 'Failed' } : r));
          }
          next();
        },
        error: e => {
          this.failedCount.update(n => n + 1);
          this.rows.update(rows => rows.map(r =>
            r.studentId === row.studentId ? { ...r, status: 'fail', statusMessage: e?.message ?? 'Network error' } : r));
          next();
        }
      });
    };
    next();
  }

  recordingProgress = computed(() => {
    const t = this.totalToRecord();
    if (!t) return 0;
    return Math.round(((this.recordedCount() + this.failedCount()) / t) * 100);
  });
}
