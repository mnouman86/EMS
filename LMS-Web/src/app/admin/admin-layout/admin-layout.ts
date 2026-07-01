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
      items: [
        { label: 'Dashboard', icon: 'pi pi-home', route: '/admin/dashboard', feature: '' },
        // Always visible — admins/principals can use it for support/preview;
        // for teachers it's their primary landing page.
        { label: 'My Teaching', icon: 'pi pi-graduation-cap', route: '/admin/my-teaching', feature: '' }
      ]
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
        // Self-service inventory screens — share the 'Inventory' feature so any
        // role granted Inventory.Read can request items / see what's issued to them.
        { label: 'My Inventory', icon: 'pi pi-shopping-bag', route: '/admin/inventory/my-issued', feature: 'Inventory' },
        { label: 'Issue Requests', icon: 'pi pi-send', route: '/admin/inventory/requests', feature: 'Inventory' },
        // Staff check-in/out (empty feature ⇒ always shown to anyone in admin shell;
        // panel + write endpoints hide themselves for admin per spec).
        { label: 'My Attendance', icon: 'pi pi-clock', route: '/admin/my-attendance', feature: '' },
        { label: 'Staff Attendance', icon: 'pi pi-users', route: '/admin/staff-attendance', feature: '' },
        // Permission-matrix gated — visible only if role/user has Attendance: Read
        { label: 'Attendance Summary', icon: 'pi pi-chart-bar', route: '/admin/attendance-summary', feature: 'Attendance' }
      ]
    },
    {
      title: 'Feedback',
      items: [
        { label: 'Log Complaint / Suggestion', icon: 'pi pi-comment', route: '/admin/complaints/new', feature: 'Complaints' },
        { label: 'My Complaints', icon: 'pi pi-inbox', route: '/admin/complaints/mine', feature: 'Complaints' },
        { label: 'Complaints Report', icon: 'pi pi-flag', route: '/admin/complaints/report', feature: 'ComplaintsReport' }
      ]
    },
    {
      title: 'Administration',
      items: [
        { label: 'User Permissions', icon: 'pi pi-lock', route: '/admin/access', feature: 'UserPermissions' },
        { label: 'Users', icon: 'pi pi-users', route: '/admin/users', feature: 'UserManagement' },
        { label: 'Parent Links', icon: 'pi pi-link', route: '/admin/parent-links', feature: 'ParentLinks' },
        { label: 'School Calendar', icon: 'pi pi-calendar', route: '/admin/calendar', feature: 'Calendar' },
        // Nature catalogue feeds the complaint dropdown. Admin-only by route guard.
        { label: 'Complaint Nature', icon: 'pi pi-tag', route: '/admin/complaints/nature', feature: 'ComplaintsReport' }
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
