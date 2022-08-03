import { ComponentFixture, ComponentFixtureAutoDetect, TestBed } from '@angular/core/testing';
import { TrendComponent } from './trend.component';
import { of } from 'rxjs';
import { CommoditiesService } from '../../service/commodities.service';
import { Model } from '../../models/model';
import { Commodity } from '../../models/commodity';
import { chartdetail } from '../../models/chart-detail';

describe('TrendComponent', () => {
  let component: TrendComponent;
  let fixture: ComponentFixture<TrendComponent>;

  const models: Model[] = [
    { id: 1, name: 'S&P' },
    { id: 2, name: 'FTSE' }
  ];

  const chartDetail: chartdetail[] = [
    {
      date: new Date("2018-01-02T00:00:00"),
      pnlDaily: 15183.69,
      position: 1
    },
    {
      date: new Date("2018-01-03T00:00:00"),
      pnlDaily: 15380.51,
      position: 1
    },
  ];

  const commodity: Commodity[] = [
    { id: 1, name: 'Oil' },
    { id: 2, name: 'Gold' }
  ];

  const mockCommoditiesService  = {
    commodities$: of(models),
    models$: of(commodity),
    getChartDetail(c: number, m: number) { 
      if (c === 1 && m ===1) return of(chartDetail)
      else return of([]);}
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrendComponent ],
      providers: [ {provide: CommoditiesService, useValue: mockCommoditiesService}]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should populate chart detail when model and commodity is selected', () => {
    
    spyOn(component, 'createHighchart');

    component.onCommodityChanged(1);
    component.onModelChanged(1);  
    
    expect(component.data).toEqual(chartDetail);
    expect(component.createHighchart).toHaveBeenCalled();
  });

  it('should not populate chart detail when not found for selected model and commodity', () => {
    spyOn(component, 'createHighchart');

    component.onCommodityChanged(3);
    component.onModelChanged(3);  
    
    expect(component.data).toBeUndefined();
    expect(component.createHighchart).toHaveBeenCalledTimes(0);
  });
});
