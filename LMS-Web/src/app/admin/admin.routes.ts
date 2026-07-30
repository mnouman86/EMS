import { Routes } from '@angular/router';
import { authGuard } from '../core/guards/auth.guard';
import { roleGuard } from '../core/guards/role.guard';
import { permissionGuard } from '../core/guards/permission.guard';
import { RoleSets } from '../core/models/roles';
import { AdminLayout } from './admin-layout/admin-layout';

/**
 * Admin subtree. Lazy-loaded standalone components keep the public marketing
 * bundle untouched. Module screens get appended here phase by phase.
 */
export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminLayout,
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/dashboard').then(m => m.AdminDashboard)
      },
      {
        // Teacher landing page — classes I head, subjects I teach, class-subject matrix.
        // Any authenticated user can open it; admins/principals just see their own
        // (likely empty) sections because they aren't linked to dbo.Employee.
        path: 'my-teaching',
        loadComponent: () => import('./my-teaching/my-teaching').then(m => m.MyTeaching)
      },
      {
        path: 'forbidden',
        loadComponent: () => import('./forbidden/forbidden').then(m => m.Forbidden)
      },
      // Self-service password change — parent authGuard on the layout covers auth;
      // backend derives UserId from the JWT so callers can only change their own.
      {
        path: 'change-password',
        loadComponent: () => import('./change-password/change-password').then(m => m.ChangePassword)
      },
      // Route guards now use `permissionGuard` keyed on AppFeature codes — any role
      // granted the feature's Read permission can open the screen.  Sub-screens share
      // the parent module's feature; the API still gates writes per-action via
      // PermissionMap (e.g. Fees.Update on bulk-collect endpoints).
      {
        path: 'academic-years',
        canActivate: [permissionGuard],
        data: { feature: 'AcademicYears' },
        loadComponent: () => import('./academic-years/academic-years').then(m => m.AcademicYears)
      },
      {
        path: 'classes',
        canActivate: [permissionGuard],
        data: { feature: 'Classes' },
        loadComponent: () => import('./classes/classes').then(m => m.Classes)
      },
      {
        path: 'subjects',
        canActivate: [permissionGuard],
        data: { feature: 'Subjects' },
        loadComponent: () => import('./subjects/subjects').then(m => m.Subjects)
      },
      {
        path: 'subjects/mapping',
        canActivate: [permissionGuard],
        data: { feature: 'Subjects' },
        loadComponent: () => import('./subjects/class-subject-mapping').then(m => m.ClassSubjectMapping)
      },
      // Employees — order matters: 'new' and ':id/edit' before ':id'
      {
        path: 'employees',
        canActivate: [permissionGuard],
        data: { feature: 'Employees' },
        loadComponent: () => import('./employees/employees').then(m => m.Employees)
      },
      {
        path: 'employees/new',
        canActivate: [permissionGuard],
        data: { feature: 'Employees' },
        loadComponent: () => import('./employees/employee-form').then(m => m.EmployeeForm)
      },
      {
        path: 'employees/:id/edit',
        canActivate: [permissionGuard],
        data: { feature: 'Employees' },
        loadComponent: () => import('./employees/employee-form').then(m => m.EmployeeForm)
      },
      {
        path: 'employees/:id',
        canActivate: [permissionGuard],
        data: { feature: 'Employees' },
        loadComponent: () => import('./employees/employee-profile').then(m => m.EmployeeProfile)
      },
      // Students — order matters: fixed segments before ':id'
      {
        path: 'students',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/students').then(m => m.Students)
      },
      {
        path: 'students/new',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/admission-form/admission-form').then(m => m.AdmissionForm)
      },
      {
        path: 'students/import',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/student-import').then(m => m.StudentImport)
      },
      {
        path: 'students/promote',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/student-promote').then(m => m.StudentPromote)
      },
      {
        path: 'students/:id/edit',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/admission-form/admission-form').then(m => m.AdmissionForm)
      },
      {
        path: 'students/:id',
        canActivate: [permissionGuard],
        data: { feature: 'Students' },
        loadComponent: () => import('../students/student-profile').then(m => m.StudentProfile)
      },
      // Fees (FEE-01..14) — gated by the 'Fees' AppFeature. The API still enforces
      // per-action checks (Fees.Update on collect, Fees.Approve on concessions, …)
      // so a Read-only grant lets the user browse without being able to mutate.
      {
        path: 'fees',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-dashboard/fee-dashboard').then(m => m.FeeDashboard)
      },
      {
        path: 'fees/setup',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-setup/fee-setup').then(m => m.FeeSetup)
      },
      {
        path: 'fees/generate',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-generate/fee-generate').then(m => m.FeeGenerate)
      },
      {
        path: 'fees/collect',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-collect/fee-collect').then(m => m.FeeCollect)
      },
      {
        // Class Fee Board — bulk-collect by class, multi-select students,
        // shared payment defaults, per-row amount override.
        path: 'fees/class-board',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-class-board/fee-class-board').then(m => m.FeeClassBoard)
      },
      {
        path: 'fees/ledger',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-ledger/fee-ledger').then(m => m.FeeLedger)
      },
      {
        path: 'fees/reports',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-reports/fee-reports').then(m => m.FeeReports)
      },
      {
        path: 'fees/concessions',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-concessions/fee-concessions').then(m => m.FeeConcessions)
      },
      {
        path: 'fees/reminders',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-reminders/fee-reminders').then(m => m.FeeReminders)
      },
      {
        path: 'fees/arrears',
        canActivate: [permissionGuard],
        data: { feature: 'Fees' },
        loadComponent: () => import('../fees/fee-arrears/fee-arrears').then(m => m.FeeArrears)
      },
      // Inventory (INV-01..08) — gated by the 'Inventory' AppFeature. Sub-screens
      // (catalogue / purchase / issue / returns) share the same Read gate; write
      // actions are enforced server-side via Inventory.Create / Update / Delete.
      {
        path: 'inventory',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-dashboard/inventory-dashboard').then(m => m.InventoryDashboard)
      },
      {
        path: 'inventory/catalogue',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-catalogue/inventory-catalogue').then(m => m.InventoryCatalogue)
      },
      {
        path: 'inventory/purchase',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-purchase/inventory-purchase').then(m => m.InventoryPurchase)
      },
      {
        path: 'inventory/issue',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-issue/inventory-issue').then(m => m.InventoryIssue)
      },
      {
        path: 'inventory/alerts',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-alerts/inventory-alerts').then(m => m.InventoryAlerts)
      },
      {
        path: 'inventory/returns',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-returns/inventory-returns').then(m => m.InventoryReturns)
      },
      {
        path: 'inventory/reports',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-reports/inventory-reports').then(m => m.InventoryReports)
      },
      // INV: staff self-service — "what's been issued to me" / "request items"
      {
        path: 'inventory/my-issued',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-my-issued/inventory-my-issued').then(m => m.InventoryMyIssued)
      },
      {
        path: 'inventory/requests',
        canActivate: [permissionGuard],
        data: { feature: 'Inventory' },
        loadComponent: () => import('./inventory/inventory-issue-requests/inventory-issue-requests').then(m => m.InventoryIssueRequests)
      },
      // Expenses + Payroll (EXP-01..05) — gated by 'Expenses' AppFeature
      {
        path: 'expenses',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/expense-list/expense-list').then(m => m.ExpenseList)
      },
      {
        path: 'expenses/categories',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/expense-categories/expense-categories').then(m => m.ExpenseCategories)
      },
      {
        path: 'expenses/recurring',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/expense-recurring/expense-recurring').then(m => m.ExpenseRecurring)
      },
      {
        path: 'expenses/budget',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/expense-budget/expense-budget').then(m => m.ExpenseBudget)
      },
      {
        path: 'expenses/payroll',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/payroll-runs/payroll-runs').then(m => m.PayrollRuns)
      },
      {
        path: 'expenses/payroll/:id',
        canActivate: [permissionGuard],
        data: { feature: 'Expenses' },
        loadComponent: () => import('./expenses/payroll-detail/payroll-detail').then(m => m.PayrollDetail)
      },
      // Finance & Accounts (FIN-01..05) — gated by 'Finance' AppFeature
      {
        path: 'finance',
        canActivate: [permissionGuard],
        data: { feature: 'Finance' },
        loadComponent: () => import('./finance/finance-dashboard/finance-dashboard').then(m => m.FinanceDashboard)
      },
      {
        path: 'finance/pnl',
        canActivate: [permissionGuard],
        data: { feature: 'Finance' },
        loadComponent: () => import('./finance/finance-pnl/finance-pnl').then(m => m.FinancePnl)
      },
      {
        path: 'finance/reports',
        canActivate: [permissionGuard],
        data: { feature: 'Finance' },
        loadComponent: () => import('./finance/finance-reports/finance-reports').then(m => m.FinanceReports)
      },
      {
        path: 'finance/cashflow',
        canActivate: [permissionGuard],
        data: { feature: 'Finance' },
        loadComponent: () => import('./finance/finance-cashflow/finance-cashflow').then(m => m.FinanceCashflow)
      },
      // Attendance — gated by 'Attendance' AppFeature. The daily grid is the
      // primary screen; the monthly bulk-summary screen is still reachable at
      // /admin/attendance/monthly for back-fills.
      {
        path: 'attendance',
        canActivate: [permissionGuard],
        data: { feature: 'Attendance' },
        loadComponent: () => import('../attendance/attendance-daily/attendance-daily').then(m => m.AttendanceDaily)
      },
      {
        path: 'attendance/monthly',
        canActivate: [permissionGuard],
        data: { feature: 'Attendance' },
        loadComponent: () => import('../attendance/attendance-entry/attendance-entry').then(m => m.AttendanceEntry)
      },
      // Results (RES-01..08) — gated by 'Results' AppFeature
      {
        path: 'results',
        canActivate: [permissionGuard],
        data: { feature: 'Results' },
        loadComponent: () => import('../results/result-sessions/result-sessions').then(m => m.ResultSessions)
      },
      {
        path: 'results/grade-bands',
        canActivate: [permissionGuard],
        data: { feature: 'Results' },
        loadComponent: () => import('../results/grade-bands/grade-bands').then(m => m.GradeBands)
      },
      {
        path: 'results/marks',
        canActivate: [permissionGuard],
        data: { feature: 'Results' },
        loadComponent: () => import('../results/marks-entry/marks-entry').then(m => m.MarksEntry)
      },
      {
        path: 'results/class-sheet',
        canActivate: [permissionGuard],
        data: { feature: 'Results' },
        loadComponent: () => import('../results/class-sheet/class-sheet').then(m => m.ClassSheet)
      },
      // Access Control (admin-only)
      {
        path: 'access',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./access-control/access-control').then(m => m.AccessControl)
      },
      // User Management (admin-only) — create logins, reset passwords, manage roles
      {
        path: 'users',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./user-management/user-management').then(m => m.UserManagement)
      },
      // Role Permissions (admin-only) — the template new users inherit from their role
      {
        path: 'role-permissions',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./role-permissions/role-permissions').then(m => m.RolePermissions)
      },
      // School calendar — gated by 'Calendar' AppFeature (admin bypass, others by grant)
      {
        path: 'calendar',
        canActivate: [permissionGuard],
        data: { feature: 'Calendar' },
        loadComponent: () => import('./calendar/calendar').then(m => m.CalendarAdmin)
      },
      // Staff-attendance surfaces — each gated by its own AppFeature so the
      // admin can allow / deny them per role via the Permission Matrix.
      {
        path: 'attendance-summary',
        canActivate: [permissionGuard],
        data: { feature: 'AttendanceSummary' },
        loadComponent: () => import('./attendance-summary/attendance-summary').then(m => m.AttendanceSummary)
      },
      {
        path: 'my-attendance',
        canActivate: [permissionGuard],
        data: { feature: 'StaffAttendance' },
        loadComponent: () => import('./staff-attendance/my-staff-attendance').then(m => m.MyStaffAttendance)
      },
      {
        path: 'staff-attendance',
        canActivate: [permissionGuard],
        data: { feature: 'StaffAttendanceOverview' },
        loadComponent: () => import('./staff-attendance/staff-attendance-overview').then(m => m.StaffAttendanceOverview)
      },
      {
        path: 'parent-links',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./parent-links/parent-links').then(m => m.ParentLinks)
      },
      // Leaves — self-service for any role granted 'Leaves'; admin surfaces gated by 'LeavesAdmin'.
      {
        path: 'leaves/apply',
        canActivate: [permissionGuard],
        data: { feature: 'Leaves' },
        loadComponent: () => import('./leaves/leave-apply').then(m => m.LeaveApply)
      },
      {
        path: 'leaves/mine',
        canActivate: [permissionGuard],
        data: { feature: 'Leaves' },
        loadComponent: () => import('./leaves/leave-mine').then(m => m.LeaveMine)
      },
      {
        // Approver queue — the SP filters to items where the caller is the routed approver,
        // so the layout's authGuard is enough (no extra permission gate needed).
        path: 'leaves/approvals',
        loadComponent: () => import('./leaves/leave-approvals').then(m => m.LeaveApprovals)
      },
      {
        path: 'leaves/types',
        canActivate: [permissionGuard],
        data: { feature: 'LeavesAdmin' },
        loadComponent: () => import('./leaves/leave-types').then(m => m.LeaveTypes)
      },
      {
        path: 'leaves/routing',
        canActivate: [permissionGuard],
        data: { feature: 'LeavesAdmin' },
        loadComponent: () => import('./leaves/leave-routing').then(m => m.LeaveRouting)
      },
      {
        path: 'leaves/policy',
        canActivate: [permissionGuard],
        data: { feature: 'LeavesAdmin' },
        loadComponent: () => import('./leaves/leave-policy').then(m => m.LeavePolicy)
      },
      {
        path: 'leaves/dashboard',
        canActivate: [permissionGuard],
        data: { feature: 'LeavesAdmin' },
        loadComponent: () => import('./leaves/leave-dashboard').then(m => m.LeaveDashboard)
      },
      // Complaints / Suggestions — login user can log + view-own (Complaints feature).
      // Admin / reviewer report is gated by ComplaintsReport. Nature catalogue is admin-only.
      {
        path: 'complaints/new',
        canActivate: [permissionGuard],
        data: { feature: 'Complaints' },
        loadComponent: () => import('./complaints/complaint-form').then(m => m.ComplaintForm)
      },
      {
        path: 'complaints/mine',
        canActivate: [permissionGuard],
        data: { feature: 'Complaints' },
        loadComponent: () => import('./complaints/my-complaints').then(m => m.MyComplaints)
      },
      {
        path: 'complaints/:id/edit',
        canActivate: [permissionGuard],
        data: { feature: 'Complaints' },
        loadComponent: () => import('./complaints/complaint-form').then(m => m.ComplaintForm)
      },
      {
        path: 'complaints/nature',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./complaints/complaint-nature').then(m => m.ComplaintNatureAdmin)
      },
      {
        path: 'complaints/report',
        canActivate: [permissionGuard],
        data: { feature: 'ComplaintsReport' },
        loadComponent: () => import('./complaints/complaints-report').then(m => m.ComplaintsReport)
      }
    ]
  }
];
