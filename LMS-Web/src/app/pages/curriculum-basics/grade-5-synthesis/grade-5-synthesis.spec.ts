import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade5Synthesis } from './grade-5-synthesis';

describe('Grade5Synthesis', () => {
  let component: Grade5Synthesis;
  let fixture: ComponentFixture<Grade5Synthesis>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade5Synthesis]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade5Synthesis);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
