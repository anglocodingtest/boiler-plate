import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetricsDetailComponent } from './metrics-detail.component';

describe('MetricsDetailComponent', () => {
  let component: MetricsDetailComponent;
  let fixture: ComponentFixture<MetricsDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MetricsDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MetricsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
