import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmationService } from 'primeng/api';
import { FeeService } from '../fee.service';
import { InvoicePreviewRow, InvoiceOverrideInput } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-fee-generate',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, MessageModule, TagModule, ConfirmDialogModule, TooltipModule
  ],
  providers: [ConfirmationService],
  templateUrl: './fee-generate.html'
})
export class FeeGenerate implements OnInit {
  private svc = inject(FeeService);
  private classSvc = inject(SchoolClassService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  monthOptions = [
    { label: 'January', value: 1 }, { label: 'February', value: 2 }, { label: 'March', value: 3 }, { label: 'April', value: 4 },
    { label: 'May', value: 5 }, { label: 'June', value: 6 }, { label: 'July', value: 7 }, { label: 'August', value: 8 },
    { label: 'September', value: 9 }, { label: 'October', value: 10 }, { label: 'November', value: 11 }, { label: 'December', value: 12 }
  ];
  classOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All classes', value: null }]);
  yearOptions = signal<{ label: string; value: number }[]>([]);

  yearId: number | null = null;
  month: number = new Date().getMonth() + 1;
  year: number = new Date().getFullYear();
  classId: number | null = null;

  previewing = signal(false);
  generating = signal(false);
  previewed = signal(false);
  /** True once Generate has completed for this batch — locks the grid and
   *  reveals per-row download-PDF buttons. Reset by "Start new batch". */
  generatedBatch = signal(false);
  rows = signal<InvoicePreviewRow[]>([]);
  selected = signal<InvoicePreviewRow[]>([]);

  billableRows = computed(() => this.rows().filter(r => !r.note));
  /** Sum of TOTAL PAYABLE (net + arrears) for selected rows. Depends on rows()
   *  so per-cell edits (which touch rows via a spread reassignment) refresh it. */
  totalNet = computed(() => {
    this.rows();          // subscribe so edits refresh
    return this.selected().reduce((sum, r) => sum + (r.totalPayable ?? r.netAmount), 0);
  });
  /** Rows whose Net was edited away from the original. */
  overriddenRows = computed(() => {
    this.rows();
    return this.selected().filter(r => r._originalNetAmount != null && r.netAmount !== r._originalNetAmount);
  });

  ngOnInit(): void {
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      const active = (res.data ?? []).filter(c => c.isActive);
      this.classOptions.set([{ label: 'All classes', value: null }, ...active.map(c => ({ label: c.levelName, value: c.id as number | null }))]);
    });
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      this.yearOptions.set((res.data ?? []).map(y => ({ label: y.displayName, value: y.id })));
      const open = (res.data ?? []).find(y => y.isOpen);
      if (open) this.yearId = open.id;
    });
  }

  preview(): void {
    if (!this.yearId) { this.toast.warn('Pick an academic year.'); return; }
    this.previewing.set(true);
    this.generatedBatch.set(false);
    this.svc.generateInvoices({
      academicYearId: this.yearId, billingMonth: this.month, billingYear: this.year, classId: this.classId, dryRun: true
    }).subscribe({
      next: res => {
        const data = (res.data ?? []).map(r => ({ ...r, _originalNetAmount: r.netAmount, overrideReason: '' }));
        this.rows.set(data);
        this.selected.set(data.filter(r => !r.note));
        this.previewed.set(true);
        this.previewing.set(false);
      },
      error: () => this.previewing.set(false)
    });
  }

  /** Called via (ngModelChange) whenever an editable Net cell changes.
   *  ngModelChange fires AFTER Angular writes the new value back into r.netAmount,
   *  so we can safely recompute totalPayable from it. We then create new array
   *  references for both rows() and selected() so any computed derived from
   *  either recomputes (totalNet, overriddenRows). */
  onNetChanged(row: InvoicePreviewRow): void {
    row.totalPayable = (row.netAmount ?? 0) + (row.priorArrearsBrought ?? 0);
    this.rows.set([...this.rows()]);
    // Selection membership hasn't changed, but the array reference must so
    // that any computed reading `selected()` picks up the mutated row values.
    this.selected.set([...this.selected()]);
  }

  /** Download the PDF for an already-generated invoice row. */
  downloadInvoicePdf(row: InvoicePreviewRow): void {
    if (!row?.invoiceId) return;
    this.svc.getInvoicePdf(row.invoiceId).subscribe({
      next: blob => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `TSSS_Invoice_${(row.invoiceNo ?? String(row.invoiceId)).replace(/-/g, '_')}.pdf`;
        a.click();
        URL.revokeObjectURL(url);
      },
      error: () => this.toast.error('Could not download the invoice.')
    });
  }

  startNewBatch(): void {
    this.generatedBatch.set(false);
    this.previewed.set(false);
    this.rows.set([]);
    this.selected.set([]);
  }

  generate(): void {
    if (!this.yearId) return;
    const billable = this.billableRows();
    const included = this.selected().filter(r => !r.note);
    if (included.length === 0) { this.toast.warn('Select at least one student to invoice.'); return; }
    const excludedIds = billable.filter(r => !included.includes(r)).map(r => r.studentId);

    // Build overrides list — every row whose Net differs from the original.
    // Backend rejects the batch if any override lacks a reason.
    const overrides: InvoiceOverrideInput[] = [];
    for (const r of this.overriddenRows()) {
      if (!r.overrideReason || r.overrideReason.trim().length < 2) {
        this.toast.error(`Enter a reason for ${r.studentFullName} — you changed the Net amount.`);
        return;
      }
      overrides.push({ studentId: r.studentId, netAmount: r.netAmount, reason: r.overrideReason.trim() });
    }

    const overrideNote = overrides.length ? ` (${overrides.length} override${overrides.length === 1 ? '' : 's'} logged)` : '';
    this.confirm.confirm({
      header: 'Generate invoices',
      message: `Create invoices for ${included.length} student(s), total Rs. ${this.totalNet().toLocaleString()}${overrideNote}? This cannot be undone in bulk.`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.generating.set(true);
        this.svc.generateInvoices({
          academicYearId: this.yearId!, billingMonth: this.month, billingYear: this.year, classId: this.classId,
          excludedStudentIds: excludedIds, overrides, dryRun: false
        }).subscribe({
          next: res => {
            this.toast.success(`Invoices generated for ${included.length} student(s)`);
            this.generating.set(false);
            // Keep the grid alive so the admin can download each generated PDF
            // from the same screen. The response rows now carry invoiceId /
            // invoiceNo on the students that were actually invoiced.
            const returned = res.data ?? [];
            const byStudent = new Map(returned.map(r => [r.studentId, r]));
            const merged = this.rows().map(existing => {
              const fresh = byStudent.get(existing.studentId);
              if (!fresh) return existing;
              return {
                ...existing,
                invoiceId: fresh.invoiceId ?? null,
                invoiceNo: fresh.invoiceNo ?? null,
                netAmount: fresh.netAmount,
                priorArrearsBrought: fresh.priorArrearsBrought,
                totalPayable: fresh.totalPayable,
                note: fresh.note ?? existing.note
              };
            });
            this.rows.set(merged);
            this.generatedBatch.set(true);
          },
          error: () => this.generating.set(false)
        });
      }
    });
  }
}
