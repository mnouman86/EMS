import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { FileUploadModule } from 'primeng/fileupload';
import { EmployeeService } from './employee.service';
import {
  EmployeeDetail, TeacherAssignment, EmployeeDocument, EmployeeSalary, EmployeeAdvance, ClassSubjectPair
} from './employee.models';
import { SchoolClassService } from '../classes/school-class.service';
import { SubjectService } from '../subjects/subject.service';
import { SchoolClass } from '../classes/school-class.models';
import { Subject } from '../subjects/subject.models';
import { ApiService } from '../../core/services/api.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-employee-profile',
  standalone: true,
  imports: [
    CommonModule, DatePipe, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, TagModule, DialogModule,
    InputNumberModule, InputTextModule, SelectModule, DatePickerModule, FileUploadModule
  ],
  templateUrl: './employee-profile.html'
})
export class EmployeeProfile implements OnInit {
  private svc = inject(EmployeeService);
  private classSvc = inject(SchoolClassService);
  private subjectSvc = inject(SubjectService);
  private api = inject(ApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(ToastService);

  id!: number;
  loading = signal(false);
  employee = signal<EmployeeDetail | null>(null);

  assignments = signal<TeacherAssignment[]>([]);
  documents = signal<EmployeeDocument[]>([]);
  currentSalary = signal<EmployeeSalary | null>(null);
  salaryHistory = signal<EmployeeSalary[]>([]);
  advances = signal<EmployeeAdvance[]>([]);

  // assignment editor
  classes = signal<SchoolClass[]>([]);
  subjects = signal<Subject[]>([]);
  assignDialog = signal(false);
  assignSaving = signal(false);
  newClassId: number | null = null;
  newSubjectId: number | null = null;

  // salary dialog
  salaryDialog = signal(false);
  salarySaving = signal(false);
  salBasic = 0; salAllow = 0; salDeduct = 0; salFrom: Date | null = new Date();

  // advance dialog
  advanceDialog = signal(false);
  advanceSaving = signal(false);
  advAmount = 0; advReason = '';

  // document dialog
  docDialog = signal(false);
  docSaving = signal(false);
  docType = 'CNIC';
  docTypeOptions = ['Photo', 'CNIC', 'Degree', 'Certificate', 'Other'].map(v => ({ label: v, value: v }));
  pendingFile: File | null = null;

  netSalary = computed(() => {
    const s = this.currentSalary();
    return s ? s.basicSalary + s.allowances - s.fixedDeductions : 0;
  });

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadAll();
  }

  loadAll(): void {
    this.loading.set(true);
    this.svc.getById(this.id).subscribe({
      next: res => { this.employee.set(res.data ?? null); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
    this.loadAssignments();
    this.loadDocuments();
    this.loadSalary();
    this.loadAdvances();
  }

  loadAssignments(): void { this.svc.getTeacherAssignments(this.id).subscribe(r => this.assignments.set(r.data ?? [])); }
  loadDocuments(): void { this.svc.getDocuments(this.id).subscribe(r => this.documents.set(r.data ?? [])); }
  loadAdvances(): void { this.svc.getAdvances(this.id).subscribe(r => this.advances.set(r.data ?? [])); }
  loadSalary(): void {
    this.svc.getCurrentSalary(this.id).subscribe(r => this.currentSalary.set(r.data ?? null));
    this.svc.getSalaryHistory(this.id).subscribe(r => this.salaryHistory.set(r.data ?? []));
  }

  statusSeverity(status?: string | null): 'success' | 'warn' | 'secondary' {
    if (status === 'Active') return 'success';
    if (status === 'OnLeave') return 'warn';
    return 'secondary';
  }

  /* ---- Teacher assignments ---- */
  openAssign(): void {
    if (this.classes().length === 0) {
      forkJoin({ c: this.classSvc.getAll(), s: this.subjectSvc.getAll() }).subscribe(({ c, s }) => {
        this.classes.set((c.data ?? []).filter(x => x.isActive));
        this.subjects.set((s.data ?? []).filter(x => x.isActive));
      });
    }
    this.newClassId = null; this.newSubjectId = null;
    this.assignDialog.set(true);
  }

  addAssignment(): void {
    if (!this.newClassId || !this.newSubjectId) { this.toast.warn('Pick a class and a subject.'); return; }
    // Send the full desired set (existing + new) since the backend replaces.
    const pairs: ClassSubjectPair[] = this.assignments().map(a => ({ schoolClassId: a.schoolClassId, subjectId: a.subjectId }));
    if (pairs.some(p => p.schoolClassId === this.newClassId && p.subjectId === this.newSubjectId)) {
      this.toast.warn('That class–subject is already assigned.'); return;
    }
    pairs.push({ schoolClassId: this.newClassId, subjectId: this.newSubjectId });
    this.assignSaving.set(true);
    this.svc.assignTeacherSubjects(this.id, pairs).subscribe({
      next: () => { this.toast.success('Assignment added'); this.assignSaving.set(false); this.assignDialog.set(false); this.loadAssignments(); },
      error: () => this.assignSaving.set(false)
    });
  }

  removeAssignment(a: TeacherAssignment): void {
    const pairs = this.assignments()
      .filter(x => !(x.schoolClassId === a.schoolClassId && x.subjectId === a.subjectId))
      .map(x => ({ schoolClassId: x.schoolClassId, subjectId: x.subjectId }));
    this.svc.assignTeacherSubjects(this.id, pairs).subscribe({
      next: () => { this.toast.success('Assignment removed'); this.loadAssignments(); }
    });
  }

  /* ---- Salary ---- */
  openSalary(): void {
    const s = this.currentSalary();
    this.salBasic = s?.basicSalary ?? 0;
    this.salAllow = s?.allowances ?? 0;
    this.salDeduct = s?.fixedDeductions ?? 0;
    this.salFrom = new Date();
    this.salaryDialog.set(true);
  }

  saveSalary(): void {
    if (!this.salFrom) { this.toast.warn('Pick an effective-from date.'); return; }
    this.salarySaving.set(true);
    this.svc.upsertSalary({
      employeeId: this.id, basicSalary: this.salBasic, allowances: this.salAllow,
      fixedDeductions: this.salDeduct, effectiveFrom: this.salFrom.toISOString()
    }).subscribe({
      next: () => { this.toast.success('Salary structure saved'); this.salarySaving.set(false); this.salaryDialog.set(false); this.loadSalary(); },
      error: () => this.salarySaving.set(false)
    });
  }

  /* ---- Advances ---- */
  openAdvance(): void { this.advAmount = 0; this.advReason = ''; this.advanceDialog.set(true); }
  saveAdvance(): void {
    if (this.advAmount <= 0) { this.toast.warn('Amount must be greater than 0.'); return; }
    this.advanceSaving.set(true);
    this.svc.issueAdvance(this.id, this.advAmount, this.advReason).subscribe({
      next: () => { this.toast.success('Advance issued'); this.advanceSaving.set(false); this.advanceDialog.set(false); this.loadAdvances(); },
      error: () => this.advanceSaving.set(false)
    });
  }

  repayAdvance(a: EmployeeAdvance): void {
    if (a.outstandingBalance <= 0) return;
    this.svc.adjustAdvance(a.id, a.outstandingBalance, 'Full repayment').subscribe({
      next: () => { this.toast.success('Advance settled'); this.loadAdvances(); }
    });
  }

  /* ---- Documents ---- */
  openDoc(): void { this.docType = 'CNIC'; this.pendingFile = null; this.docDialog.set(true); }
  onDocSelect(event: { files: File[] }): void { this.pendingFile = event.files?.[0] ?? null; }

  saveDoc(): void {
    if (!this.pendingFile) { this.toast.warn('Choose a file first.'); return; }
    this.docSaving.set(true);
    const file = this.pendingFile;
    this.api.uploadFile(file).subscribe({
      next: up => {
        this.svc.uploadDocument({
          employeeId: this.id, documentType: this.docType, fileName: file.name,
          filePath: up.dbPath, contentType: file.type, fileSizeBytes: file.size,
          isPhoto: this.docType === 'Photo'
        }).subscribe({
          next: () => { this.toast.success('Document uploaded'); this.docSaving.set(false); this.docDialog.set(false); this.loadDocuments(); },
          error: () => this.docSaving.set(false)
        });
      },
      error: () => { this.docSaving.set(false); this.toast.error('File upload failed.'); }
    });
  }
}
