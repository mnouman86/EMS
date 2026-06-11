import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade1Growth } from './grade-1-growth';

describe('Grade1Growth', () => {
  let component: Grade1Growth;
  let fixture: ComponentFixture<Grade1Growth>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade1Growth]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade1Growth);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
