import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { TagModule } from 'primeng/tag';
import { MessageModule } from 'primeng/message';
import { PermissionService } from '../../core/services/permission.service';
import { PermissionUser, RoleLookup, AppFeature, UserPermissionGridRow, FeaturePermissionInput } from '../../core/models/permission.models';
import { ToastService } from '../../core/services/toast.service';

type PermKey = 'canRead' | 'canCreate' | 'canUpdate' | 'canDelete' | 'canExport' | 'canApprove' | 'canPrint';

@Component({
  selector: 'app-access-control',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TableModule, ButtonModule, SelectModule, InputTextModule, CheckboxModule, TagModule, MessageModule
  ],
  templateUrl: './access-control.html'
})
export class AccessControl implements OnInit {
  private perms = inject(PermissionService);
  private toast = inject(ToastService);

  readonly permCols: { key: PermKey; label: string }[] = [
    { key: 'canRead', label: 'Read' }, { key: 'canCreate', label: 'Create' }, { key: 'canUpdate', label: 'Update' },
    { key: 'canDelete', label: 'Delete' }, { key: 'canExport', label: 'Export' }, { key: 'canApprove', label: 'Approve' },
    { key: 'canPrint', label: 'Print' }
  ];

  // user grid
  users = signal<PermissionUser[]>([]);
  total = signal(0);
  loading = signal(false);
  page = 1; pageSize = 15;
  search = ''; roleFilter: number | null = null; statusFilter: boolean | null = null;

  roleOptions = signal<{ label: string; value: number | null }[]>([{ label: 'All roles', value: null }]);
  statusOptions = [{ label: 'All', value: null }, { label: 'Active', value: true }, { label: 'Inactive', value: false }];
  copyRoleOptions = signal<{ label: string; value: number }[]>([]);

  selected = signal<PermissionUser[]>([]);
  selectedIds = computed(() => this.selected().map(u => u.userId));

  // matrix
  private parentNames = new Map<number, string>();
  private template: UserPermissionGridRow[] = [];   // blank feature list
  matrix = signal<UserPermissionGridRow[]>([]);
  matrixLoading = signal(false);
  saving = signal(false);
  copyUserId: number | null = null;
  copyRoleId: number | null = null;

  ngOnInit(): void {
    forkJoin({
      roles: this.perms.getRoles(),
      features: this.perms.getFeatures()
    }).subscribe(({ roles, features }) => {
      const rs = (roles.data ?? []) as RoleLookup[];
      this.roleOptions.set([{ label: 'All roles', value: null }, ...rs.map(r => ({ label: r.roleName, value: r.roleId as number | null }))]);
      this.copyRoleOptions.set(rs.map(r => ({ label: r.roleName, value: r.roleId })));

      const fs = (features.data ?? []) as AppFeature[];
      fs.filter(f => !f.route).forEach(p => this.parentNames.set(p.featureId, p.featureName));
      this.template = fs.filter(f => !!f.route).sort((a, b) => a.displayOrder - b.displayOrder).map(f => this.blankRow(f));
      this.matrix.set(this.template.map(r => ({ ...r })));
    });
    // user grid loads via the table's lazy (onLazyLoad) on first render
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

  loadUsers(): void {
    this.loading.set(true);
    this.perms.getUsers(this.search.trim() || null, this.roleFilter, this.statusFilter, this.page, this.pageSize).subscribe({
      next: res => { this.users.set(res.data ?? []); this.total.set(res.totalCount ?? (res.data?.length ?? 0)); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  searchNow(): void { this.page = 1; this.loadUsers(); }
  onPage(e: any): void {
    this.pageSize = e?.rows ?? this.pageSize;
    this.page = Math.floor((e?.first ?? 0) / this.pageSize) + 1;
    this.loadUsers();
  }

  onSelectionChange(sel: PermissionUser[]): void {
    this.selected.set(sel ?? []);
    if (sel?.length === 1) this.loadUserMatrix(sel[0].userId);
    else this.matrix.set(this.template.map(r => ({ ...r })));   // blank for bulk / none
  }

  private loadUserMatrix(userId: number): void {
    this.matrixLoading.set(true);
    this.perms.getUserGrid(userId).subscribe({
      next: res => { this.matrix.set((res.data ?? []).map(r => ({ ...r }))); this.matrixLoading.set(false); },
      error: () => { this.matrix.set(this.template.map(r => ({ ...r }))); this.matrixLoading.set(false); }
    });
  }

  /* ---- matrix helpers ---- */
  isFull(row: UserPermissionGridRow): boolean {
    return this.permCols.every(c => (row as any)[c.key]);
  }
  toggleFull(row: UserPermissionGridRow, value: boolean): void {
    this.permCols.forEach(c => (row as any)[c.key] = value);
    this.matrix.set([...this.matrix()]);
  }
  onCellChange(): void { this.matrix.set([...this.matrix()]); }

  setAll(value: boolean): void {
    this.matrix().forEach(r => this.permCols.forEach(c => (r as any)[c.key] = value));
    this.matrix.set([...this.matrix()]);
  }

  /* ---- copy ---- */
  copyFromRole(roleId: number | null): void {
    if (!roleId) return;
    this.perms.getRoleTemplate(roleId).subscribe({
      next: res => {
        const byCode = new Map((res.data ?? []).map(r => [r.featureCode, r]));
        this.matrix().forEach(row => {
          const src = byCode.get(row.featureCode);
          this.permCols.forEach(c => (row as any)[c.key] = src ? (src as any)[c.key] : false);
        });
        this.matrix.set([...this.matrix()]);
        this.toast.success('Copied permissions from role template');
      }
    });
  }
  copyFromUser(userId: number | null): void {
    if (!userId) return;
    this.perms.getUserGrid(userId).subscribe({
      next: res => {
        const byCode = new Map((res.data ?? []).map(r => [r.featureCode, r]));
        this.matrix().forEach(row => {
          const src = byCode.get(row.featureCode);
          this.permCols.forEach(c => (row as any)[c.key] = src ? (src as any)[c.key] : false);
        });
        this.matrix.set([...this.matrix()]);
        this.toast.success('Copied permissions from user');
      }
    });
  }

  /* ---- save ---- */
  save(): void {
    const ids = this.selectedIds();
    if (ids.length === 0) { this.toast.warn('Select at least one user.'); return; }
    const payload: FeaturePermissionInput[] = this.matrix().map(r => ({
      featureId: r.featureId,
      canRead: r.canRead, canCreate: r.canCreate, canUpdate: r.canUpdate, canDelete: r.canDelete,
      canExport: r.canExport, canApprove: r.canApprove, canPrint: r.canPrint
    }));
    this.saving.set(true);
    this.perms.saveUserPermissions(ids, payload).subscribe({
      next: () => {
        this.toast.success(`Permissions saved for ${ids.length} user(s)`);
        this.saving.set(false);
        // If the admin edited their own row, refresh their live permissions.
        this.perms.loadMine().subscribe();
      },
      error: () => this.saving.set(false)
    });
  }

  copyUserOptions = computed(() => this.users().map(u => ({ label: `${u.fullName || u.userName} (${u.email})`, value: u.userId })));
}
