import { Routes } from '@angular/router';
import { authGuard } from '../core/guards/auth.guard';
import { roleGuard } from '../core/guards/role.guard';
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
        path: 'forbidden',
        loadComponent: () => import('./forbidden/forbidden').then(m => m.Forbidden)
      },
      {
        path: 'academic-years',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./academic-years/academic-years').then(m => m.AcademicYears)
      },
      {
        path: 'classes',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./classes/classes').then(m => m.Classes)
      },
      {
        path: 'subjects',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./subjects/subjects').then(m => m.Subjects)
      },
      {
        path: 'subjects/mapping',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./subjects/class-subject-mapping').then(m => m.ClassSubjectMapping)
      },
      // Employees — order matters: 'new' and ':id/edit' before ':id'
      {
        path: 'employees',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./employees/employees').then(m => m.Employees)
      },
      {
        path: 'employees/new',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./employees/employee-form').then(m => m.EmployeeForm)
      },
      {
        path: 'employees/:id/edit',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./employees/employee-form').then(m => m.EmployeeForm)
      },
      {
        path: 'employees/:id',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('./employees/employee-profile').then(m => m.EmployeeProfile)
      },
      // Students — order matters: fixed segments before ':id'
      {
        path: 'students',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/students').then(m => m.Students)
      },
      {
        path: 'students/new',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/admission-form/admission-form').then(m => m.AdmissionForm)
      },
      {
        path: 'students/import',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/student-import').then(m => m.StudentImport)
      },
      {
        path: 'students/promote',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/student-promote').then(m => m.StudentPromote)
      },
      {
        path: 'students/:id/edit',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/admission-form/admission-form').then(m => m.AdmissionForm)
      },
      {
        path: 'students/:id',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../students/student-profile').then(m => m.StudentProfile)
      },
      // Fees (FEE-01..14)
      {
        path: 'fees',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('../fees/fee-dashboard/fee-dashboard').then(m => m.FeeDashboard)
      },
      {
        path: 'fees/setup',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('../fees/fee-setup/fee-setup').then(m => m.FeeSetup)
      },
      {
        path: 'fees/generate',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('../fees/fee-generate/fee-generate').then(m => m.FeeGenerate)
      },
      {
        path: 'fees/collect',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('../fees/fee-collect/fee-collect').then(m => m.FeeCollect)
      },
      {
        // Class Fee Board — bulk-collect by class, multi-select students,
        // shared payment defaults, per-row amount override.
        path: 'fees/class-board',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('../fees/fee-class-board/fee-class-board').then(m => m.FeeClassBoard)
      },
      {
        path: 'fees/ledger',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('../fees/fee-ledger/fee-ledger').then(m => m.FeeLedger)
      },
      {
        path: 'fees/reports',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('../fees/fee-reports/fee-reports').then(m => m.FeeReports)
      },
      {
        path: 'fees/concessions',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../fees/fee-concessions/fee-concessions').then(m => m.FeeConcessions)
      },
      {
        path: 'fees/reminders',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('../fees/fee-reminders/fee-reminders').then(m => m.FeeReminders)
      },
      {
        path: 'fees/arrears',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../fees/fee-arrears/fee-arrears').then(m => m.FeeArrears)
      },
      // Inventory (INV-01..08)
      {
        path: 'inventory',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-dashboard/inventory-dashboard').then(m => m.InventoryDashboard)
      },
      {
        path: 'inventory/catalogue',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-catalogue/inventory-catalogue').then(m => m.InventoryCatalogue)
      },
      {
        path: 'inventory/purchase',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-purchase/inventory-purchase').then(m => m.InventoryPurchase)
      },
      {
        path: 'inventory/issue',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-issue/inventory-issue').then(m => m.InventoryIssue)
      },
      {
        path: 'inventory/alerts',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-alerts/inventory-alerts').then(m => m.InventoryAlerts)
      },
      {
        path: 'inventory/returns',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-returns/inventory-returns').then(m => m.InventoryReturns)
      },
      {
        path: 'inventory/reports',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./inventory/inventory-reports/inventory-reports').then(m => m.InventoryReports)
      },
      // INV: staff self-service — "what's been issued to me"
      {
        path: 'inventory/my-issued',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('./inventory/inventory-my-issued/inventory-my-issued').then(m => m.InventoryMyIssued)
      },
      // INV: issue request workflow (teacher submits, accountant approves/fulfills)
      {
        path: 'inventory/requests',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('./inventory/inventory-issue-requests/inventory-issue-requests').then(m => m.InventoryIssueRequests)
      },
      // Expenses + Payroll (EXP-01..05)
      {
        path: 'expenses',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./expenses/expense-list/expense-list').then(m => m.ExpenseList)
      },
      {
        path: 'expenses/categories',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('./expenses/expense-categories/expense-categories').then(m => m.ExpenseCategories)
      },
      {
        path: 'expenses/recurring',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrAccountant },
        loadComponent: () => import('./expenses/expense-recurring/expense-recurring').then(m => m.ExpenseRecurring)
      },
      {
        path: 'expenses/budget',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./expenses/expense-budget/expense-budget').then(m => m.ExpenseBudget)
      },
      {
        path: 'expenses/payroll',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./expenses/payroll-runs/payroll-runs').then(m => m.PayrollRuns)
      },
      {
        path: 'expenses/payroll/:id',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./expenses/payroll-detail/payroll-detail').then(m => m.PayrollDetail)
      },
      // Finance & Accounts (FIN-01..05)
      {
        path: 'finance',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./finance/finance-dashboard/finance-dashboard').then(m => m.FinanceDashboard)
      },
      {
        path: 'finance/pnl',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./finance/finance-pnl/finance-pnl').then(m => m.FinancePnl)
      },
      {
        path: 'finance/reports',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./finance/finance-reports/finance-reports').then(m => m.FinanceReports)
      },
      {
        path: 'finance/cashflow',
        canActivate: [roleGuard],
        data: { roles: RoleSets.Finance },
        loadComponent: () => import('./finance/finance-cashflow/finance-cashflow').then(m => m.FinanceCashflow)
      },
      // Attendance — Admin / Principal / Teacher
      {
        path: 'attendance',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('../attendance/attendance-entry/attendance-entry').then(m => m.AttendanceEntry)
      },
      // Results (RES-01..08)
      {
        path: 'results',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('../results/result-sessions/result-sessions').then(m => m.ResultSessions)
      },
      {
        path: 'results/grade-bands',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOrPrincipal },
        loadComponent: () => import('../results/grade-bands/grade-bands').then(m => m.GradeBands)
      },
      {
        path: 'results/marks',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('../results/marks-entry/marks-entry').then(m => m.MarksEntry)
      },
      {
        path: 'results/class-sheet',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AnyStaff },
        loadComponent: () => import('../results/class-sheet/class-sheet').then(m => m.ClassSheet)
      },
      // Access Control (admin-only)
      {
        path: 'access',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./access-control/access-control').then(m => m.AccessControl)
      },
      // Parent links (admin-only)
      {
        path: 'parent-links',
        canActivate: [roleGuard],
        data: { roles: RoleSets.AdminOnly },
        loadComponent: () => import('./parent-links/parent-links').then(m => m.ParentLinks)
      }
    ]
  }
];
