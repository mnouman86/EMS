import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FinanceService } from '../finance/finance.service';
import { InventoryService } from '../inventory/inventory.service';
import { StudentService } from '../../students/student.service';
import { EmployeeService } from '../employees/employee.service';
import { AcademicYearService } from '../academic-years/academic-year.service';
import { defaultSearch } from '../../core/models/search-request';
import { AuthService } from '../../core/services/auth.service';
import { Roles } from '../../core/models/roles';

interface QuickAction { label: string; icon: string; route: string; roles: string[]; }

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html'
})
export class AdminDashboard implements OnInit {
  private finance = inject(FinanceService);
  private inventory = inject(InventoryService);
  private studentSvc = inject(StudentService);
  private employeeSvc = inject(EmployeeService);
  private yearSvc = inject(AcademicYearService);
  private auth = inject(AuthService);

  user = this.auth.currentUser;

  isFinance = this.auth.hasAnyRole([Roles.Admin, Roles.Principal, Roles.Accountant]);
  isAdminOrPrincipal = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);
  isAdminOrAccountant = this.auth.hasAnyRole([Roles.Admin, Roles.Accountant]);

  yearLabel = signal<string>('');

  revenueYear = signal<number | null>(null);
  expensesYear = signal<number | null>(null);
  netSurplus = signal<number | null>(null);
  collectionRate = signal<number | null>(null);
  studentCount = signal<number | null>(null);
  employeeCount = signal<number | null>(null);
  lowStockCount = signal<number | null>(null);

  quickActions: QuickAction[] = [
    { label: 'Collect Fee', icon: 'pi pi-wallet', route: '/admin/fees/collect', roles: [Roles.Admin, Roles.Accountant] },
    { label: 'New Admission', icon: 'pi pi-user-plus', route: '/admin/students/new', roles: [Roles.Admin, Roles.Principal] },
    { label: 'Record Expense', icon: 'pi pi-credit-card', route: '/admin/expenses', roles: [Roles.Admin, Roles.Accountant] },
    { label: 'Generate Invoices', icon: 'pi pi-file-plus', route: '/admin/fees/generate', roles: [Roles.Admin, Roles.Accountant] },
    { label: 'Start Payroll', icon: 'pi pi-money-bill', route: '/admin/expenses/payroll', roles: [Roles.Admin, Roles.Accountant] },
    { label: 'Record Purchase', icon: 'pi pi-shopping-cart', route: '/admin/inventory/purchase', roles: [Roles.Admin, Roles.Accountant] },
    { label: 'Defaulters', icon: 'pi pi-exclamation-circle', route: '/admin/fees/reports', roles: [Roles.Admin, Roles.Principal, Roles.Accountant] },
    { label: 'P&L Statement', icon: 'pi pi-chart-line', route: '/admin/finance/pnl', roles: [Roles.Admin, Roles.Principal, Roles.Accountant] }
  ];

  visibleActions(): QuickAction[] {
    return this.quickActions.filter(a => this.auth.hasAnyRole(a.roles));
  }

  ngOnInit(): void {
    if (this.isAdminOrPrincipal) {
      this.studentSvc.getAll(defaultSearch()).subscribe(res => this.studentCount.set(res.totalCount ?? (res.data?.length ?? 0)));
      this.employeeSvc.getAll(defaultSearch()).subscribe(res => this.employeeCount.set(res.totalCount ?? (res.data?.length ?? 0)));
    }
    if (this.isAdminOrAccountant) {
      this.inventory.getLowStockAlerts().subscribe(res => this.lowStockCount.set((res.data ?? []).length));
    }
    if (this.isFinance) {
      this.yearSvc.getCurrent().subscribe(res => {
        const year = res.data;
        if (!year) return;
        this.yearLabel.set(year.displayName);
        this.finance.getIncomeSummary(year.id).subscribe(r => {
          this.revenueYear.set(r.data?.revenueThisYear ?? 0);
          this.collectionRate.set(r.data?.collectionRatePercent ?? 0);
          this.recomputeNet();
        });
        this.finance.getExpenseSummary(year.id).subscribe(r => {
          this.expensesYear.set(r.data?.expensesThisYear ?? 0);
          this.recomputeNet();
        });
      });
    }
  }

  private recomputeNet(): void {
    const rev = this.revenueYear();
    const exp = this.expensesYear();
    if (rev !== null && exp !== null) this.netSurplus.set(rev - exp);
  }
}
