import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { MultiSelectModule } from 'primeng/multiselect';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FeeService } from '../fee.service';
import { FeeType, FeeStructureRow } from '../fee.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { AcademicYearService } from '../../admin/academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-fee-setup',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, InputTextModule, InputNumberModule,
    SelectModule, DatePickerModule, ToggleSwitchModule, MultiSelectModule, DialogModule, TagModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './fee-setup.html'
})
export class FeeSetup implements OnInit {
  private svc = inject(FeeService);
  private classSvc = inject(SchoolClassService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  categoryOptions = ['OneTime', 'Monthly', 'Annual', 'Periodic'].map(v => ({ label: v, value: v }));
  monthOptions = [
    { label: 'Jan', value: 1 }, { label: 'Feb', value: 2 }, { label: 'Mar', value: 3 }, { label: 'Apr', value: 4 },
    { label: 'May', value: 5 }, { label: 'Jun', value: 6 }, { label: 'Jul', value: 7 }, { label: 'Aug', value: 8 },
    { label: 'Sep', value: 9 }, { label: 'Oct', value: 10 }, { label: 'Nov', value: 11 }, { label: 'Dec', value: 12 }
  ];

  classOptions = signal<{ label: string; value: number }[]>([]);
  yearOptions = signal<{ label: string; value: number }[]>([]);
  feeTypeOptions = signal<{ label: string; value: number }[]>([]);

  /* ---- Fee types tab ---- */
  types = signal<FeeType[]>([]);
  typesLoading = signal(false);
  typeDialog = signal(false);
  typeSaving = signal(false);
  editType: { id?: number | null; name: string; code: string; category: string; isActive: boolean } =
    { id: null, name: '', code: '', category: 'Monthly', isActive: true };

  /* ---- Structure tab ---- */
  structClassId: number | null = null;
  structYearId: number | null = null;
  structure = signal<FeeStructureRow[]>([]);
  structLoading = signal(false);
  amountDialog = signal(false);
  amountSaving = signal(false);
  amountForm: { feeTypeId: number | null; amount: number; effectiveFrom: Date } =
    { feeTypeId: null, amount: 0, effectiveFrom: new Date() };

  /* ---- Calendar tab ---- */
  calFeeTypeId: number | null = null;
  calYearId: number | null = null;
  calMonths: number[] = [];
  calSaving = signal(false);

  ngOnInit(): void {
    this.loadTypes();
    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      const active = (res.data ?? []).filter(c => c.isActive);
      this.classOptions.set(active.map(c => ({ label: c.levelName, value: c.id })));
    });
    this.yearSvc.getAll(defaultSearch()).subscribe(res => {
      this.yearOptions.set((res.data ?? []).map(y => ({ label: y.displayName, value: y.id })));
    });
  }

  /* ---- Fee types ---- */
  loadTypes(): void {
    this.typesLoading.set(true);
    this.svc.getTypes(defaultSearch()).subscribe({
      next: res => {
        this.types.set(res.data ?? []);
        this.feeTypeOptions.set((res.data ?? []).map(t => ({ label: `${t.name} (${t.code})`, value: t.id })));
        this.typesLoading.set(false);
      },
      error: () => this.typesLoading.set(false)
    });
  }

  openType(t?: FeeType): void {
    this.editType = t
      ? { id: t.id, name: t.name, code: t.code, category: t.category, isActive: t.isActive }
      : { id: null, name: '', code: '', category: 'Monthly', isActive: true };
    this.typeDialog.set(true);
  }

  saveType(): void {
    if (!this.editType.name.trim() || !this.editType.code.trim()) { this.toast.warn('Name and code are required.'); return; }
    this.typeSaving.set(true);
    this.svc.upsertType(this.editType).subscribe({
      next: () => { this.toast.success('Fee type saved'); this.typeSaving.set(false); this.typeDialog.set(false); this.loadTypes(); },
      error: () => this.typeSaving.set(false)
    });
  }

  deleteType(t: FeeType): void {
    this.confirm.confirm({
      header: 'Delete fee type',
      message: `Delete ${t.name}? In-use types are soft-deleted.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.deleteType(t.id).subscribe({ next: () => { this.toast.success('Fee type deleted'); this.loadTypes(); } })
    });
  }

  /* ---- Structure ---- */
  loadStructure(): void {
    if (!this.structClassId || !this.structYearId) { this.toast.warn('Pick a class and academic year.'); return; }
    this.structLoading.set(true);
    this.svc.getStructureForClass(this.structClassId, this.structYearId).subscribe({
      next: res => { this.structure.set(res.data ?? []); this.structLoading.set(false); },
      error: () => this.structLoading.set(false)
    });
  }

  openAmount(row?: FeeStructureRow): void {
    this.amountForm = {
      feeTypeId: row ? row.feeTypeId : null,
      amount: row ? row.amount : 0,
      effectiveFrom: new Date()
    };
    this.amountDialog.set(true);
  }

  saveAmount(): void {
    if (!this.structClassId) { this.toast.warn('Pick a class first.'); return; }
    if (!this.amountForm.feeTypeId) { this.toast.warn('Pick a fee type.'); return; }
    this.amountSaving.set(true);
    this.svc.upsertAmount({
      feeTypeId: this.amountForm.feeTypeId,
      schoolClassId: this.structClassId,
      amount: this.amountForm.amount,
      effectiveFrom: this.amountForm.effectiveFrom.toISOString()
    }).subscribe({
      next: () => { this.toast.success('Amount saved'); this.amountSaving.set(false); this.amountDialog.set(false); this.loadStructure(); },
      error: () => this.amountSaving.set(false)
    });
  }

  /* ---- Calendar ---- */
  saveCalendar(): void {
    if (!this.calFeeTypeId || !this.calYearId) { this.toast.warn('Pick a fee type and academic year.'); return; }
    if (this.calMonths.length === 0) { this.toast.warn('Select at least one billed month.'); return; }
    this.calSaving.set(true);
    this.svc.configureCalendar({ feeTypeId: this.calFeeTypeId, academicYearId: this.calYearId, billedMonths: this.calMonths }).subscribe({
      next: () => { this.toast.success('Billing calendar saved'); this.calSaving.set(false); },
      error: () => this.calSaving.set(false)
    });
  }
}
