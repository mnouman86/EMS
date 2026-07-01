export interface StaffAttendanceRow {
  id: number;
  userId: number;
  attendanceDate: string;
  checkInTime: string;
  checkOutTime?: string | null;
  remarks?: string | null;
  isBackdated: boolean;
}
export interface StaffAttendanceOverviewRow extends StaffAttendanceRow {
  userEmail?: string | null;
  staffName?: string | null;
  roleName?: string | null;
}
export interface StaffCheckInPayload {
  checkInTime?: string | null;
  remarks?: string | null;
  isBackdated: boolean;
}
export interface StaffCheckOutPayload {
  checkOutTime?: string | null;
  remarks?: string | null;
  isBackdated: boolean;
}
