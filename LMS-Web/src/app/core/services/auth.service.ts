import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ApiService } from './api.service';
import { TokenStorageService } from './token-storage.service';
import { CurrentUser } from '../models/auth.models';

/** Raw token payload returned by /AdminManager/Login (AccessToken model).
 *  The access_token is an encrypted JWE we can't read, so identity & roles
 *  are carried alongside it as plain fields. */
interface AccessTokenDto {
  access_token: string;
  refresh_token: string;
  token_type: string;
  expires_in: number;
  userID: number;
  roleID: number;
  userName?: string;
  email?: string;
  roles?: string[];
  mustChangePassword?: boolean;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = inject(ApiService);
  private storage = inject(TokenStorageService);

  /** Reactive current-user state. Components read currentUser()/isAuthenticated(). */
  private readonly _user = signal<CurrentUser | null>(this.storage.getUser<CurrentUser>());
  readonly currentUser = this._user.asReadonly();
  readonly isAuthenticated = computed(() => !!this._user() && !!this.storage.accessToken);

  /** Login against AdminManager/Login. RememberMe defaults true. */
  login(userName: string, password: string, rememberMe = true): Observable<CurrentUser> {
    return this.api
      .post<AccessTokenDto>('AdminManager/Login', { userName, password, rememberMe })
      .pipe(
        map(res => {
          if (!res.isSuccess || !res.data?.access_token) {
            throw new Error(res.message || 'Login failed');
          }
          return res.data;
        }),
        tap(token => this.persistSession(token)),
        map(() => this._user()!)
      );
  }

  logout(): void {
    this.storage.clear();
    this._user.set(null);
  }

  /** Request a password-reset email. Resolves to the API success flag. */
  forgotPassword(email: string): Observable<string> {
    return this.api
      .post<boolean>('AdminManager/ForgotPassword', { email })
      .pipe(
        map(res => {
          if (!res.isSuccess) throw new Error(res.message || 'Request failed');
          return res.message || 'If the email exists, a reset link has been sent.';
        })
      );
  }

  /** Complete a password reset using the emailed token. */
  resetPassword(email: string, token: string, newPassword: string, confirmNewPassword: string): Observable<string> {
    return this.api
      .post<boolean>('AdminManager/ResetPassword', { email, token, newPassword, confirmNewPassword })
      .pipe(
        map(res => {
          if (!res.isSuccess) throw new Error(res.message || 'Reset failed');
          return res.message || 'Password reset successfully.';
        })
      );
  }

  /**
   * Self-service password change for the currently logged-in user. The backend
   * pulls UserId off the JWT, so the caller can only change *their own* password.
   * Server-side FluentValidation enforces "current required" + strong-password rule.
   */
  changePassword(currentPassword: string, newPassword: string, confirmNewPassword: string): Observable<string> {
    return this.api
      .post<boolean>('AdminManager/ChangePassword', { currentPassword, newPassword, confirmNewPassword })
      .pipe(
        map(res => {
          if (!res.isSuccess) throw new Error(res.message || 'Change failed');
          return res.message || 'Password changed successfully.';
        })
      );
  }

  hasAnyRole(roles: string[]): boolean {
    const user = this._user();
    if (!user) return false;
    return user.roles.some(r => roles.map(x => x.toLowerCase()).includes(r.toLowerCase()));
  }

  /** Default landing route after login: parents go to the portal, staff to admin.
   *  First-login users are diverted to the change-password screen (parent or
   *  admin shell depending on role) so they can't touch anything else first. */
  landingRoute(): string {
    const roles = (this._user()?.roles ?? []).map(r => r.toLowerCase());
    const isParent = roles.includes('parent');
    const isAdminOrPrincipal = roles.some(r => ['admin', 'principal'].includes(r));
    const isTeacher = roles.includes('teacher');
    const isStaff = roles.some(r => ['admin', 'principal', 'accountant', 'teacher'].includes(r));

    if (this._user()?.mustChangePassword) {
      return (isParent && !isStaff) ? '/parent/change-password' : '/admin/change-password';
    }
    if (isParent && !isStaff) return '/parent';
    // Pure teacher (not also admin/principal) → land on the teacher dashboard.
    if (isTeacher && !isAdminOrPrincipal) return '/admin/my-teaching';
    return '/admin/dashboard';
  }

  // ---- internals ----

  private persistSession(token: AccessTokenDto): void {
    this.storage.setTokens(token.access_token, token.refresh_token);
    // The access token is an encrypted JWE; prefer the plain fields the API
    // returns alongside it, and fall back to a (signed-token) decode only if absent.
    const claims = this.decodeJwt(token.access_token);
    const user: CurrentUser = {
      id: token.userID || Number(claims?.['nameid'] ?? claims?.['sub'] ?? 0),
      userName: token.userName ?? claims?.['unique_name'] ?? claims?.['name'] ?? claims?.['email'] ?? '',
      email: token.email ?? claims?.['email'],
      roles: token.roles?.length ? token.roles : this.extractRoles(claims),
      mustChangePassword: !!token.mustChangePassword
    };
    this.storage.setUser(user);
    this._user.set(user);
  }

  private extractRoles(claims: Record<string, any> | null): string[] {
    if (!claims) return [];
    const roleKeys = [
      'role',
      'roles',
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ];
    for (const key of roleKeys) {
      const v = claims[key];
      if (Array.isArray(v)) return v.map(String);
      if (typeof v === 'string') return [v];
    }
    return [];
  }

  /** Browser-side JWT payload decode (no signature check — server validates). */
  private decodeJwt(token: string): Record<string, any> | null {
    try {
      const payload = token.split('.')[1];
      const json = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
      return JSON.parse(decodeURIComponent(escape(json)));
    } catch {
      return null;
    }
  }
}
