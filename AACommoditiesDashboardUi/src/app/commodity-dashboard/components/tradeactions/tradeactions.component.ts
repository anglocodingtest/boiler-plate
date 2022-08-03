import { Component } from '@angular/core';
import { CommoditiesService } from '../../service/commodities.service';
import { ColDef } from 'ag-grid-community';
import { Subject, catchError, EMPTY } from 'rxjs';

@Component({
  selector: 'app-tradeactions',
  templateUrl: './tradeactions.component.html',
  styleUrls: ['./tradeactions.component.scss']  
})
export class TradeactionsComponent {
  errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  tradeActions$ = this.commoditiesService.tradeActions$
  .pipe(catchError(err => {
    this.errorMessageSubject.next(err);
    return EMPTY;
  }));

  constructor(private commoditiesService: CommoditiesService) { }  

  columnDefs: ColDef[] = [    
    { field: 'modelName', sortable: true, filter: true },
    { field: 'commodityName', sortable: true, filter: true },
    { field: 'tradeAction', sortable: true, filter: true },
    { field: 'date', sortable: true, filter: true,
      cellRenderer: (data: { value: Date; }) => {
        return data.value ? (new Date(data.value)).toLocaleDateString() : ''
    }}
  ];
}