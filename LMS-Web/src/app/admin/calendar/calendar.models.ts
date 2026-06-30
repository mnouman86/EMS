export interface SchoolHoliday {
  id: number;
  holidayDate: string;   // ISO date
  description: string;
  createdAt?: string | null;
  updatedAt?: string | null;
}

export interface CalendarConfig {
  weekendDays: string;   // CSV: "Saturday,Sunday"
  updatedAt?: string | null;
}

export interface NonWorkingDate {
  nonWorkingDate: string;  // ISO date
  reason: string;
}

export const ALL_WEEKDAYS = [
  'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'
] as const;
export type Weekday = typeof ALL_WEEKDAYS[number];
