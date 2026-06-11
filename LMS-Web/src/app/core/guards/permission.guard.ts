import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { PermissionService } from '../services/permission.service';

/**
 * Blocks a route unless the user holds the feature permission declared in
 * route data: `{ feature: 'Fees' }` (Read implied) or `{ feature: 'Fees.Create' }`.
 * Loads permissions first if not yet loaded (deep-link / refresh safe).
 */
export const permissionGuard: CanActivateFn = (route) => {
  const perms = inject(PermissionService);
  const router = inject(Router);
  const spec = route.data?.['feature'] as string | undefined;

  const ensure = perms.isLoaded() ? of(true) : perms.loadMine();
  return ensure.pipe(
    map(() => {
      if (!spec) return true;
      if (perms.has(spec)) return true;
      return router.parseUrl('/admin/forbidden');
    })
  );
};
