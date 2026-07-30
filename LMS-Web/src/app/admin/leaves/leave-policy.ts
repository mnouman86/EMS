import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { LeaveService, LeavePolicyRow, LeaveType } from './leave.service';
import { EmployeeService } from '../employees/employee.service';
import { AcademicYearService } from '../academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

interface LookupOpt<T = number> { label: string; value: T; }

@Component({
  selector: 'app-leave-policy',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, ButtonModule, DialogModule, SelectModule,
            InputNumberModule, InputTextModule, MessageModule, TagModule, TooltipModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Leave Policy per Employee</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">
          Optional per-employee override on a leave type's default annual quota — used when a specific
          staff member gets more (or fewer) days than the type default. No row = default applies.
        </p>
      </div>
      <p-button label="Add policy" icon="pi pi-plus" (onClick)="add()"></p-button>
    </div>

    <div style="display:flex;gap:10px;flex-wrap:wrap;align-items:flex-end;margin-bottom:12px;">
      <div style="min-width:200px;">
        <label style="display:block;font-size:0.78rem;color:#475569;margin-bottom:4px;">Academic year</label>
        <p-select [options]="yearOptions()" optionLabel="label" optionValue="value"
                  [(ngModel)]="filter.academicYearId" (onChange)="load()"
                  [style]="{ width: '100%' }" appendTo="body"></p-select>
      </div>
      <div style="min-width:240px;">
        <label style="display:block;font-size:0.78rem;color:#475569;margin-bottom:4px;">Employee</label>
        <p-select [options]="employeeOptions()" optionLabel="label" optionValue="value"
                  [(ngModel)]="filter.employeeId" (onChange)="load()" [filter]="true"
                  [style]="{ width: '100%' }" appendTo="body"></p-select>
      </div>
      <p-button label="Refresh" icon="pi pi-refresh" severity="secondary" [outlined]="true" (onClick)="load()"></p-button>
    </div>

    <p-message severity="info" styleClass="mb-3" [closable]="false"
               text="Overrides apply for the selected academic year. When no override exists, the leave type's default quota is used."></p-message>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines"
             [paginator]="rows().length > 25" [rows]="25">
      <ng-template pTemplate="header">
        <tr>
          <th>Employee</th>
          <th>Leave type</th>
          <th>Academic year</th>
          <th style="width:140px;text-align:right;">Annual quota</th>
          <th>Notes</th>
          <th style="width:100px;text-align:center;">Actions</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td>{{ r.employeeName }}</td>
          <td>{{ r.leaveTypeName }} <span style="color:#94a3b8;">({{ r.leaveTypeCode }})</span></td>
          <td>{{ r.academicYearName || '—' }}</td>
          <td style="text-align:right;">{{ r.annualQuota | number:'1.0-2' }}</td>
          <td>{{ r.notes || '—' }}</td>
          <td style="text-align:center;">
            <p-button icon="pi pi-pencil" [text]="true" size="small" (onClick)="edit(r)" pTooltip="Edit"></p-button>
          </td>
        </tr>
      </ng-template>
      <ng-template pTemplate="emptymessage">
        <tr><td colspan="6" style="text-align:center;padding:18px;color:#94a3b8;">No policy overrides for these filters.</td></tr>
      </ng-template>
    </p-table>

    <p-dialog [(visible)]="dialogOpen" [modal]="true" [style]="{ width: '520px' }" [draggable]="false" [resizable]="false"
              [header]="editing.leavePolicyId ? 'Edit policy override' : 'Add policy override'">
      <div style="display:flex;flex-direction:column;gap:12px;">
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Employee *</label>
          <p-select [options]="employeePickOptions()" optionLabel="label" optionValue="value"
                    [(ngModel)]="editing.employeeId" [filter]="true"
                    [style]="{ width: '100%' }" appendTo="body" placeholder="Select employee"></p-select>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Leave type *</label>
          <p-select [options]="typeOptions()" optionLabel="label" optionValue="value"
                    [(ngModel)]="editing.leaveTypeId" (ngModelChange)="onTypeChange()"
                    [style]="{ width: '100%' }" appendTo="body" placeholder="Select leave type"></p-select>
          @if (selectedType(); as t) {
            <div style="margin-top:6px;font-size:0.78rem;color:#64748b;">
              Default quota for this type: <strong>{{ t.defaultAnnualQuota }} days</strong>. Override below to set a different quota for this employee.
            </div>
          }
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Academic year *</label>
          <p-select [options]="yearPickOptions()" optionLabel="label" optionValue="value"
                    [(ngModel)]="editing.academicYearId"
                    [style]="{ width: '100%' }" appendTo="body" placeholder="Select academic year"></p-select>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Annual quota (days) *</label>
          <p-inputnumber [(ngModel)]="editing.annualQuota" [min]="0" [minFractionDigits]="0" [maxFractionDigits]="2" [fluid]="true"></p-inputnumber>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Notes</label>
          <input pInputText [(ngModel)]="editing.notes" maxlength="400" style="width:100%;" />
        </div>
      </div>
      <ng-template pTemplate="footer">
        <p-button label="Cancel" severity="secondary" [outlined]="true" (onClick)="dialogOpen.set(false)"></p-button>
        <p-button label="Save" icon="pi pi-check" [loading]="saving()" (onClick)="save()"></p-button>
      </ng-template>
    </p-dialog>
  `
})
export class LeavePolicy implements OnInit {
  private svc = inject(LeaveService);
  private empSvc = inject(EmployeeService);
  private yearSvc = inject(AcademicYearService);
  private toast = inject(ToastService);

  rows = signal<LeavePolicyRow[]>([]);
  loading = signal(false);
  saving = signal(false);
  dialogOpen = signal(false);

  types = signal<LeaveType[]>([]);
  years = signal<Array<{ id: number; displayName: string; isOpen?: boolean }>>([]);
  employees = signal<Array<{ id: number; fullName?: string }>>([]);

  yearOptions = computed<LookupOpt<number | null>[]>(() => [
    { label: 'All years', value: null as any },
    ...this.years().map(y => ({ label: y.displayName + (y.isOpen ? ' (open)' : ''), value: y.id }))
  ]);
  yearPickOptions = computed<LookupOpt[]>(() =>
    this.years().map(y => ({ label: y.displayName + (y.isOpen ? ' (open)' : ''), value: y.id })));
  employeeOptions = computed<LookupOpt<number | null>[]>(() => [
    { label: 'All employees', value: null as any },
    ...this.employees().map(e => ({ label: e.fullName || `Emp #${e.id}`, value: e.id }))
  ]);
  employeePickOptions = computed<LookupOpt[]>(() =>
    this.employees().map(e => ({ label: e.fullName || `Emp #${e.id}`, value: e.id })));
  typeOptions = computed<LookupOpt[]>(() =>
    this.types().filter(t => t.isActive).map(t => ({ label: `${t.name} (${t.code})`, value: t.leaveTypeId })));
  selectedType = computed(() => this.types().find(t => t.leaveTypeId === this.editing.leaveTypeId));

  filter: { academicYearId: number | null; employeeId: number | null } = { academicYearId: null, employeeId: null };

  editing: {
    leavePolicyId?: number;
    employeeId?: number;
    leaveTypeId?: number;
    academicYearId?: number;
    annualQuota?: number;
    notes?: string;
  } = {};

  ngOnInit(): void {
    forkJoin({
      types: this.svc.getTypes(true),
      years: this.yearSvc.getAll(defaultSearch()),
      emps: this.empSvc.getAll(defaultSearch())
    }).subscribe(({ types, years, emps }) => {
      this.types.set(types.data ?? []);
      this.years.set((years.data ?? []) as any);
      this.employees.set((emps.data ?? []) as any);
      const open = (years.data ?? []).find((y: any) => y.isOpen);
      if (open) this.filter.academicYearId = open.id;
      this.load();
    });
  }

  load(): void {
    this.loading.set(true);
    this.svc.getPolicies(this.filter.academicYearId, this.filter.employeeId).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load policies.'); this.loading.set(false); }
    });
  }

  add(): void {
    const open = this.years().find(y => y.isOpen);
    this.editing = {
      academicYearId: this.filter.academicYearId ?? open?.id,
      annualQuota: 0
    };
    this.dialogOpen.set(true);
  }
  edit(r: LeavePolicyRow): void {
    this.editing = {
      leavePolicyId: r.leavePolicyId,
      employeeId: r.employeeId,
      leaveTypeId: r.leaveTypeId,
      academicYearId: r.academicYearId,
      annualQuota: r.annualQuota,
      notes: r.notes
    };
    this.dialogOpen.set(true);
  }
  onTypeChange(): void {
    // On create, seed the quota field with the type's default so admin only tweaks it.
    if (!this.editing.leavePolicyId) {
      const t = this.selectedType();
      if (t && (this.editing.annualQuota == null || this.editing.annualQuota === 0)) {
        this.editing.annualQuota = t.defaultAnnualQuota;
      }
    }
  }

  save(): void {
    if (!this.editing.employeeId || !this.editing.leaveTypeId || !this.editing.academicYearId) {
      this.toast.error('Employee, leave type and academic year are required.'); return;
    }
    if (this.editing.annualQuota == null || this.editing.annualQuota < 0) {
      this.toast.error('Annual quota must be >= 0.'); return;
    }
    this.saving.set(true);
    this.svc.upsertPolicy({
      employeeId: this.editing.employeeId!,
      leaveTypeId: this.editing.leaveTypeId!,
      academicYearId: this.editing.academicYearId!,
      annualQuota: this.editing.annualQuota!,
      notes: this.editing.notes
    }).subscribe({
      next: (res: any) => {
        this.saving.set(false);
        if (res.isSuccess) { this.toast.success(res.message || 'Saved.'); this.dialogOpen.set(false); this.load(); }
        else this.toast.error(res.message || 'Save failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Save failed.'); }
    });
  }
}
