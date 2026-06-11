import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassesPage } from './classes';

describe('Classes', () => {
  let component: ClassesPage;
  let fixture: ComponentFixture<ClassesPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassesPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
