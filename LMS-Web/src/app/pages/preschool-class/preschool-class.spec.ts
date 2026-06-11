import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreschoolClass } from './preschool-class';

describe('PreschoolClass', () => {
  let component: PreschoolClass;
  let fixture: ComponentFixture<PreschoolClass>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreschoolClass]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreschoolClass);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
