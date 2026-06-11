import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurriculumLayout } from './curriculum-layout';

describe('CurriculumLayout', () => {
  let component: CurriculumLayout;
  let fixture: ComponentFixture<CurriculumLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CurriculumLayout]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurriculumLayout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
