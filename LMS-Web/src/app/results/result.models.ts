/* ---------- RES-01: Sessions ---------- */
export interface ResultSession {
  id: number;
  name: string;
  status: string;
  maxWritten: number;
  maxOral: number;
  maxAttribute: number;
  weightWritten: number;
  weightOral: number;
  weightPerformance: number;
  roundingDecimals: number;
  isConfigLocked?: boolean | null;
  createdAt?: string | null;
}

export interface CreateResultSession {
  name: string;
  maxWritten: number;
  maxOral: number;
  maxAttribute: number;
  weightWritten: number;
  weightOral: number;
  weightPerformance: number;
  roundingDecimals: number;
}

/* ---------- RES-04: Grade bands ---------- */
export interface GradeBand {
  id?: number;
  grade: string;
  minPercent: number;
  maxPercent: number;
  remarkTemplate?: string | null;
  displayOrder: number;
}

export interface ConfigureGradeBands {
  bands: GradeBand[];
}

/* ---------- RES-02 / RES-03: Marks ---------- */
export interface MarksEntryGridRow {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  subjectId: number;
  subjectName: string;
  isRTL: boolean;
  written?: number | null;
  oral?: number | null;
  attrPunctuality?: number | null;
  attrDiscipline?: number | null;
  attrClassParticipation?: number | null;
  attrCreativity?: number | null;
  attrBehaviorWithPeers?: number | null;
  perSubjectPercent?: number | null;
  grade?: string | null;
  isLocked?: boolean | null;
}

export interface MarksRow {
  studentId: number;
  written?: number | null;
  oral?: number | null;
  attrPunctuality?: number | null;
  attrDiscipline?: number | null;
  attrClassParticipation?: number | null;
  attrCreativity?: number | null;
  attrBehaviorWithPeers?: number | null;
}

export interface EnterMarks {
  resultSessionId: number;
  schoolClassId: number;
  subjectId: number;
  teacherEmployeeId: number;
  entries: MarksRow[];
}

export interface MarksEntryGridRequest {
  resultSessionId: number;
  schoolClassId: number;
  subjectId: number;
  teacherEmployeeId: number;
}

/* ---------- RES-05: Lock / pre-lock ---------- */
export interface PreLockMissingRow {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  subjectId: number;
  subjectName: string;
  missing: string;
}

export interface LockClassResults {
  resultSessionId: number;
  schoolClassId: number;
  reason?: string | null;
}

/* ---------- RES-06: Class sheet ---------- */
export interface ClassSheetRow {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  overallPercent?: number | null;
  overallGrade?: string | null;
  remark?: string | null;
}

/* ---------- RES-08: Student result card ---------- */
export interface StudentResultCard {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  resultSessionId: number;
  sessionName: string;
  className: string;
  overallPercent?: number | null;
  overallGrade?: string | null;
  remark?: string | null;
  issuedAt: string;
  fileNameSuggestion: string;
  marks: MarksEntryGridRow[];
}

/* ---------- RES-07: Parent search ---------- */
export interface ParentResultSummary {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  resultSessionId: number;
  sessionName: string;
  className: string;
  overallPercent?: number | null;
  overallGrade?: string | null;
  remark?: string | null;
  issuedAt: string;
}
