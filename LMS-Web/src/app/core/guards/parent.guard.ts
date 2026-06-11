import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Roles } from '../models/roles';

/**
 * Gate for the /parent subtree. Only the parent role belongs here; admins are
 * allowed through for support/preview. Everyone else is bounced to /admin.
 */
export const parentGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.hasAnyRole([Roles.Parent, Roles.Admin])) return true;
  return router.createUrlTree(['/admin/dashboard']);
};
