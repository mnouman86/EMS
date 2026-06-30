export interface MyClass {
  schoolClassId: number;
  levelName: string;
  levelCode: string;
  displayOrder: number;
}

export interface MySubject {
  subjectId: number;
  subjectName: string;
  shortCode?: string | null;
  displayOrder: number;
}

export interface MyClassSubject {
  schoolClassId: number;
  levelName: string;
  levelCode: string;
  subjectId: number;
  subjectName: string;
  shortCode?: string | null;
}

export interface MyTeachingBundle {
  classesIHead: MyClass[];
  mySubjects: MySubject[];
  classSubjectMap: MyClassSubject[];
}
