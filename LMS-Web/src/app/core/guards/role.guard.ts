import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

/**
 * Role gate. Reads allowed roles from route `data.roles: string[]`.
 * If no roles are declared, allows any authenticated user.
 * Usage:
 *   { path: 'fees', loadComponent: ..., canActivate: [authGuard, roleGuard],
 *     data: { roles: RoleSets.Finance } }
 */
export const roleGuard: CanActivateFn = route => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const allowed = (route.data?.['roles'] as string[] | undefined) ?? [];
  if (allowed.length === 0) return true;
  if (auth.hasAnyRole(allowed)) return true;

  return router.createUrlTree(['/admin/forbidden']);
};
