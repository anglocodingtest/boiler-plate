import { TestBed } from '@angular/core/testing';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';

import { CommoditiesService } from './commodities.service';
import { ITradeAction } from './models/trade-action';

describe('CommoditiesService', () => {
  let service: CommoditiesService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CommoditiesService]
    });
    service = TestBed.get(CommoditiesService);
    httpMock = TestBed.get(HttpTestingController);
  });

  it('be able to retrieve trade actions from the API via GET', () => {
    const mockData: ITradeAction[] = [{
     modelName: 'model 1',
     commodityName: 'commodity 1',
     newTradeAction: 11
    }
  ];
    service.getTradeActions().subscribe(data => {
      expect(data.length).toBe(1);
      expect(data).toEqual(mockData);
    });
    const request = httpMock.expectOne('https://localhost:5001/commodities/tradeactions');
    expect(request.request.method).toBe('GET');
    request.flush(mockData);
  });

  afterEach(() => {
    httpMock.verify();
  });
});