import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { CheckboxModule } from 'primeng/checkbox';
import { TagModule } from 'primeng/tag';
import { MessageModule } from 'primeng/message';
import { PermissionService } from '../../core/services/permission.service';
import { AppFeature, FeaturePermissionInput, RoleLookup, UserPermissionGridRow } from '../../core/models/permission.models';
import { ToastService } from '../../core/services/toast.service';

type PermKey = 'canRead' | 'canCreate' | 'canUpdate' | 'canDelete' | 'canExport' | 'canApprove' | 'canPrint';

/**
 * Role Permission Matrix — assigns the permission template that new users
 * inherit on creation. Existing users of a role are NOT retroactively updated;
 * per-user tweaks live on the User Permissions screen.
 */
@Component({
  selector: 'app-role-permissions',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, SelectModule, CheckboxModule, TagModule, MessageModule
  ],
  templateUrl: './role-permissions.html'
})
export class RolePermissions implements OnInit {
  private perms = inject(PermissionService);
  private toast = inject(ToastService);

  readonly permCols: { key: PermKey; label: string }[] = [
    { key: 'canRead', label: 'Read' }, { key: 'canCreate', label: 'Create' }, { key: 'canUpdate', label: 'Update' },
    { key: 'canDelete', label: 'Delete' }, { key: 'canExport', label: 'Export' }, { key: 'canApprove', label: 'Approve' },
    { key: 'canPrint', label: 'Print' }
  ];

  roleOptions = signal<{ label: string; value: number }[]>([]);
  selectedRoleId = signal<number | null>(null);

  private parentNames = new Map<number, string>();
  private template: UserPermissionGridRow[] = [];
  matrix = signal<UserPermissionGridRow[]>([]);

  loading = signal(false);
  saving = signal(false);

  isAdminRoleSelected = computed(() => {
    const id = this.selectedRoleId();
    if (id == null) return false;
    const label = this.roleOptions().find(r => r.value === id)?.label?.toLowerCase();
    return label === 'admin';
  });

  ngOnInit(): void {
    forkJoin({ roles: this.perms.getRoles(), features: this.perms.getFeatures() }).subscribe(({ roles, features }) => {
      const rs = (roles.data ?? []) as RoleLookup[];
      // Filter out 'admin' (super-admin bypass) and 'parent' (public flow only).
      this.roleOptions.set(rs
        .filter(r => {
          const n = r.roleName.toLowerCase();
          return n !== 'parent';
        })
        .map(r => ({ label: this.titleCase(r.roleName), value: r.roleId })));

      const fs = (features.data ?? []) as AppFeature[];
      fs.filter(f => !f.route).forEach(p => this.parentNames.set(p.featureId, p.featureName));
      this.template = fs.filter(f => !!f.route).sort((a, b) => a.displayOrder - b.displayOrder).map(f => this.blankRow(f));
      this.matrix.set(this.template.map(r => ({ ...r })));
    });
  }

  onRoleChange(): void {
    const id = this.selectedRoleId();
    if (id == null) { this.matrix.set(this.template.map(r => ({ ...r }))); return; }
    this.loading.set(true);
    this.perms.getRoleTemplate(id).subscribe({
      next: res => {
        const byCode = new Map((res.data ?? []).map(r => [r.featureCode, r]));
        const merged = this.template.map(row => {
          const src = byCode.get(row.featureCode);
          const next = { ...row } as any;
          this.permCols.forEach(c => next[c.key] = src ? (src as any)[c.key] : false);
          return next as UserPermissionGridRow;
        });
        this.matrix.set(merged);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  private blankRow(f: AppFeature): UserPermissionGridRow {
    return {
      featureId: f.featureId, featureName: f.featureName, featureCode: f.featureCode,
      parentFeatureId: f.parentFeatureId ?? null, route: f.route, displayOrder: f.displayOrder,
      canRead: false, canCreate: false, canUpdate: false, canDelete: false, canExport: false, canApprove: false, canPrint: false
    };
  }

  moduleName(row: UserPermissionGridRow): string {
    return row.parentFeatureId ? (this.parentNames.get(row.parentFeatureId) ?? '') : '';
  }

  isFull(row: UserPermissionGridRow): boolean { return this.permCols.every(c => (row as any)[c.key]); }
  toggleFull(row: UserPermissionGridRow, value: boolean): void {
    this.permCols.forEach(c => (row as any)[c.key] = value);
    this.matrix.set([...this.matrix()]);
  }
  onCellChange(): void { this.matrix.set([...this.matrix()]); }
  setAll(value: boolean): void {
    this.matrix().forEach(r => this.permCols.forEach(c => (r as any)[c.key] = value));
    this.matrix.set([...this.matrix()]);
  }

  save(): void {
    const roleId = this.selectedRoleId();
    if (roleId == null) { this.toast.warn('Pick a role first.'); return; }
    const payload: FeaturePermissionInput[] = this.matrix().map(r => ({
      featureId: r.featureId,
      canRead: r.canRead, canCreate: r.canCreate, canUpdate: r.canUpdate, canDelete: r.canDelete,
      canExport: r.canExport, canApprove: r.canApprove, canPrint: r.canPrint
    }));
    this.saving.set(true);
    this.perms.saveRolePermissions(roleId, payload).subscribe({
      next: res => {
        if (res.isSuccess) this.toast.success(res.message || 'Role permissions saved.');
        else this.toast.error(res.message || 'Save failed.');
        this.saving.set(false);
      },
      error: () => { this.toast.error('Save failed.'); this.saving.set(false); }
    });
  }

  private titleCase(s: string): string { return s ? s.charAt(0).toUpperCase() + s.slice(1) : s; }
}
