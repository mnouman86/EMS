import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

/**
 * Blocks any /admin (or /parent) route when not authenticated; bounces to
 * /login with returnUrl. Also enforces the first-login "you must change your
 * password" rule — a user with `mustChangePassword = true` can only reach the
 * change-password route until they've replaced their admin-set password.
 */
export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  }

  const user = auth.currentUser();
  if (user?.mustChangePassword) {
    const isChangePwd = state.url.includes('/change-password');
    if (!isChangePwd) {
      const target = user.roles.some(r => r.toLowerCase() === 'parent')
        ? '/parent/change-password'
        : '/admin/change-password';
      return router.createUrlTree([target]);
    }
  }

  return true;
};
