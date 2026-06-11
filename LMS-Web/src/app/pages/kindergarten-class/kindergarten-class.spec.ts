import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KindergartenClass } from './kindergarten-class';

describe('KindergartenClass', () => {
  let component: KindergartenClass;
  let fixture: ComponentFixture<KindergartenClass>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [KindergartenClass]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KindergartenClass);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
