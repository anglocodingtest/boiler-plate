import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommoditiesService } from '../../service/commodities.service';
import { TradeactionsComponent } from './tradeactions.component';
import { of } from 'rxjs';
import { TradeAction } from '../../models/trade-actions';

describe('TradeactionsComponent', () => {
  let component: TradeactionsComponent;
  let fixture: ComponentFixture<TradeactionsComponent>;

  const tradeActions: TradeAction[] = [
    {
      modelName: "S&P",
      commodityName: "Gold",
      tradeAction: -4,
      date: new Date("2020-06-26T00:00:00")
    },
    {
      modelName: "S&P",
      commodityName: "Gold",
      tradeAction: 3,
      date: new Date("2020-06-26T00:00:00")
    }
  ];
  
  const mockCommoditiesService  = {
    tradeActions$: of(tradeActions)
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TradeactionsComponent ],
      providers: [ {provide: CommoditiesService, useValue: mockCommoditiesService}]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TradeactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should populate trade actions', () => {
    component.tradeActions$.subscribe(data => {
      console.log(JSON.stringify(data));
      expect(data).toEqual(tradeActions);
    })
  });
});
