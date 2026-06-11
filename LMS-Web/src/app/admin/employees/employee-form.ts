import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { ButtonModule } from 'primeng/button';
import { EmployeeService } from './employee.service';
import { EmployeeDetail } from './employee.models';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink,
    FieldsetModule, InputTextModule, InputNumberModule, TextareaModule,
    SelectModule, DatePickerModule, ToggleSwitchModule, ButtonModule
  ],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.scss'
})
export class EmployeeForm implements OnInit {
  private fb = inject(FormBuilder);
  private svc = inject(EmployeeService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(ToastService);

  saving = signal(false);
  loading = signal(false);
  employeeId = signal<number | null>(null);
  get isEditing(): boolean { return this.employeeId() !== null; }

  genderOptions = ['Male', 'Female', 'Other'].map(v => ({ label: v, value: v }));
  maritalOptions = ['Single', 'Married', 'Divorced', 'Widowed'].map(v => ({ label: v, value: v }));
  relationOptions = ['Father', 'Husband'].map(v => ({ label: v, value: v }));
  employmentOptions = ['Full-Time', 'Part-Time', 'Visiting'].map(v => ({ label: v, value: v }));

  form = this.fb.nonNullable.group({
    // Personal
    fullName: ['', [Validators.required]],
    fatherOrHusbandName: ['', [Validators.required]],
    relation: ['Father', [Validators.required]],
    dateOfBirth: [null as Date | null, [Validators.required]],
    gender: ['', [Validators.required]],
    maritalStatus: ['', [Validators.required]],
    nationality: ['Pakistani' as string | null],
    religion: ['' as string | null],
    cnic: ['', [Validators.required, Validators.pattern(/^\d{5}-\d{7}-\d$/)]],
    cnicExpiry: [null as Date | null],
    bloodGroup: ['' as string | null],
    medicalCondition: ['' as string | null],
    // Employment
    designation: ['', [Validators.required]],
    department: ['' as string | null],
    dateOfJoining: [null as Date | null, [Validators.required]],
    employmentType: ['', [Validators.required]],
    // Contact
    personalMobile: ['', [Validators.required, Validators.pattern(/^\+92\d{10}$/)]],
    whatsapp: ['' as string | null],
    landline: ['' as string | null],
    alternateMobile: ['' as string | null],
    officialEmail: ['' as string | null, [Validators.email]],
    personalEmail: ['' as string | null, [Validators.email]],
    // Address
    presentAddress: ['', [Validators.required]],
    permanentAddress: ['' as string | null],
    permanentSameAsPresent: [false],
    // Emergency
    emergencyContact1Name: ['', [Validators.required]],
    emergencyContact1Relation: ['', [Validators.required]],
    emergencyContact1Mobile: ['', [Validators.required]],
    emergencyContact2Name: ['' as string | null],
    emergencyContact2Relation: ['' as string | null],
    emergencyContact2Mobile: ['' as string | null],
    // Qualifications
    highestDegree: ['' as string | null],
    fieldOrMajor: ['' as string | null],
    passingYear: [null as number | null],
    institution: ['' as string | null],
    certifications: ['' as string | null],
    // Experience
    totalExperienceYears: [null as number | null],
    previousOrganisation: ['' as string | null],
    previousPosition: ['' as string | null],
    previousDuration: ['' as string | null],
    previousSubjectsTaught: ['' as string | null],
    previousReasonForLeaving: ['' as string | null],
    // Bank
    bankName: ['' as string | null],
    bankBranch: ['' as string | null],
    accountTitle: ['' as string | null],
    accountNumber: ['' as string | null],
    // References
    reference1Name: ['' as string | null],
    reference1Designation: ['' as string | null],
    reference1Organisation: ['' as string | null],
    reference1Contact: ['' as string | null],
    reference2Name: ['' as string | null],
    reference2Designation: ['' as string | null],
    reference2Organisation: ['' as string | null],
    reference2Contact: ['' as string | null]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.employeeId.set(Number(idParam));
      this.loadExisting(Number(idParam));
    }
  }

  private toDate(v?: string | null): Date | null {
    return v ? new Date(v) : null;
  }

  private loadExisting(id: number): void {
    this.loading.set(true);
    this.svc.getById(id).subscribe({
      next: res => {
        const e = res.data;
        if (e) {
          this.form.patchValue({
            ...e,
            dateOfBirth: this.toDate(e.dateOfBirth),
            cnicExpiry: this.toDate(e.cnicExpiry),
            dateOfJoining: this.toDate(e.dateOfJoining)
          } as any);
        }
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toast.warn('Please complete the required fields (marked *).');
      return;
    }
    this.saving.set(true);
    const v = this.form.getRawValue();
    const payload: EmployeeDetail = {
      ...(v as any),
      dateOfBirth: v.dateOfBirth ? v.dateOfBirth.toISOString() : null,
      cnicExpiry: v.cnicExpiry ? v.cnicExpiry.toISOString() : null,
      dateOfJoining: v.dateOfJoining ? v.dateOfJoining.toISOString() : null
    };

    const done = (msg: string) => { this.toast.success(msg); this.saving.set(false); this.router.navigate(['/admin/employees']); };
    const fail = () => this.saving.set(false);

    if (this.isEditing) {
      this.svc.update({ ...payload, id: this.employeeId()! }).subscribe({ next: () => done('Employee updated'), error: fail });
    } else {
      this.svc.create(payload).subscribe({ next: () => done('Employee registered'), error: fail });
    }
  }
}
