import { ChangeDetectionStrategy, Component } from '@angular/core';
import * as Highcharts from "highcharts/highstock";
import { chartdetail } from '../../models/chart-detail';
import { CommoditiesService } from '../../service/commodities.service';
import { combineLatest, map, EMPTY, Subject, catchError } from 'rxjs';

@Component({
  selector: 'app-trend',
  templateUrl: './trend.component.html',
  styleUrls: ['./trend.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TrendComponent
{
  selectedModelSubject = new Subject<number>();
  selectedModelAction$ = this.selectedModelSubject.asObservable();
  selectedCommoditySubject = new Subject<number>();
  selectedCommodityAction$ = this.selectedCommoditySubject.asObservable();
  errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  models$ = this.commoditiesService.models$
        .pipe(catchError(err => this.handleError(err)));
      
  commodities$ = this.commoditiesService.commodities$
        .pipe(catchError(err => this.handleError(err)));

  data?: chartdetail[];

  constructor(private commoditiesService: CommoditiesService) { }
  
  changed$ = combineLatest([
    this.selectedModelAction$,
    this.selectedCommodityAction$,
  ]).pipe(
    map(([selectedModelId, selectedCommodityId]) => {
          if (selectedCommodityId > 0 && selectedModelId > 0){
          this.commoditiesService.getChartDetail(selectedCommodityId, selectedModelId).subscribe(
            (cd) => {
              if (cd.length > 0){
                this.data = cd;
                this.createHighchart();
              }
            });
          }
          return EMPTY;
        }),
        catchError(err => this.handleError(err)));

  createHighchart(){
    
    Highcharts.stockChart('container', {
      chart: {
        alignTicks: false,              
        },
      rangeSelector: {
          selected: 5
      },
      title: {
          text: 'Position'
      },
      series: [{
        type: 'spline',
          name: 'PnL',
          data: this.data?.map(d => {
            const dt = new Date(d.date);
            return [Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDay()), d.position];
          }),
          tooltip: {
              valueDecimals: 2
          }
      }
    ]
  });
  };
  
  onModelChanged(value:number) : void{    
    this.selectedModelSubject.next(value);
  }

  onCommodityChanged(value:number) : void{
    this.selectedCommoditySubject.next(value);
  }  
 
  handleError = (error: string) => {
    this.errorMessageSubject.next(error);
    return EMPTY;
  }
}
