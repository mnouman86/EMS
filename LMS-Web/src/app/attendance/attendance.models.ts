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
