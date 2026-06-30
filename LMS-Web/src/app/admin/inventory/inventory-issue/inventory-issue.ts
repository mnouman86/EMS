import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { TagModule } from 'primeng/tag';
import { InventoryService } from '../inventory.service';
import { InventoryItem, IssueRegisterRow, IssueDetailRow } from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { SchoolClassService } from '../../classes/school-class.service';
import { EmployeeService } from '../../employees/employee.service';
import { StudentService } from '../../../students/student.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

interface IssueLineVM { itemId: number | null; quantity: number; stock: number; }
type IssuedToType = 'Class' | 'Teacher' | 'Department' | 'Student' | 'Employee';

interface RecipientOption { label: string; name: string; id: number | null; }

@Component({
  selector: 'app-inventory-issue',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, InputNumberModule, InputTextModule, TextareaModule, DatePickerModule,
    DialogModule, TooltipModule, TagModule
  ],
  templateUrl: './inventory-issue.html'
})
export class InventoryIssue implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private classSvc = inject(SchoolClassService);
  private empSvc = inject(EmployeeService);
  private studentSvc = inject(StudentService);
  private auth = inject(AuthService);

  canCancel = this.auth.hasAnyRole([Roles.Admin, Roles.Principal]);

  /* ---------- History grid (top half) ---------- */
  issues = signal<IssueRegisterRow[]>([]);
  issuesLoading = signal(false);
  range: Date[] = (() => { const to = new Date(); const from = new Date(); from.setDate(to.getDate() - 90); return [from, to]; })();

  detailRow = signal<IssueRegisterRow | null>(null);
  detailLines = signal<IssueDetailRow[]>([]);
  detailLoading = signal(false);
  detailOpen = signal(false);

  /* Cancel reason prompt */
  cancelOpen = signal(false);
  cancelReason = '';
  cancelling = signal(false);

  /* ---------- Add-new dialog (existing form) ---------- */
  formOpen = signal(false);

  private itemStock = new Map<number, number>();
  itemOptions = signal<{ label: string; value: number }[]>([]);
  typeOptions = ['Class', 'Teacher', 'Employee', 'Department', 'Student'].map(v => ({ label: v, value: v }));

  private classes: { id: number; levelName: string }[] = [];
  private teachers: { id: number; fullName: string; employeeCode: string }[] = [];
  private employees: { id: number; fullName: string; employeeCode: string; designation: string }[] = [];
  private students: { id: number; fullName: string; studentCode: string | null; formNo: string | null }[] = [];
  private departments: string[] = [];

  issueDate: Date = new Date();
  issuedToType: IssuedToType = 'Class';
  issuedToName = '';
  issuedToId: number | null = null;
  purpose = '';

  selectedRecipient: RecipientOption | null = null;
  recipientOptions = signal<RecipientOption[]>([]);

  lines = signal<IssueLineVM[]>([{ itemId: null, quantity: 1, stock: 0 }]);
  saving = signal(false);

  ngOnInit(): void {
    this.svc.getItems(defaultSearch()).subscribe(res => {
      const items = (res.data ?? []).filter((i: InventoryItem) => i.isActive);
      items.forEach(i => this.itemStock.set(i.id, i.currentStock));
      this.itemOptions.set(items.map(i => ({ label: `${i.name} (${i.code || '—'}) · stock ${i.currentStock}`, value: i.id })));
    });

    this.classSvc.getAll(defaultSearch()).subscribe(res => {
      this.classes = (res.data ?? []).filter(c => c.isActive).map(c => ({ id: c.id!, levelName: c.levelName }));
      if (this.issuedToType === 'Class') this.refreshRecipientOptions();
    });

    this.empSvc.getAll(defaultSearch()).subscribe(res => {
      const all = (res.data ?? []).filter(e => (e.status ?? 'Active') === 'Active');
      this.employees = all.map(e => ({
        id: e.id, fullName: e.fullName, employeeCode: e.employeeCode,
        designation: e.designation ?? ''
      }));
      this.teachers = all
        .filter(e => (e.designation || '').toLowerCase().includes('teacher'))
        .map(e => ({ id: e.id, fullName: e.fullName, employeeCode: e.employeeCode }));
      this.departments = Array.from(new Set(
        all.map(e => (e.department ?? '').trim()).filter(d => d.length > 0)
      )).sort();
      if (this.issuedToType === 'Teacher' || this.issuedToType === 'Employee' || this.issuedToType === 'Department') this.refreshRecipientOptions();
    });

    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      this.students = (res.data ?? [])
        .filter(s => (s.status ?? 'Active') === 'Active' || s.status === 'Admitted')
        .map(s => ({ id: s.id, fullName: s.fullName, studentCode: s.studentCode ?? null, formNo: s.formNo ?? null }));
      if (this.issuedToType === 'Student') this.refreshRecipientOptions();
    });

    this.loadIssues();
  }

  /* ---------- Grid actions ---------- */
  loadIssues(): void {
    if (!this.range?.[0] || !this.range?.[1]) { this.toast.warn('Pick a date range.'); return; }
    const [from, to] = this.range;
    this.issuesLoading.set(true);
    this.svc.getIssueRegister({
      fromDate: this.toIso(from),
      toDate: this.toIso(to)
    }).subscribe({
      next: r => { this.issues.set(r.data ?? []); this.issuesLoading.set(false); },
      error: () => this.issuesLoading.set(false)
    });
  }

  view(row: IssueRegisterRow): void {
    this.detailRow.set(row);
    this.detailLines.set([]);
    this.detailOpen.set(true);
    this.detailLoading.set(true);
    /* getIssueDetail returns one row per line — pull lines for this issue. */
    this.svc.getIssueDetail(row.id).subscribe({
      next: r => { this.detailLines.set(r.data ?? []); this.detailLoading.set(false); },
      error: () => this.detailLoading.set(false)
    });
  }

  /* ---------- Add-new dialog ---------- */
  openForm(): void {
    this.issueDate = new Date();
    this.issuedToType = 'Class';
    this.selectedRecipient = null;
    this.issuedToName = '';
    this.issuedToId = null;
    this.purpose = '';
    this.lines.set([{ itemId: null, quantity: 1, stock: 0 }]);
    this.refreshRecipientOptions();
    this.formOpen.set(true);
  }

  onTypeChange(): void {
    this.selectedRecipient = null;
    this.issuedToName = '';
    this.issuedToId = null;
    this.refreshRecipientOptions();
  }

  onRecipientPicked(): void {
    if (this.selectedRecipient) {
      this.issuedToName = this.selectedRecipient.name;
      this.issuedToId   = this.selectedRecipient.id;
    } else {
      this.issuedToName = '';
      this.issuedToId   = null;
    }
  }

  private refreshRecipientOptions(): void {
    let opts: RecipientOption[] = [];
    switch (this.issuedToType) {
      case 'Class':
        opts = this.classes.map(c => ({ label: c.levelName, name: c.levelName, id: c.id }));
        break;
      case 'Teacher':
        opts = this.teachers.map(t => ({
          label: `${t.fullName} (${t.employeeCode || '—'})`,
          name: t.fullName,
          id: t.id
        }));
        break;
      case 'Student':
        opts = this.students.map(s => ({
          label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`,
          name: s.fullName,
          id: s.id
        }));
        break;
      case 'Department':
        opts = this.departments.map(d => ({ label: d, name: d, id: null }));
        break;
      case 'Employee':
        opts = this.employees.map(e => ({
          label: `${e.fullName} (${e.employeeCode || '—'})${e.designation ? ' · ' + e.designation : ''}`,
          name: e.fullName,
          id: e.id
        }));
        break;
    }
    this.recipientOptions.set(opts);
  }

  addLine(): void { this.lines.update(ls => [...ls, { itemId: null, quantity: 1, stock: 0 }]); }
  removeLine(idx: number): void { this.lines.update(ls => ls.filter((_, i) => i !== idx)); }

  onItemChange(line: IssueLineVM): void {
    line.stock = line.itemId ? (this.itemStock.get(line.itemId) ?? 0) : 0;
    this.lines.update(ls => [...ls]);
  }

  save(): void {
    if (!this.issuedToName.trim()) { this.toast.warn('Pick a recipient.'); return; }
    const valid = this.lines().filter(l => l.itemId && l.quantity > 0);
    if (valid.length === 0) { this.toast.warn('Add at least one line with an item and quantity.'); return; }
    const over = valid.find(l => l.quantity > (this.itemStock.get(l.itemId!) ?? 0));
    if (over) { this.toast.warn('One or more lines exceed available stock.'); return; }

    this.saving.set(true);
    this.svc.issueItems({
      issueDate: this.issueDate.toISOString(),
      issuedToType: this.issuedToType,
      issuedToId: this.issuedToId,
      issuedToName: this.issuedToName,
      purpose: this.purpose || null,
      lines: valid.map(l => ({ itemId: l.itemId!, quantity: l.quantity }))
    }).subscribe({
      next: () => {
        this.toast.success('Items issued');
        this.saving.set(false);
        this.formOpen.set(false);
        this.loadIssues();
      },
      error: () => this.saving.set(false)
    });
  }

  openCancel(): void {
    if (!this.detailRow()) return;
    this.cancelReason = '';
    this.cancelOpen.set(true);
  }

  submitCancel(): void {
    const row = this.detailRow();
    if (!row) return;
    if (!this.cancelReason.trim()) { this.toast.warn('A reason is required.'); return; }
    this.cancelling.set(true);
    this.svc.cancelIssue(row.id, this.cancelReason.trim()).subscribe({
      next: r => {
        this.cancelling.set(false);
        if (r.isSuccess) {
          this.toast.success(r.message || 'Issue cancelled.');
          this.cancelOpen.set(false);
          this.detailOpen.set(false);
          this.loadIssues();
        } else {
          this.toast.error(r.message || 'Could not cancel the issue.');
        }
      },
      error: () => { this.cancelling.set(false); this.toast.error('Could not cancel the issue.'); }
    });
  }

  private toIso(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
