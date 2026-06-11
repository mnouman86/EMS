import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { StudentService } from '../student.service';
import { SubmitAdmission, UpdateStudent } from '../student.models';
import { SchoolClassService } from '../../admin/classes/school-class.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { ApiService } from '../../core/services/api.service';

function pastDateValidator(control: AbstractControl): ValidationErrors | null {
  const v = control.value as Date | null;
  if (!v) return null;
  return v.getTime() < Date.now() ? null : { notPast: true };
}

@Component({
  selector: 'app-admission-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink,
    FieldsetModule, InputTextModule, TextareaModule, SelectModule,
    DatePickerModule, ToggleSwitchModule, CheckboxModule, ButtonModule
  ],
  templateUrl: './admission-form.html',
  styleUrl: './admission-form.scss'
})
export class AdmissionForm implements OnInit {
  private fb = inject(FormBuilder);
  private svc = inject(StudentService);
  private classSvc = inject(SchoolClassService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(ToastService);
  private api = inject(ApiService);

  saving = signal(false);
  loading = signal(false);
  uploading = signal(false);
  submitted = signal(false);
  studentId = signal<number | null>(null);

  classOptions = signal<{ label: string; value: number }[]>([]);

  /** Public route shows the marketing thank-you panel; admin returns to the list. */
  isPublic = this.route.snapshot.data['public'] === true;
  get isEditing(): boolean { return this.studentId() !== null; }
  showDeclaration = computed(() => !this.isEditing);

  genderOptions = ['Male', 'Female', 'Other'].map(v => ({ label: v, value: v }));
  religionOptions = ['Islam', 'Christianity', 'Other'].map(v => ({ label: v, value: v }));
  relationshipOptions = ['Father', 'Mother', 'Legal Guardian'].map(v => ({ label: v, value: v }));

  form = this.fb.nonNullable.group({
    // Student
    fullName: ['', [Validators.required]],
    gender: ['', [Validators.required]],
    religion: ['', [Validators.required]],
    dateOfBirth: [null as Date | null, [Validators.required, pastDateValidator]],
    gradeApplyingForId: [null as number | null, [Validators.required]],
    // Parent / Guardian
    parentName: ['', [Validators.required]],
    parentRelationship: ['', [Validators.required]],
    parentEmail: ['', [Validators.required, Validators.email]],
    parentMobile: ['', [Validators.required, Validators.pattern(/^\+92\d{10}$/)]],
    homeAddress: ['', [Validators.required]],
    // Emergency
    emergencyContactName: ['', [Validators.required]],
    emergencyContactPhone: ['', [Validators.required]],
    // Medical
    hasMedicalConditions: [false],
    medicalDetails: ['' as string | null],
    // Previous school
    previousSchoolName: ['' as string | null],
    previousSchoolClass: ['' as string | null],
    previousSchoolDateLeft: [null as Date | null],
    // Declaration / signature (apply mode only)
    declarationAccepted: [false],
    signatureName: ['' as string | null]
  });

  signatureImagePath = signal<string | null>(null);

  ngOnInit(): void {
    this.loadClasses();
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.studentId.set(Number(idParam));
      this.loadExisting(Number(idParam));
    }
  }

  private loadClasses(): void {
    this.classSvc.getAll(defaultSearch()).subscribe({
      next: res => {
        const active = (res.data ?? []).filter(c => c.isActive);
        this.classOptions.set(active.map(c => ({ label: c.levelName, value: c.id })));
      }
    });
  }

  private toDate(v?: string | null): Date | null {
    return v ? new Date(v) : null;
  }

  private loadExisting(id: number): void {
    this.loading.set(true);
    this.svc.getById(id).subscribe({
      next: res => {
        const s = res.data;
        if (s) {
          this.form.patchValue({
            fullName: s.fullName,
            gender: s.gender,
            religion: s.religion,
            dateOfBirth: this.toDate(s.dateOfBirth),
            gradeApplyingForId: s.gradeApplyingForId,
            parentName: s.parentName,
            parentRelationship: s.parentRelationship,
            parentEmail: s.parentEmail,
            parentMobile: s.parentMobile,
            homeAddress: s.homeAddress,
            emergencyContactName: s.emergencyContactName,
            emergencyContactPhone: s.emergencyContactPhone,
            hasMedicalConditions: s.hasMedicalConditions,
            medicalDetails: s.medicalDetails ?? '',
            previousSchoolName: s.previousSchoolName ?? '',
            previousSchoolClass: s.previousSchoolClass ?? '',
            previousSchoolDateLeft: this.toDate(s.previousSchoolDateLeft)
          });
        }
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  onSignatureSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.uploading.set(true);
    this.api.uploadFile(file).subscribe({
      next: res => { this.signatureImagePath.set(res.dbPath); this.uploading.set(false); this.toast.success('Signature uploaded'); },
      error: () => { this.uploading.set(false); this.toast.error('Signature upload failed'); }
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toast.warn('Please complete the required fields (marked *).');
      return;
    }
    const v = this.form.getRawValue();

    if (this.showDeclaration()) {
      if (!v.declarationAccepted) { this.toast.warn('You must accept the declaration to submit.'); return; }
      if (!this.signatureImagePath() && !(v.signatureName && v.signatureName.trim())) {
        this.toast.warn('Type your name as signature or upload a signature image.');
        return;
      }
    }

    this.saving.set(true);
    const fail = () => this.saving.set(false);

    if (this.isEditing) {
      const payload: UpdateStudent = {
        id: this.studentId()!,
        fullName: v.fullName,
        gender: v.gender,
        religion: v.religion,
        dateOfBirth: v.dateOfBirth!.toISOString(),
        gradeApplyingForId: v.gradeApplyingForId!,
        parentName: v.parentName,
        parentRelationship: v.parentRelationship,
        parentEmail: v.parentEmail,
        parentMobile: v.parentMobile,
        homeAddress: v.homeAddress,
        emergencyContactName: v.emergencyContactName,
        emergencyContactPhone: v.emergencyContactPhone,
        hasMedicalConditions: v.hasMedicalConditions,
        medicalDetails: v.medicalDetails || null,
        previousSchoolName: v.previousSchoolName || null,
        previousSchoolClass: v.previousSchoolClass || null,
        previousSchoolDateLeft: v.previousSchoolDateLeft ? v.previousSchoolDateLeft.toISOString() : null
      };
      this.svc.update(payload).subscribe({
        next: () => { this.toast.success('Student updated'); this.saving.set(false); this.router.navigate(['/admin/students', this.studentId()]); },
        error: fail
      });
      return;
    }

    const payload: SubmitAdmission = {
      fullName: v.fullName,
      gender: v.gender,
      religion: v.religion,
      dateOfBirth: v.dateOfBirth!.toISOString(),
      gradeApplyingForId: v.gradeApplyingForId!,
      parentName: v.parentName,
      parentRelationship: v.parentRelationship,
      parentEmail: v.parentEmail,
      parentMobile: v.parentMobile,
      homeAddress: v.homeAddress,
      emergencyContactName: v.emergencyContactName,
      emergencyContactPhone: v.emergencyContactPhone,
      hasMedicalConditions: v.hasMedicalConditions,
      medicalDetails: v.medicalDetails || null,
      previousSchoolName: v.previousSchoolName || null,
      previousSchoolClass: v.previousSchoolClass || null,
      previousSchoolDateLeft: v.previousSchoolDateLeft ? v.previousSchoolDateLeft.toISOString() : null,
      declarationAccepted: v.declarationAccepted,
      signatureName: v.signatureName || null,
      signatureImagePath: this.signatureImagePath(),
      signatureDate: new Date().toISOString()
    };
    this.svc.submit(payload).subscribe({
      next: () => {
        this.saving.set(false);
        if (this.isPublic) {
          this.submitted.set(true);
          window.scrollTo({ top: 0, behavior: 'smooth' });
        } else {
          this.toast.success('Admission submitted');
          this.router.navigate(['/admin/students']);
        }
      },
      error: fail
    });
  }
}
