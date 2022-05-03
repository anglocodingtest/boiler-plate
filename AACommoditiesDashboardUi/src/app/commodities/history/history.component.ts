import { Component, OnDestroy, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import { Observable, Subscription } from 'rxjs';
import { CommoditiesService } from '../api/commodities.service';
import { ITradeAction } from '../api/models/trade-action';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription;
 
  constructor(private commoditiesService: CommoditiesService) {}


  ngOnInit(): void {
    this.subscription = this.commoditiesService.getTradeActions()
    .subscribe((tradeActions: ITradeAction[]) => this.rowData = tradeActions);
  }

  columnDefs: ColDef[] = [
    { field: 'modelName', sortable: true, filter: true },
    { field: 'commodityName', sortable: true, filter: true },
    { field: 'newTradeAction', sortable: true, filter: true }
  ];

  rowData: any[] = [];

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
