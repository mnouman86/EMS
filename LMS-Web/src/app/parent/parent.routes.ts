import { Routes } from '@angular/router';
import { authGuard } from '../core/guards/auth.guard';
import { parentGuard } from '../core/guards/parent.guard';
import { ParentLayout } from './parent-layout/parent-layout';

/** Parent portal subtree — auth-guarded and restricted to the parent role. */
export const PARENT_ROUTES: Routes = [
  {
    path: '',
    component: ParentLayout,
    canActivate: [authGuard, parentGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
      { path: 'dashboard', loadComponent: () => import('./parent-dashboard/parent-dashboard').then(m => m.ParentDashboard) },
      { path: 'profile', loadComponent: () => import('./parent-profile/parent-profile').then(m => m.ParentProfile) },
      { path: 'fees', loadComponent: () => import('./parent-fees/parent-fees').then(m => m.ParentFees) },
      { path: 'attendance', loadComponent: () => import('./parent-attendance/parent-attendance').then(m => m.ParentAttendance) },
      { path: 'results', loadComponent: () => import('./parent-results/parent-results').then(m => m.ParentResults) },
      // Self-service password change — reuses the same component the admin
      // shell uses. Auth is covered by the parent authGuard on this layout.
      { path: 'change-password', loadComponent: () => import('../admin/change-password/change-password').then(m => m.ChangePassword) }
    ]
  }
];
