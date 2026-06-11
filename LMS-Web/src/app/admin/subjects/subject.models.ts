export interface Subject {
  id: number;
  name: string;
  shortCode?: string | null;
  displayOrder: number;
  isRTL: boolean;
  isActive: boolean;
  mappedClassCount?: number | null;
}

export interface CreateSubject {
  name: string;
  shortCode?: string | null;
  displayOrder: number;
  isRTL: boolean;
  isActive: boolean;
  cultureId?: number | null;
}

export interface UpdateSubject {
  id: number;
  name: string;
  shortCode?: string | null;
  displayOrder: number;
  isRTL: boolean;
  isActive: boolean;
  cultureId?: number | null;
}

/** Row returned by SubjectGetByClass (a class's mapped subjects). */
export interface ClassSubject {
  id: number;
  schoolClassId: number;
  subjectId: number;
  subjectName: string;
  shortCode?: string | null;
  isRTL: boolean;
  displayOrder: number;
}
