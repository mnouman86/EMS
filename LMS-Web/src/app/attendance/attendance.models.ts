/** One row in the class entry grid (every active student in the class). */
export interface AttendanceGridRow {
  studentId: number;
  studentCode?: string | null;
  formNo?: string | null;
  fullName: string;
  studentStatus?: string | null;
  attendanceId?: number | null;
  workingDays: number;
  presentDays: number;
  remarks?: string | null;
  updatedAt?: string | null;
}

/** Bulk save payload. */
export interface AttendanceEntryInput {
  studentId: number;
  workingDays: number;
  presentDays: number;
  remarks?: string | null;
}

export interface BulkSaveAttendancePayload {
  academicYearId: number;
  schoolClassId: number;
  periodYear: number;
  periodMonth: number;
  entries: AttendanceEntryInput[];
}

/** One row per recorded period (parent view + student profile). */
export interface AttendancePeriod {
  id: number;
  periodYear: number;
  periodMonth: number;
  workingDays: number;
  presentDays: number;
  absentDays: number;
  attendancePercent?: number | null;
  remarks?: string | null;
  className?: string | null;
  academicYear?: string | null;
  updatedAt?: string | null;
}

/** Aggregate across a year (or all-time). */
export interface AttendanceSummary {
  studentId: number;
  totalWorkingDays: number;
  totalPresentDays: number;
  totalAbsentDays: number;
  periodsRecorded: number;
  overallPercent?: number | null;
}

/* ---------- Daily ---------- */
export type DailyStatus = 'Present' | 'Absent' | 'Late';

export interface DailyAttendanceGridRow {
  studentId: number;
  studentCode?: string | null;
  formNo?: string | null;
  fullName: string;
  studentStatus?: string | null;
  attendanceId?: number | null;
  dayStatus?: DailyStatus | null;
  remarks?: string | null;
  markedAt?: string | null;
}

export interface DailyEntryInput {
  studentId: number;
  status: DailyStatus;
  remarks?: string | null;
}

export interface BulkSaveDailyPayload {
  academicYearId: number;
  schoolClassId: number;
  attendanceDate: string;       // YYYY-MM-DD
  entries: DailyEntryInput[];
}

export interface StudentDailyAttendance {
  id: number;
  attendanceDate: string;
  status: DailyStatus;
  remarks?: string | null;
  className?: string | null;
  markedAt?: string | null;
}
