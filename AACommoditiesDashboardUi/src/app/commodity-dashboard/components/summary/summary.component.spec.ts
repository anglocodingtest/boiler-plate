import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SummaryComponent } from './summary.component';
import { of } from 'rxjs';
import { CommoditiesService } from '../../service/commodities.service';
import { Model } from '../../models/model';
import { Commodity } from '../../models/commodity';
import { CommoditySummary } from '../../models/commodity-summary';

describe('SummaryComponent', () => {
  let component: SummaryComponent;
  let fixture: ComponentFixture<SummaryComponent>;

  const models: Model[] = [
    { id: 1, name: 'S&P' },
    { id: 2, name: 'FTSE' }
  ];

  const commodity: Commodity[] = [
    { id: 1, name: 'Oil' },
    { id: 2, name: 'Gold' }
  ];
  
  const commoditySummary: CommoditySummary[] = [
    {
      modelId: 1, commodityId: 1,
      modelName: "S&P", commodityName: "Oil",
      date: "2020-06-29T00:00:00", position: -3,
      pnlCurrent: 1290.96, price: 27242.7,
      pnlLtd: 84176.12,
      yearSummaries: [
        {
          "year": 2019, "pnlYtd": -43160.9,"drawdownYtd": -7762.97
        }
      ]
    },
    {
      modelId: 1, commodityId: 2,
      modelName: "S&P", commodityName: "Gold",
      date: "2021-06-29T00:00:00", position: -3,
      pnlCurrent: 2000.96, price: 3010.7,
      pnlLtd: 20000.12,
      yearSummaries: [
        {
          "year": 2021, "pnlYtd": 8989.9,"drawdownYtd": 76767.97
        }
      ]
    }
  ];

  const mockCommoditiesService  = {
    commodities$: of(models),
    models$: of(commodity),
    commoditySummary$: of(commoditySummary)
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SummaryComponent ],
      providers: [ {provide: CommoditiesService, useValue: mockCommoditiesService}]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should populate commodity summary when model and commodity is selected', () => {
    const expected: CommoditySummary = 
      {
        modelId: 1, commodityId: 1,
        modelName: "S&P", commodityName: "Oil",
        date: "2020-06-29T00:00:00", position: -3,
        pnlCurrent: 1290.96, price: 27242.7,
        pnlLtd: 84176.12,
        yearSummaries: [
          {
            "year": 2019, "pnlYtd": -43160.9,"drawdownYtd": -7762.97
          }]
      };

   component.summaryData$.subscribe(data => {
      console.log(JSON.stringify(data));
      expect(data).toEqual(expected);
    })

    component.onCommodityChanged(1);
    component.onModelChanged(1);    
  });

  it('should not populate commodity summary when summary not found for selected model and commodity', () => {
   
    component.summaryData$.subscribe(data => {
      console.log(JSON.stringify(data));
      expect(data).toBeUndefined();
    })
    component.onCommodityChanged(3);
    component.onModelChanged(3);    
  });
});
