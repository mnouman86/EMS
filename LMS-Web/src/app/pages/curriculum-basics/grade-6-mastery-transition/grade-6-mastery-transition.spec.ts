import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade6MasteryTransition } from './grade-6-mastery-transition';

describe('Grade6MasteryTransition', () => {
  let component: Grade6MasteryTransition;
  let fixture: ComponentFixture<Grade6MasteryTransition>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade6MasteryTransition]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade6MasteryTransition);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
