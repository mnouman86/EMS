import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NurseryExploration } from './nursery-exploration';

describe('NurseryExploration', () => {
  let component: NurseryExploration;
  let fixture: ComponentFixture<NurseryExploration>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NurseryExploration]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NurseryExploration);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
