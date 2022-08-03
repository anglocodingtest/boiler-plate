import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CommoditiesService } from './commodities.service';
import { Commodity } from '../models/commodity';
import { Model } from '../models/model';
import { CommoditySummary } from '../models/commodity-summary';
import { TradeAction } from '../models/trade-actions';
import { chartdetail } from '../models/chart-detail';

describe('CommoditiesService', () => {
  let service: CommoditiesService;
  let httpTestingController: HttpTestingController;
  
  beforeEach(() => {
    
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CommoditiesService]
    });
    httpTestingController = TestBed.get(HttpTestingController);
    service = TestBed.get(CommoditiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should be able to retrieve commodities', () => {
    const expected: Commodity[] = [
      { id: 1, name: 'Oil' },
      { id: 2, name: 'Gold' }
    ];

    service.commodities$.subscribe(data => {      
      expect(data).toEqual(expected);      
    });
     
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities');
    testRequest.flush(expected);
  });

  it('should be able to retrieve models', () => {
    const expected: Model[] = [
      { id: 1, name: 'S&P' },
      { id: 2, name: 'FTSE' }
    ];

    service.models$.subscribe(data => {      
      expect(data).toEqual(expected);      
    });
     
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities/models');
    testRequest.flush(expected);
  });

  it('should be able to retrieve commoditySummary', () => {
    const expected: CommoditySummary[] = [
      {
        modelId: 1, commodityId: 2,
        modelName: "S&P", commodityName: "Oil",
        date: "2020-06-29T00:00:00", position: -3,
        pnlCurrent: 1290.96, price: 27242.7,
        pnlLtd: 84176.12,
        yearSummaries: [
          {
            "year": 2019, "pnlYtd": -43160.9,"drawdownYtd": -7762.97
          }
        ]
      }
    ];

    service.commoditySummary$.subscribe(data => {      
      expect(data).toEqual(expected);      
    });
     
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities/summary');
    testRequest.flush(expected);
  });

  it('should be able to retrieve tradeAction', () => {
    const expected: TradeAction[] = [
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

    service.tradeActions$.subscribe(data => {      
      expect(data).toEqual(expected);      
    });
     
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities/tradeactions');
    testRequest.flush(expected);
  });

  it('should be able to retrieve ChartDetail', () => {
    const modelId = 1;
    const commodityId = 1;
    const expected: chartdetail[] = [
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

    service.getChartDetail(modelId, commodityId).subscribe(data => {      
      expect(data).toEqual(expected);      
    });
     
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities/chartdetail/1/1');
    testRequest.flush(expected);
  });

  it('should handle error', () => {
    const emsg = 'Backend returned error: Http failure response for http://localhost:5279/Commodities: 404 Not Found';
  
    service.commodities$.subscribe({      
      next: () => fail('should have failed.r'),
      error: (error: string) => {
         expect(error).toEqual(emsg);
      },   
    });
  
    const testRequest = httpTestingController.expectOne(req => req.method === 'GET'
      && req.url === 'http://localhost:5279/Commodities');
  
    testRequest.flush(emsg, { status: 404, statusText: 'Not Found' });
  });

  afterEach(() => {
      httpTestingController.verify();
  });
});
