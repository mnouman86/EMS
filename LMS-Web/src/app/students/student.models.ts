export interface StudentListItem {
  id: number;
  studentCode?: string | null;
  formNo?: string | null;
  fullName: string;
  parentName?: string;
  gender?: string;
  dateOfBirth?: string | null;
  admittedClassId?: number | null;
  admittedClassName?: string | null;
  gradeApplyingForId?: number | null;
  applyingForClassName?: string | null;
  status?: string; // Applied / Admitted / Active / Left / Alumni / Withdrawn
  emergencyContactPhone?: string;
}

export interface StudentDetail {
  id?: number;
  studentCode?: string | null;
  formNo?: string | null;

  fullName: string;
  gender: string;
  religion: string;
  dateOfBirth?: string | null;
  gradeApplyingForId: number;
  admittedClassId?: number | null;
  admittedClassName?: string | null;
  admittedLevelCode?: string | null;

  parentName: string;
  parentRelationship: string;
  parentEmail: string;
  parentMobile: string;
  homeAddress: string;

  emergencyContactName: string;
  emergencyContactPhone: string;

  hasMedicalConditions: boolean;
  medicalDetails?: string | null;
  previousSchoolName?: string | null;
  previousSchoolClass?: string | null;
  previousSchoolDateLeft?: string | null;

  declarationAccepted?: boolean;
  declarationAcceptedAt?: string | null;
  signatureName?: string | null;
  signatureImagePath?: string | null;
  signatureDate?: string | null;

  status?: string | null;
  admittedAt?: string | null;
  leftOrAlumniAt?: string | null;
  lifecycleReason?: string | null;
  cultureId?: number | null;
}

/** Payload for StudentCreate (public admission). */
export interface SubmitAdmission {
  fullName: string;
  gender: string;
  religion: string;
  dateOfBirth: string;
  gradeApplyingForId: number;
  parentName: string;
  parentRelationship: string;
  parentEmail: string;
  parentMobile: string;
  homeAddress: string;
  emergencyContactName: string;
  emergencyContactPhone: string;
  hasMedicalConditions: boolean;
  medicalDetails?: string | null;
  previousSchoolName?: string | null;
  previousSchoolClass?: string | null;
  previousSchoolDateLeft?: string | null;
  declarationAccepted: boolean;
  signatureName?: string | null;
  signatureImagePath?: string | null;
  signatureDate?: string | null;
  cultureId?: number | null;
}

export interface UpdateStudent {
  id: number;
  fullName: string;
  gender: string;
  religion: string;
  dateOfBirth: string;
  gradeApplyingForId: number;
  parentName: string;
  parentRelationship: string;
  parentEmail: string;
  parentMobile: string;
  homeAddress: string;
  emergencyContactName: string;
  emergencyContactPhone: string;
  hasMedicalConditions: boolean;
  medicalDetails?: string | null;
  previousSchoolName?: string | null;
  previousSchoolClass?: string | null;
  previousSchoolDateLeft?: string | null;
  cultureId?: number | null;
}

export interface ChangeStudentStatus {
  id: number;
  targetStatus: string;
  admittedClassId?: number | null;
  lifecycleReason?: string | null;
}

export interface StudentImportRow {
  srNo?: number | null;
  studentId?: string | null;
  formNo?: string | null;
  studentName?: string | null;
  fatherName?: string | null;
  emergencyContact?: string | null;
  className?: string | null;
}

export interface PromoteStudents {
  sourceClassId: number;
  targetClassId: number;
  studentIds: number[];
  moveToAlumni: boolean;
}
