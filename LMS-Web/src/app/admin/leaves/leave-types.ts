import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { LeaveService, LeaveType } from './leave.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-types',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, ButtonModule, DialogModule, InputTextModule,
            InputNumberModule, ToggleSwitchModule, TagModule, TooltipModule, ConfirmDialogModule],
  providers: [ConfirmationService],
  template: `
    <p-confirmDialog></p-confirmDialog>
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Leave Types</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">Catalogue of leave types with their default annual quota (days).</p>
      </div>
      <p-button label="Add type" icon="pi pi-plus" (onClick)="add()"></p-button>
    </div>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines">
      <ng-template pTemplate="header">
        <tr>
          <th style="width:120px;">Code</th>
          <th>Name</th>
          <th style="width:130px;text-align:right;">Default Quota</th>
          <th style="width:80px;text-align:center;">Paid</th>
          <th style="width:110px;text-align:center;">Attachment</th>
          <th style="width:110px;text-align:center;">Active</th>
          <th style="width:120px;text-align:center;">Actions</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td><strong>{{ r.code }}</strong></td>
          <td>{{ r.name }}</td>
          <td style="text-align:right;">{{ r.defaultAnnualQuota | number:'1.0-2' }}</td>
          <td style="text-align:center;">@if (r.isPaid) { <i class="pi pi-check" style="color:#16a34a;"></i> } @else { <i class="pi pi-times" style="color:#94a3b8;"></i> }</td>
          <td style="text-align:center;">@if (r.requiresAttachment) { <p-tag value="Required" severity="warn"></p-tag> } @else { — }</td>
          <td style="text-align:center;">@if (r.isActive) { <p-tag value="Active" severity="success"></p-tag> } @else { <p-tag value="Inactive" severity="secondary"></p-tag> }</td>
          <td style="text-align:center;">
            <p-button icon="pi pi-pencil" [text]="true" size="small" (onClick)="edit(r)" pTooltip="Edit"></p-button>
            <p-button icon="pi pi-trash" [text]="true" size="small" severity="danger" (onClick)="del(r)" pTooltip="Delete / deactivate"></p-button>
          </td>
        </tr>
      </ng-template>
    </p-table>

    <p-dialog [(visible)]="dialogOpen" [modal]="true" [style]="{ width: '480px' }" [draggable]="false" [resizable]="false"
              [header]="editing.leaveTypeId ? 'Edit leave type' : 'Add leave type'">
      <div style="display:flex;flex-direction:column;gap:12px;">
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Code *</label>
          <input pInputText [(ngModel)]="editing.code" maxlength="20" style="width:100%;" placeholder="CASUAL, SICK, EARNED..."/>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Name *</label>
          <input pInputText [(ngModel)]="editing.name" maxlength="80" style="width:100%;" />
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Default annual quota (days)</label>
          <p-inputnumber [(ngModel)]="editing.defaultAnnualQuota" [min]="0" [minFractionDigits]="0" [maxFractionDigits]="2" [fluid]="true"></p-inputnumber>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Display order</label>
          <p-inputnumber [(ngModel)]="editing.displayOrder" [min]="1" [max]="999" [fluid]="true"></p-inputnumber>
        </div>
        <div style="display:flex;gap:16px;">
          <div style="display:flex;align-items:center;gap:8px;"><p-toggleswitch [(ngModel)]="editing.isPaid"></p-toggleswitch><span>Paid</span></div>
          <div style="display:flex;align-items:center;gap:8px;"><p-toggleswitch [(ngModel)]="editing.requiresAttachment"></p-toggleswitch><span>Requires attachment</span></div>
          <div style="display:flex;align-items:center;gap:8px;"><p-toggleswitch [(ngModel)]="editing.isActive"></p-toggleswitch><span>Active</span></div>
        </div>
      </div>
      <ng-template pTemplate="footer">
        <p-button label="Cancel" severity="secondary" [outlined]="true" (onClick)="dialogOpen.set(false)"></p-button>
        <p-button label="Save" icon="pi pi-check" [loading]="saving()" (onClick)="save()"></p-button>
      </ng-template>
    </p-dialog>
  `
})
export class LeaveTypes implements OnInit {
  private svc = inject(LeaveService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  rows = signal<LeaveType[]>([]);
  loading = signal(false);
  dialogOpen = signal(false);
  saving = signal(false);
  editing: Partial<LeaveType> = {};

  ngOnInit(): void { this.load(); }
  load(): void {
    this.loading.set(true);
    this.svc.getTypes(false).subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load leave types.'); this.loading.set(false); }
    });
  }
  add(): void {
    this.editing = { code: '', name: '', defaultAnnualQuota: 0, isPaid: true, requiresAttachment: false, isActive: true, displayOrder: 99 };
    this.dialogOpen.set(true);
  }
  edit(r: LeaveType): void { this.editing = { ...r }; this.dialogOpen.set(true); }
  save(): void {
    if (!this.editing.code?.trim() || !this.editing.name?.trim()) { this.toast.error('Code and Name are required.'); return; }
    this.saving.set(true);
    this.svc.upsertType(this.editing).subscribe({
      next: (r: any) => {
        this.saving.set(false);
        if (r.isSuccess) { this.toast.success(r.message || 'Saved.'); this.dialogOpen.set(false); this.load(); }
        else this.toast.error(r.message || 'Save failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Save failed.'); }
    });
  }
  del(r: LeaveType): void {
    this.confirm.confirm({
      message: `Delete "${r.name}"? If historical applications exist it will be deactivated instead.`,
      header: 'Delete leave type', icon: 'pi pi-exclamation-triangle',
      accept: () => this.svc.deleteType(r.leaveTypeId).subscribe({
        next: (res: any) => { res.isSuccess ? this.toast.success(res.message) : this.toast.error(res.message); this.load(); },
        error: () => this.toast.error('Delete failed.')
      })
    });
  }
}
