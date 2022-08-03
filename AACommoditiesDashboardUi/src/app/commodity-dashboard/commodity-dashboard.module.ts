import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SummaryComponent } from './components/summary/summary.component';
import { TradeactionsComponent } from './components/tradeactions/tradeactions.component';
import { TrendComponent } from './components/trend/trend.component';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule } from '@angular/forms';
import { HighchartsChartModule } from 'highcharts-angular';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommodityDashboardRoutingModule } from './commodity-dashboard-routing.module';

@NgModule({
  declarations: [
    SummaryComponent,
    TradeactionsComponent,
    TrendComponent    
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
   MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    HttpClientModule,
    FormsModule,
    MatSelectModule,
    MatFormFieldModule,
    AgGridModule,
    HighchartsChartModule,
    CommodityDashboardRoutingModule
  ]
})
export class CommodityDashboardModule { }
