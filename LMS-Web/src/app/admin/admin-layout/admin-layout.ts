import { Component, computed, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet, Router } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { AuthService } from '../../core/services/auth.service';
import { PermissionService } from '../../core/services/permission.service';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  feature: string;       // FeatureCode used for permission gating ('' = always visible)
}

interface NavGroup {
  title: string;
  items: NavItem[];
}

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, ToastModule],
  templateUrl: './admin-layout.html'
})
export class AdminLayout implements OnInit {
  private auth = inject(AuthService);
  private perms = inject(PermissionService);
  private router = inject(Router);

  collapsed = signal(false);
  user = this.auth.currentUser;
  primaryRole = computed(() => this.user()?.roles?.[0] ?? '');

  /** Full nav definition; filtered per permission in visibleGroups(). */
  private readonly groups: NavGroup[] = [
    {
      title: 'Overview',
      items: [{ label: 'Dashboard', icon: 'pi pi-home', route: '/admin/dashboard', feature: '' }]
    },
    {
      title: 'Academics',
      items: [
        { label: 'Academic Years', icon: 'pi pi-calendar', route: '/admin/academic-years', feature: 'AcademicYears' },
        { label: 'Classes', icon: 'pi pi-building', route: '/admin/classes', feature: 'Classes' },
        { label: 'Subjects', icon: 'pi pi-book', route: '/admin/subjects', feature: 'Subjects' },
        { label: 'Employees', icon: 'pi pi-id-card', route: '/admin/employees', feature: 'Employees' },
        { label: 'Students', icon: 'pi pi-users', route: '/admin/students', feature: 'Students' },
        { label: 'Attendance', icon: 'pi pi-calendar-times', route: '/admin/attendance', feature: 'Attendance' },
        { label: 'Results', icon: 'pi pi-chart-line', route: '/admin/results', feature: 'Results' }
      ]
    },
    {
      title: 'Finance',
      items: [
        { label: 'Fees', icon: 'pi pi-wallet', route: '/admin/fees', feature: 'Fees' },
        { label: 'Inventory', icon: 'pi pi-box', route: '/admin/inventory', feature: 'Inventory' },
        { label: 'Expenses', icon: 'pi pi-credit-card', route: '/admin/expenses', feature: 'Expenses' },
        { label: 'Finance', icon: 'pi pi-chart-pie', route: '/admin/finance', feature: 'Finance' }
      ]
    },
    {
      title: 'My Workspace',
      items: [
        // Empty `feature` → always visible to anyone who reaches the admin shell
        // (route guards enforce the actual role gate: AnyStaff for both).
        { label: 'My Inventory', icon: 'pi pi-shopping-bag', route: '/admin/inventory/my-issued', feature: '' },
        { label: 'Issue Requests', icon: 'pi pi-send', route: '/admin/inventory/requests', feature: '' }
      ]
    },
    {
      title: 'Administration',
      items: [
        { label: 'User Permissions', icon: 'pi pi-lock', route: '/admin/access', feature: 'UserPermissions' },
        { label: 'Parent Links', icon: 'pi pi-link', route: '/admin/parent-links', feature: 'ParentLinks' }
      ]
    }
  ];

  // Recomputes when the permission set version changes.
  visibleGroups = computed<NavGroup[]>(() => {
    this.perms.version();
    return this.groups
      .map(g => ({ title: g.title, items: g.items.filter(i => !i.feature || this.perms.canRead(i.feature)) }))
      .filter(g => g.items.length > 0);
  });

  ngOnInit(): void {
    if (!this.perms.isLoaded()) this.perms.loadMine().subscribe();
  }

  toggle(): void {
    this.collapsed.update(v => !v);
  }

  logout(): void {
    this.perms.clear();
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
