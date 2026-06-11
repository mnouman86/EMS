import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToddlerClass } from './toddler-class';

describe('ToddlerClass', () => {
  let component: ToddlerClass;
  let fixture: ComponentFixture<ToddlerClass>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ToddlerClass]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ToddlerClass);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
