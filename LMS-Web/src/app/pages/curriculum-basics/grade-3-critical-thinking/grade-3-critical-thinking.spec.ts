import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade3CriticalThinking } from './grade-3-critical-thinking';

describe('Grade3CriticalThinking', () => {
  let component: Grade3CriticalThinking;
  let fixture: ComponentFixture<Grade3CriticalThinking>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade3CriticalThinking]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade3CriticalThinking);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
