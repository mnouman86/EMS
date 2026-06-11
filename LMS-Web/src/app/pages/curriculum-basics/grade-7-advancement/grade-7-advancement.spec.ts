import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Grade7Advancement } from './grade-7-advancement';

describe('Grade7Advancement', () => {
  let component: Grade7Advancement;
  let fixture: ComponentFixture<Grade7Advancement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Grade7Advancement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Grade7Advancement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
