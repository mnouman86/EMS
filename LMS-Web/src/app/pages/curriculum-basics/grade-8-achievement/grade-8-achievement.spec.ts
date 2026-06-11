import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade8Achievement } from './grade-8-achievement';

describe('Grade8Achievement', () => {
  let component: Grade8Achievement;
  let fixture: ComponentFixture<Grade8Achievement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade8Achievement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade8Achievement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
