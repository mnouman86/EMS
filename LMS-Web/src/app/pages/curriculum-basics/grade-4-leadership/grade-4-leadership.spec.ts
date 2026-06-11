import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade4Leadership } from './grade-4-leadership';

describe('Grade4Leadership', () => {
  let component: Grade4Leadership;
  let fixture: ComponentFixture<Grade4Leadership>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade4Leadership]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade4Leadership);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
