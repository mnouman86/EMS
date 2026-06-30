import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { SelectModule } from 'primeng/select';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { PermissionService } from '../../core/services/permission.service';
import { PermissionUser, RoleLookup } from '../../core/models/permission.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    TabsModule, TableModule, ButtonModule, InputTextModule, PasswordModule, SelectModule,
    TagModule, TooltipModule, DialogModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './user-management.html'
})
export class UserManagement implements OnInit {
  private perms = inject(PermissionService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  /* ---------- Users tab ---------- */
  users = signal<PermissionUser[]>([]);
  usersLoading = signal(false);
  searchTerm = '';
  pageSize = 50;

  roles = signal<RoleLookup[]>([]);
  roleOptions = computed(() => this.roles().map(r => ({ label: this.titleCase(r.roleName), value: r.roleId })));

  /* Add-user dialog */
  userDialogOpen = signal(false);
  userSaving = signal(false);
  newUser = { email: '', fullName: '', password: '', roleId: null as number | null };

  /* Reset-password dialog */
  resetOpen = signal(false);
  resetTarget = signal<PermissionUser | null>(null);
  resetPassword = '';
  resetSaving = signal(false);

  /* ---------- Roles tab ---------- */
  roleDialogOpen = signal(false);
  roleSaving = signal(false);
  editingRole: { roleId: number | null; name: string } = { roleId: null, name: '' };

  ngOnInit(): void {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers(): void {
    this.usersLoading.set(true);
    this.perms.getUsers(this.searchTerm?.trim() || null, null, null, 1, this.pageSize).subscribe({
      next: r => { this.users.set(r.data ?? []); this.usersLoading.set(false); },
      error: () => this.usersLoading.set(false)
    });
  }

  loadRoles(): void {
    this.perms.getRoles().subscribe({ next: r => this.roles.set(r.data ?? []) });
  }

  /* ---------- Users tab actions ---------- */

  openCreateUser(): void {
    this.newUser = { email: '', fullName: '', password: '', roleId: this.roleOptions()[0]?.value ?? null };
    this.userDialogOpen.set(true);
  }

  saveNewUser(): void {
    if (!this.newUser.email.trim() || !this.newUser.fullName.trim() || !this.newUser.password || !this.newUser.roleId) {
      this.toast.warn('All fields are required.'); return;
    }
    if (this.newUser.password.length < 6) { this.toast.warn('Password must be at least 6 characters.'); return; }
    this.userSaving.set(true);
    this.perms.createUser(this.newUser.email.trim(), this.newUser.fullName.trim(), this.newUser.password, this.newUser.roleId!).subscribe({
      next: r => {
        this.userSaving.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'User created.');
          this.userDialogOpen.set(false);
          this.loadUsers();
        } else {
          this.toast.error(r.message || 'Could not create user.');
        }
      },
      error: () => { this.userSaving.set(false); this.toast.error('Could not create user.'); }
    });
  }

  openReset(u: PermissionUser): void {
    this.resetTarget.set(u);
    this.resetPassword = '';
    this.resetOpen.set(true);
  }

  submitReset(): void {
    const target = this.resetTarget();
    if (!target) return;
    if (!this.resetPassword || this.resetPassword.length < 6) {
      this.toast.warn('Password must be at least 6 characters.'); return;
    }
    this.resetSaving.set(true);
    this.perms.resetUserPassword(target.userId, this.resetPassword).subscribe({
      next: r => {
        this.resetSaving.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Password reset.');
          this.resetOpen.set(false);
        } else {
          this.toast.error(r.message || 'Could not reset password.');
        }
      },
      error: () => { this.resetSaving.set(false); this.toast.error('Could not reset password.'); }
    });
  }

  toggleActive(u: PermissionUser): void {
    const willActivate = (u.status || '').toLowerCase() !== 'active';
    this.confirm.confirm({
      header: willActivate ? 'Enable user' : 'Disable user',
      message: willActivate
        ? `Re-enable ${u.fullName || u.email}? They will be able to sign in again.`
        : `Disable ${u.fullName || u.email}? They will be signed out and unable to log in until re-enabled.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: willActivate ? '' : 'p-button-danger',
      accept: () => {
        this.perms.setUserActive(u.userId, willActivate).subscribe({
          next: r => {
            if (r.isSuccess) { this.toast.success(r.message || 'Done.'); this.loadUsers(); }
            else this.toast.error(r.message || 'Could not update.');
          },
          error: () => this.toast.error('Could not update.')
        });
      }
    });
  }

  statusSeverity(status?: string): 'success' | 'danger' | 'warn' | 'secondary' {
    switch ((status || '').toLowerCase()) {
      case 'active': return 'success';
      case 'disabled': case 'locked': return 'danger';
      case 'pending': return 'warn';
      default: return 'secondary';
    }
  }

  /* ---------- Roles tab actions ---------- */

  openCreateRole(): void {
    this.editingRole = { roleId: null, name: '' };
    this.roleDialogOpen.set(true);
  }

  openEditRole(r: RoleLookup): void {
    this.editingRole = { roleId: r.roleId, name: r.roleName };
    this.roleDialogOpen.set(true);
  }

  saveRole(): void {
    const name = (this.editingRole.name || '').trim();
    if (!name) { this.toast.warn('Role name is required.'); return; }
    this.roleSaving.set(true);
    const obs = this.editingRole.roleId
      ? this.perms.updateRole(this.editingRole.roleId, name)
      : this.perms.createRole(name);
    obs.subscribe({
      next: r => {
        this.roleSaving.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Saved.');
          this.roleDialogOpen.set(false);
          this.loadRoles();
        } else {
          this.toast.error(r.message || 'Could not save role.');
        }
      },
      error: () => { this.roleSaving.set(false); this.toast.error('Could not save role.'); }
    });
  }

  deleteRole(r: RoleLookup): void {
    if (r.roleName?.toLowerCase() === 'admin') { this.toast.warn('The admin role cannot be deleted.'); return; }
    this.confirm.confirm({
      header: 'Delete role',
      message: `Delete role "${r.roleName}"? Users currently in this role will be unassigned.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.perms.deleteRole(r.roleId).subscribe({
          next: res => {
            if (res.isSuccess) { this.toast.success(res.message || 'Role deleted.'); this.loadRoles(); this.loadUsers(); }
            else this.toast.error(res.message || 'Could not delete role.');
          },
          error: () => this.toast.error('Could not delete role.')
        });
      }
    });
  }

  /** Display the seeded lowercase role names as proper case. */
  titleCase(s?: string | null): string {
    if (!s) return '';
    return s.charAt(0).toUpperCase() + s.slice(1);
  }
}
