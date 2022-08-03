import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CommoditiesService } from 'src/app/commodity-dashboard/service/commodities.service';
import { combineLatest, map, Subject, catchError, EMPTY } from 'rxjs';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SummaryComponent  {
  selectedModelSubject = new Subject<number>();
  selectedModelAction$ = this.selectedModelSubject.asObservable();
  selectedCommoditySubject = new Subject<number>();
  selectedCommodityAction$ = this.selectedCommoditySubject.asObservable();
  errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  models$ = this.commoditiesService.models$;
  commodities$ = this.commoditiesService.commodities$;

  summaryData$ = combineLatest([
    this.commoditiesService.commoditySummary$,
    this.selectedModelAction$,
    this.selectedCommodityAction$,
  ])
  .pipe(
    map(([commoditySummary, selectedModelId, selectedCommodityId]) =>
          commoditySummary.find(c => c.commodityId === selectedModelId &&
            c.modelId === selectedCommodityId)),
    catchError(err => {
              this.errorMessageSubject.next(err);
              return EMPTY;
            }));

  constructor(private commoditiesService: CommoditiesService) { }

  onModelChanged(value:number) : void{    
    this.selectedModelSubject.next(value);
  }

  onCommodityChanged(value:number) : void{
    this.selectedCommoditySubject.next(value);
  }  
}
