import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { ApiService } from './api.service';
import {
  MyPermissions, EffectivePermission, PermissionActionName,
  AppFeature, UserPermissionGridRow, RolePermission, PermissionUser, FeaturePermissionInput, RoleLookup
} from '../models/permission.models';
import { ApiResult } from '../models/api-result';

@Injectable({ providedIn: 'root' })
export class PermissionService {
  private api = inject(ApiService);

  private superAdmin = signal(false);
  private map = signal<Record<string, EffectivePermission>>({});
  private loaded = signal(false);

  readonly isSuperAdmin = this.superAdmin.asReadonly();
  readonly isLoaded = this.loaded.asReadonly();
  /** Bumps whenever permissions change — directives/components can depend on it. */
  readonly version = computed(() => (this.loaded() ? 1 : 0) + Object.keys(this.map()).length + (this.superAdmin() ? 1000 : 0));

  /** Load the current user's effective permissions (call after login + on app init). */
  loadMine(): Observable<boolean> {
    return this.api.post<MyPermissions>('Permission/PermissionGetMine', {}).pipe(
      map(res => res.data ?? { isSuperAdmin: false, permissions: [] }),
      tap(mp => this.apply(mp)),
      map(() => true),
      catchError(() => { this.apply({ isSuperAdmin: false, permissions: [] }); return of(false); })
    );
  }

  private apply(mp: MyPermissions): void {
    this.superAdmin.set(!!mp.isSuperAdmin);
    const rec: Record<string, EffectivePermission> = {};
    (mp.permissions ?? []).forEach(p => { if (p.featureCode) rec[p.featureCode] = p; });
    this.map.set(rec);
    this.loaded.set(true);
  }

  clear(): void {
    this.superAdmin.set(false);
    this.map.set({});
    this.loaded.set(false);
  }

  /** Core check. `spec` is "Feature" (implies Read) or "Feature.Action". */
  has(spec: string): boolean {
    if (this.superAdmin()) return true;
    const [feature, actionRaw] = spec.split('.');
    const action = (actionRaw || 'Read') as PermissionActionName;
    const p = this.map()[feature];
    if (!p) return false;
    switch (action) {
      case 'Read': return p.canRead;
      case 'Create': return p.canCreate;
      case 'Update': return p.canUpdate;
      case 'Delete': return p.canDelete;
      case 'Export': return p.canExport;
      case 'Approve': return p.canApprove;
      case 'Print': return p.canPrint;
      default: return false;
    }
  }

  canRead(feature: string): boolean { return this.has(feature + '.Read'); }

  /* ---------- Admin management API ---------- */
  getFeatures(): Observable<ApiResult<AppFeature[]>> {
    return this.api.post<AppFeature[]>('Permission/PermissionGetFeatures', {});
  }
  getUsers(searchTerm: string | null, roleId: number | null, isActive: boolean | null, pageNumber: number, pageSize: number): Observable<ApiResult<PermissionUser[]>> {
    return this.api.post<PermissionUser[]>('Permission/PermissionGetUsers', { searchTerm, roleId, isActive, pageNumber, pageSize });
  }
  getUserGrid(userId: number): Observable<ApiResult<UserPermissionGridRow[]>> {
    return this.api.post<UserPermissionGridRow[]>('Permission/PermissionGetUserGrid', { userId });
  }
  getRoleTemplate(roleId: number): Observable<ApiResult<RolePermission[]>> {
    return this.api.post<RolePermission[]>('Permission/PermissionGetRoleTemplate', { roleId });
  }
  getRoles(): Observable<ApiResult<RoleLookup[]>> {
    return this.api.post<RoleLookup[]>('Permission/PermissionGetRoles', {});
  }
  saveUserPermissions(userIds: number[], permissions: FeaturePermissionInput[]): Observable<ApiResult<unknown>> {
    return this.api.post('Permission/PermissionSaveUser', { userIds, permissions });
  }
}
