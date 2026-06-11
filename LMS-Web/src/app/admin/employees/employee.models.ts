/** Row shape returned by EmployeeGetAll. */
export interface EmployeeListItem {
  id: number;
  employeeCode: string;
  fullName: string;
  fatherOrHusbandName?: string;
  designation?: string;
  department?: string;
  employmentType?: string;
  status?: string; // Active / OnLeave / Left
  dateOfJoining?: string;
  cnic?: string;
  personalMobile?: string;
  officialEmail?: string;
  photoPath?: string;
}

/** Full flattened employee (EmployeeGetById + the Create/Update payload). */
export interface EmployeeDetail {
  id?: number;
  employeeCode?: string;

  // Personal
  fullName: string;
  fatherOrHusbandName: string;
  relation: string;
  dateOfBirth?: string | null;
  gender: string;
  maritalStatus: string;
  nationality?: string | null;
  religion?: string | null;
  cnic: string;
  cnicExpiry?: string | null;
  bloodGroup?: string | null;
  medicalCondition?: string | null;

  // Employment
  designation: string;
  department?: string | null;
  dateOfJoining?: string | null;
  employmentType: string;
  status?: string | null;
  lastWorkingDay?: string | null;
  reasonForLeaving?: string | null;

  // Contact
  personalMobile: string;
  whatsapp?: string | null;
  landline?: string | null;
  alternateMobile?: string | null;
  officialEmail?: string | null;
  personalEmail?: string | null;

  // Address
  presentAddress: string;
  permanentAddress?: string | null;
  permanentSameAsPresent?: boolean;

  // Emergency
  emergencyContact1Name: string;
  emergencyContact1Relation: string;
  emergencyContact1Mobile: string;
  emergencyContact2Name?: string | null;
  emergencyContact2Relation?: string | null;
  emergencyContact2Mobile?: string | null;

  // Qualifications
  highestDegree?: string | null;
  fieldOrMajor?: string | null;
  passingYear?: number | null;
  institution?: string | null;
  certifications?: string | null;

  // Experience
  totalExperienceYears?: number | null;
  previousOrganisation?: string | null;
  previousPosition?: string | null;
  previousDuration?: string | null;
  previousSubjectsTaught?: string | null;
  previousReasonForLeaving?: string | null;

  // Bank
  bankName?: string | null;
  bankBranch?: string | null;
  accountTitle?: string | null;
  accountNumber?: string | null;

  // References
  reference1Name?: string | null;
  reference1Designation?: string | null;
  reference1Organisation?: string | null;
  reference1Contact?: string | null;
  reference2Name?: string | null;
  reference2Designation?: string | null;
  reference2Organisation?: string | null;
  reference2Contact?: string | null;

  photoPath?: string | null;
  cultureId?: number | null;
}

export interface MarkEmployeeLeft {
  id: number;
  lastWorkingDay: string;
  reasonForLeaving?: string | null;
}

export interface ClassSubjectPair {
  schoolClassId: number;
  subjectId: number;
}

export interface TeacherAssignment {
  id: number;
  employeeId: number;
  schoolClassId: number;
  subjectId: number;
  className?: string;
  subjectName?: string;
  assignedAt?: string;
}

export interface EmployeeDocument {
  id: number;
  employeeId: number;
  documentType: string;
  fileName: string;
  filePath: string;
  contentType?: string;
  fileSizeBytes?: number;
  uploadedAt?: string;
}

export interface EmployeeSalary {
  id: number;
  employeeId: number;
  basicSalary: number;
  allowances: number;
  fixedDeductions: number;
  allowanceBreakdownJson?: string | null;
  effectiveFrom: string;
  effectiveTo?: string | null;
  isActive?: boolean;
}

export interface UpsertEmployeeSalary {
  employeeId: number;
  basicSalary: number;
  allowances: number;
  fixedDeductions: number;
  allowanceBreakdownJson?: string | null;
  effectiveFrom: string;
}

export interface EmployeeAdvance {
  id: number;
  employeeId: number;
  amount: number;
  outstandingBalance: number;
  reason?: string;
  issuedAt?: string;
  status?: string; // Open / Settled / WrittenOff
  settledAt?: string | null;
}
