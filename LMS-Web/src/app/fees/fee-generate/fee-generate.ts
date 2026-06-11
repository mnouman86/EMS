import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FeeService } from '../fee.service';
import { InvoicePreviewRow } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-fee-generate',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, MessageModule, TagModule, ConfirmDialogModule
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
  rows = signal<InvoicePreviewRow[]>([]);
  selected = signal<InvoicePreviewRow[]>([]);

  billableRows = computed(() => this.rows().filter(r => !r.note));
  totalNet = computed(() => this.selected().reduce((sum, r) => sum + r.netAmount, 0));

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
    this.svc.generateInvoices({
      academicYearId: this.yearId, billingMonth: this.month, billingYear: this.year, classId: this.classId, dryRun: true
    }).subscribe({
      next: res => {
        const data = res.data ?? [];
        this.rows.set(data);
        this.selected.set(data.filter(r => !r.note)); // pre-select only billable rows
        this.previewed.set(true);
        this.previewing.set(false);
      },
      error: () => this.previewing.set(false)
    });
  }

  generate(): void {
    if (!this.yearId) return;
    const billable = this.billableRows();
    const included = this.selected().filter(r => !r.note);
    if (included.length === 0) { this.toast.warn('Select at least one student to invoice.'); return; }
    const excludedIds = billable.filter(r => !included.includes(r)).map(r => r.studentId);

    this.confirm.confirm({
      header: 'Generate invoices',
      message: `Create invoices for ${included.length} student(s), total Rs. ${this.totalNet().toLocaleString()}? This cannot be undone in bulk.`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.generating.set(true);
        this.svc.generateInvoices({
          academicYearId: this.yearId!, billingMonth: this.month, billingYear: this.year, classId: this.classId,
          excludedStudentIds: excludedIds, dryRun: false
        }).subscribe({
          next: () => {
            this.toast.success(`Invoices generated for ${included.length} student(s)`);
            this.generating.set(false);
            this.previewed.set(false);
            this.rows.set([]);
            this.selected.set([]);
          },
          error: () => this.generating.set(false)
        });
      }
    });
  }
}
