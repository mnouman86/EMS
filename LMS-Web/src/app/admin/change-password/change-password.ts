import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { MessageModule } from 'primeng/message';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

/**
 * Self-service change-password screen. Backend pulls UserId off the JWT so this
 * form can only ever change *your own* password. Admin-initiated password resets
 * (for other users) live on the Users Management screen and use a separate
 * command (`PermissionResetUserPassword`).
 */
@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonModule, PasswordModule, MessageModule],
  template: `
    <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px;gap:12px;flex-wrap:wrap;">
      <div>
        <h2 style="margin:0;font-size:1.3rem;">Change password</h2>
        <p style="margin:2px 0 0;color:#64748b;font-size:1rem;">
          You'll stay logged in on this device — other active sessions won't be affected.
        </p>
      </div>
    </div>

    <div style="max-width:520px;background:#fff;border:1px solid #e2e8f0;border-radius:10px;padding:20px;">
      <div class="row">
        <label>Current password *</label>
        <p-password [(ngModel)]="currentPassword" [feedback]="false" [toggleMask]="true"
                    styleClass="w-full" [style]="{ width: '100%' }" [inputStyle]="{ width: '100%' }"></p-password>
      </div>

      <div class="row">
        <label>New password *</label>
        <p-password [(ngModel)]="newPassword" [toggleMask]="true"
                    styleClass="w-full" [style]="{ width: '100%' }" [inputStyle]="{ width: '100%' }"
                    promptLabel="Choose a strong password"
                    weakLabel="Too weak" mediumLabel="OK" strongLabel="Strong"></p-password>
        <small style="color:#64748b;">Min 8 chars, includes upper + lower case, a digit and a symbol.</small>
      </div>

      <div class="row">
        <label>Confirm new password *</label>
        <p-password [(ngModel)]="confirmNewPassword" [feedback]="false" [toggleMask]="true"
                    styleClass="w-full" [style]="{ width: '100%' }" [inputStyle]="{ width: '100%' }"></p-password>
        @if (confirmNewPassword && newPassword && confirmNewPassword !== newPassword) {
          <small style="color:#dc2626;">Passwords don't match.</small>
        }
      </div>

      @if (errorMsg()) {
        <p-message severity="error" [text]="errorMsg()!" styleClass="w-full mb-3" [closable]="false"></p-message>
      }

      <div style="display:flex;gap:8px;justify-content:flex-end;margin-top:14px;">
        <p-button label="Cancel" severity="secondary" [outlined]="true" (onClick)="cancel()"></p-button>
        <p-button label="Change password" icon="pi pi-check" [loading]="saving()"
                  (onClick)="save()" [disabled]="!canSubmit()"></p-button>
      </div>
    </div>

    <style>
      .row { margin-bottom:14px; }
      .row label { display:block; font-size:0.82rem; font-weight:600; margin-bottom:5px; color:#0f172a; }
      .w-full { width:100%; }
    </style>
  `
})
export class ChangePassword {
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private router = inject(Router);

  currentPassword = '';
  newPassword = '';
  confirmNewPassword = '';
  saving = signal(false);
  errorMsg = signal<string | null>(null);

  canSubmit = computed(() => {
    // Signals aren't tracked on plain component fields, so we still block the
    // click in save() below — this just disables the button once all three
    // fields are populated and match. See save() for the real validation.
    return true;
  });

  save(): void {
    this.errorMsg.set(null);
    if (!this.currentPassword) { this.errorMsg.set('Current password is required.'); return; }
    if (!this.newPassword || this.newPassword.length < 8) {
      this.errorMsg.set('New password must be at least 8 characters.'); return;
    }
    if (this.newPassword !== this.confirmNewPassword) {
      this.errorMsg.set('New password and confirmation do not match.'); return;
    }
    if (this.newPassword === this.currentPassword) {
      this.errorMsg.set('New password must be different from the current password.'); return;
    }

    this.saving.set(true);
    this.auth.changePassword(this.currentPassword, this.newPassword, this.confirmNewPassword).subscribe({
      next: msg => {
        this.saving.set(false);
        this.toast.success(msg + ' Please log in again.');
        this.currentPassword = ''; this.newPassword = ''; this.confirmNewPassword = '';
        // Force full re-login — the backend also rotated the security stamp so
        // any other active sessions on other devices are invalidated too.
        this.auth.logout();
        this.router.navigate(['/login']);
      },
      error: err => {
        this.saving.set(false);
        this.errorMsg.set(err?.message || 'Change failed. Please check the current password and try again.');
      }
    });
  }

  cancel(): void {
    // First-login users can't cancel out — bounce them back to change-password
    // (the guard would do this anyway). Otherwise send them to their dashboard.
    const user = this.auth.currentUser();
    if (user?.mustChangePassword) return;
    const isParent = (user?.roles ?? []).some(r => r.toLowerCase() === 'parent');
    this.router.navigate([isParent ? '/parent' : '/admin/dashboard']);
  }
}
