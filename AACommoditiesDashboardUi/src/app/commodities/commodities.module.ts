import { NgModule }      from '@angular/core';
import { FormsModule }      from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MetricsComponent } from './metrics/metrics.component';
import { TrendsComponent } from './trends/trends.component';
import { HistoryComponent } from './history/history.component';
import { CommoditiesRoutingModule } from './commodities-routing.module';
import { AgGridModule } from 'ag-grid-angular';
import { CommoditiesService } from './api/commodities.service';
import { MetricsDetailComponent } from './metrics/metrics-detail/metrics-detail.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule} from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { ChartModule } from 'angular-highcharts';
import { CommoditiesFilterComponent } from './shared/commodities-filter/commodities-filter.component';
import { TrendsChartComponent } from './trends/trends-chart/trends-chart.component';

@NgModule({
imports:      [ CommonModule, FormsModule, CommoditiesRoutingModule, AgGridModule , MatCardModule, MatFormFieldModule, MatSelectModule, ChartModule],
  declarations: [ MetricsComponent, TrendsComponent, HistoryComponent, CommoditiesFilterComponent, MetricsDetailComponent, TrendsChartComponent ],
  providers: [CommoditiesService],
  exports: [ MetricsComponent ]
})
export class CommoditiesModule { }