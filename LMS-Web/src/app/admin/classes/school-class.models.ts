export interface SchoolClass {
  id: number;
  levelName: string;
  levelCode: string;
  gradeNumber?: number | null;
  displayOrder: number;
  capacity?: number | null;
  classTeacherId?: number | null;
  classTeacherName?: string | null;
  currentStudentCount?: number | null;
  isActive: boolean;
}

export interface CreateSchoolClass {
  levelName: string;
  levelCode: string;
  gradeNumber?: number | null;
  displayOrder: number;
  capacity?: number | null;
  classTeacherId?: number | null;
  isActive: boolean;
  cultureId?: number | null;
}

export interface UpdateSchoolClass {
  id: number;
  levelName: string;
  gradeNumber?: number | null;
  displayOrder: number;
  capacity?: number | null;
  classTeacherId?: number | null;
  isActive: boolean;
  cultureId?: number | null;
}
