import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SummaryComponent } from './components/summary/summary.component';
import { TradeactionsComponent } from './components/tradeactions/tradeactions.component';
import { TrendComponent } from './components/trend/trend.component';

const routes: Routes = [
  {path: 'summary', component: SummaryComponent},
  {path: 'tradeactions', component: TradeactionsComponent},
  {path: 'trend', component: TrendComponent}
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ], 
  exports: [ RouterModule ]
})

export class CommodityDashboardRoutingModule { }
