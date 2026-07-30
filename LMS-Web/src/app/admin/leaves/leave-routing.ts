import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { TooltipModule } from 'primeng/tooltip';
import { LeaveService, LeaveRouteRow } from './leave.service';
import { PermissionService } from '../../core/services/permission.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-leave-routing',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, ButtonModule, DialogModule, SelectModule,
            InputTextModule, MessageModule, TooltipModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Leave Approval Routing</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">
          For each applicant role, choose the user who approves their leaves.
          e.g. Teacher &rarr; Principal, Principal &rarr; Director Academics.
        </p>
      </div>
      <p-button label="Add / update route" icon="pi pi-plus" (onClick)="add()"></p-button>
    </div>

    <p-message severity="info" styleClass="mb-3" [closable]="false"
               text="Routes are snapshotted onto each leave application at submit time — mid-route changes don't reassign already-pending items."></p-message>

    <p-table [value]="rows()" [loading]="loading()" styleClass="p-datatable-sm p-datatable-gridlines">
      <ng-template pTemplate="header">
        <tr>
          <th>Applicant role</th>
          <th>Approver</th>
          <th>Notes</th>
          <th style="width:110px;text-align:center;">Actions</th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-r>
        <tr>
          <td>{{ titleCase(r.applicantRoleName) }}</td>
          <td>{{ r.approverName || r.approverUserName }}</td>
          <td>{{ r.notes || '—' }}</td>
          <td style="text-align:center;">
            <p-button icon="pi pi-pencil" [text]="true" size="small" (onClick)="edit(r)" pTooltip="Edit"></p-button>
          </td>
        </tr>
      </ng-template>
      <ng-template pTemplate="emptymessage">
        <tr><td colspan="4" style="text-align:center;padding:18px;color:#94a3b8;">No routes configured yet. Add one so submitted leaves have an approver.</td></tr>
      </ng-template>
    </p-table>

    <p-dialog [(visible)]="dialogOpen" [modal]="true" [style]="{ width: '480px' }" [draggable]="false" [resizable]="false"
              header="Leave route">
      <div style="display:flex;flex-direction:column;gap:12px;">
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Applicant role *</label>
          <p-select [options]="roleOptions()" optionLabel="label" optionValue="value" [(ngModel)]="editing.applicantRoleId"
                    [style]="{ width: '100%' }" appendTo="body" placeholder="Select role"></p-select>
        </div>
        <div>
          <label style="display:block;font-weight:600;font-size:0.82rem;margin-bottom:4px;">Approver *</label>
          <p-select [options]="userOptions()" optionLabel="label" optionValue="value" [(ngModel)]="editing.approverUserId"
                    [filter]="true" [style]="{ width: '100%' }" appendTo="body" placeholder="Select user"></p-select>
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
export class LeaveRouting implements OnInit {
  private svc = inject(LeaveService);
  private perms = inject(PermissionService);
  private toast = inject(ToastService);

  rows = signal<LeaveRouteRow[]>([]);
  loading = signal(false);
  saving = signal(false);
  dialogOpen = signal(false);
  editing: { applicantRoleId?: number; approverUserId?: number; notes?: string } = {};

  roleOptions = signal<Array<{ label: string; value: number }>>([]);
  userOptions = signal<Array<{ label: string; value: number }>>([]);

  ngOnInit(): void {
    this.load();
    forkJoin({ roles: this.perms.getRoles(), users: this.perms.getUsers(null, null, true, 1, 500) }).subscribe(({ roles, users }) => {
      this.roleOptions.set((roles.data ?? []).map(r => ({ label: this.titleCase(r.roleName), value: r.roleId })));
      this.userOptions.set((users.data ?? []).map(u => ({ label: `${u.fullName || u.userName} (${u.email})`, value: u.userId })));
    });
  }
  load(): void {
    this.loading.set(true);
    this.svc.getRoutes().subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load routes.'); this.loading.set(false); }
    });
  }
  add(): void { this.editing = {}; this.dialogOpen.set(true); }
  edit(r: LeaveRouteRow): void {
    this.editing = { applicantRoleId: r.applicantRoleId, approverUserId: r.approverUserId, notes: r.notes };
    this.dialogOpen.set(true);
  }
  save(): void {
    if (!this.editing.applicantRoleId || !this.editing.approverUserId) {
      this.toast.error('Both applicant role and approver are required.'); return;
    }
    this.saving.set(true);
    this.svc.upsertRoute({
      applicantRoleId: this.editing.applicantRoleId,
      approverUserId: this.editing.approverUserId,
      notes: this.editing.notes
    }).subscribe({
      next: (r: any) => {
        this.saving.set(false);
        if (r.isSuccess) { this.toast.success(r.message || 'Saved.'); this.dialogOpen.set(false); this.load(); }
        else this.toast.error(r.message || 'Save failed.');
      },
      error: () => { this.saving.set(false); this.toast.error('Save failed.'); }
    });
  }
  titleCase(s?: string | null): string { return s ? s.charAt(0).toUpperCase() + s.slice(1) : ''; }
}
