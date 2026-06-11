import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreNurseryDiscovery } from './pre-nursery-discovery';

describe('PreNurseryDiscovery', () => {
  let component: PreNurseryDiscovery;
  let fixture: ComponentFixture<PreNurseryDiscovery>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreNurseryDiscovery]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreNurseryDiscovery);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
