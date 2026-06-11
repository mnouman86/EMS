import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade2Expansion } from './grade-2-expansion';

describe('Grade2Expansion', () => {
  let component: Grade2Expansion;
  let fixture: ComponentFixture<Grade2Expansion>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade2Expansion]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade2Expansion);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
