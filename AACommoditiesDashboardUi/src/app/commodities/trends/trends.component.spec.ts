import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { CommoditiesService } from '../api/commodities.service';
import { IModelCommodity } from '../api/models/model-commodity';

import { TrendsComponent } from './trends.component';

describe('TrendsComponent', () => {
  let component: TrendsComponent;
  let fixture: ComponentFixture<TrendsComponent>;

  const modelCommodities: IModelCommodity[] = [{
    id: 99,
    modelName: 'modelName',
    commodityName: 'commodityName',
    pnLDaily: 0,
    pnlLTD: 1,
    position: 2,
    price: 3,
    varAllocation: 4
  }];

  const commoditiesStub: CommoditiesService = jasmine.createSpyObj('CommoditiesService', ['getModelCommodities']);
  commoditiesStub.getModelCommodities = jasmine.createSpy().and.returnValue(of(modelCommodities));

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrendsComponent ],
      imports: [HttpClientTestingModule],
      providers: [
        {provide: CommoditiesService, useValue: commoditiesStub}
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('call ngOnInit', () => {
    it('should call getModelCommodities', () => {
      const commodities: CommoditiesService = fixture.debugElement.injector.get(CommoditiesService);
      component.ngOnInit();

      expect(commodities.getModelCommodities).toHaveBeenCalled();
    });

    it('should set selected model', () => {
      expect(component.selectedModel?.id).toBe(99);
    });
  });
});
