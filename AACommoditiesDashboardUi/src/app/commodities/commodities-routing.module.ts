import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrendsComponent } from './trends/trends.component';
import { HistoryComponent } from './history/history.component';
import { MetricsComponent } from './metrics/metrics.component';

const routes: Routes = [
    { path: 'metrics', component: MetricsComponent },
    { path: 'trends', component: TrendsComponent },
    { path: 'history', component: HistoryComponent }
];

@NgModule({
    imports: [ RouterModule.forChild(routes) ], 
    exports: [ RouterModule ]
})
export class CommoditiesRoutingModule {

}