import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { MultiSelectModule } from 'primeng/multiselect';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FeeService } from '../fee.service';
import { Concession } from '../fee.models';
import { StudentService } from '../../students/student.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-fee-concessions',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, MultiSelectModule, InputNumberModule, InputTextModule,
    DatePickerModule, DialogModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './fee-concessions.html'
})
export class FeeConcessions implements OnInit {
  private svc = inject(FeeService);
  private studentSvc = inject(StudentService);
  private toast = inject(ToastService);
  private auth = inject(AuthService);
  private confirm = inject(ConfirmationService);

  typeOptions = ['Percentage', 'Fixed', 'Sibling', 'FullWaiver'].map(v => ({ label: v, value: v }));
  studentOptions = signal<{ label: string; value: number }[]>([]);
  feeTypeOptions = signal<{ label: string; value: number }[]>([]);
  private studentNames = new Map<number, string>();

  filterStudentId: number | null = null;
  rows = signal<Concession[]>([]);
  loading = signal(false);

  dialog = signal(false);
  saving = signal(false);
  form: {
    studentId: number | null; concessionType: string; value: number;
    applicableFeeTypeIds: number[]; effectiveFrom: Date; effectiveTo: Date | null; reason: string;
  } = { studentId: null, concessionType: 'Percentage', value: 0, applicableFeeTypeIds: [], effectiveFrom: new Date(), effectiveTo: null, reason: '' };

  ngOnInit(): void {
    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      const list = res.data ?? [];
      this.studentOptions.set(list.map(s => ({ label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`, value: s.id })));
      list.forEach(s => this.studentNames.set(s.id, s.fullName));
    });
    this.svc.getTypes(defaultSearch()).subscribe(res => {
      this.feeTypeOptions.set((res.data ?? []).map(t => ({ label: `${t.name} (${t.code})`, value: t.id })));
    });
    this.load();
  }

  studentName(id: number): string { return this.studentNames.get(id) ?? `Student #${id}`; }

  load(): void {
    this.loading.set(true);
    this.svc.getConcessions(this.filterStudentId).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  open(): void {
    this.form = { studentId: this.filterStudentId, concessionType: 'Percentage', value: 0, applicableFeeTypeIds: [], effectiveFrom: new Date(), effectiveTo: null, reason: '' };
    this.dialog.set(true);
  }

  save(): void {
    if (!this.form.studentId) { this.toast.warn('Select a student.'); return; }
    if (this.form.concessionType === 'Percentage' && (this.form.value < 0 || this.form.value > 100)) { this.toast.warn('Percentage must be 0–100.'); return; }
    this.saving.set(true);
    this.svc.grantConcession({
      studentId: this.form.studentId,
      concessionType: this.form.concessionType,
      value: this.form.value,
      applicableFeeTypeIds: this.form.applicableFeeTypeIds.length ? this.form.applicableFeeTypeIds : undefined,
      effectiveFrom: this.form.effectiveFrom.toISOString(),
      effectiveTo: this.form.effectiveTo ? this.form.effectiveTo.toISOString() : null,
      reason: this.form.reason || null,
      approvedByUserId: this.auth.currentUser()?.id ?? null
    }).subscribe({
      next: () => { this.toast.success('Concession granted'); this.saving.set(false); this.dialog.set(false); this.load(); },
      error: () => this.saving.set(false)
    });
  }

  revoke(c: Concession): void {
    this.confirm.confirm({
      header: 'Revoke concession',
      message: `Revoke this ${c.concessionType} concession for ${this.studentName(c.studentId)}?`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.revokeConcession({ concessionId: c.id, reason: 'Revoked by staff' })
        .subscribe({ next: () => { this.toast.success('Concession revoked'); this.load(); } })
    });
  }

  statusSeverity(status?: string): 'success' | 'secondary' | 'danger' {
    if (status === 'Active') return 'success';
    if (status === 'Revoked') return 'danger';
    return 'secondary';
  }
}
