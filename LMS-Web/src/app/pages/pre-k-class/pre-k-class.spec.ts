import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreKClass } from './pre-k-class';

describe('PreKClass', () => {
  let component: PreKClass;
  let fixture: ComponentFixture<PreKClass>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreKClass]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreKClass);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
