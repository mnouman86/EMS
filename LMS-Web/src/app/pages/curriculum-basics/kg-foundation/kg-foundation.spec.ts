import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KgFoundation } from './kg-foundation';

describe('KgFoundation', () => {
  let component: KgFoundation;
  let fixture: ComponentFixture<KgFoundation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [KgFoundation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KgFoundation);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
