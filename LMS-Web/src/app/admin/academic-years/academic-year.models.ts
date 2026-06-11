export interface AcademicYear {
  id: number;
  code: string;
  displayName: string;
  startDate: string; // ISO
  endDate: string;   // ISO
  isOpen: boolean;
}

export interface CreateAcademicYear {
  code: string;
  displayName: string;
  startDate: string;
  endDate: string;
  isOpen: boolean;
}

export interface UpdateAcademicYear {
  id: number;
  displayName: string;
  startDate: string;
  endDate: string;
}
