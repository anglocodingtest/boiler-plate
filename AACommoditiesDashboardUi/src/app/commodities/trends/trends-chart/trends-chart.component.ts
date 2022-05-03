import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CommoditiesService } from '../../api/commodities.service';
import { Chart } from 'angular-highcharts';
import { Subscription } from 'rxjs';
import { IChartPoint } from '../../api/models/chart-point';

@Component({
  selector: 'app-trends-chart',
  templateUrl: './trends-chart.component.html',
  styleUrls: ['./trends-chart.component.css']
})
export class TrendsChartComponent implements OnInit, OnDestroy {
  @Input()
  set modelId(value: any) {
    if (value !== null)
    {
    this.subscription = this.commoditiesService.getChartData(value)
    .subscribe((chartData: IChartPoint[]) => {
      console.log(chartData);
      this.chartData = chartData;
      this.chart = new Chart({
        chart: {
          type: 'spline'
        },
        title: {
          text: 'Historical PnL'
        },
        credits: {
          enabled: false
        },
        xAxis: {
          type: 'datetime',
          dateTimeLabelFormats: { // don't display the dummy year
            day: '%e/%b',
            week: '%e/%b',
            month: '%b/%y',
            year: '%Y'
          },
          title: {
              text: 'Date'
          }
        },
        yAxis: {
          title: {
              text: 'Value (£)'
          },
        },
        tooltip: {
            headerFormat: '<b>{series.name}</b><br>',
            pointFormat: '{point.x:%e/%b/%y}: {point.y:.2f} m'
        },
        series: [
          {
            name: 'PnL',
            type: 'spline',
            data: this.chartData.map(x => {
              const d = new Date(x.date);
              console.log([Date.UTC(d.getFullYear(), d.getMonth(), d.getDay()), x.pnl]);
              return [Date.UTC(d.getFullYear(), d.getMonth(), d.getDay()), x.pnl]}) 
          }
        ]
      });
    }); 
  }
  }

  chart: Chart = new Chart();
  chartData: IChartPoint[] = [];
  subscription: Subscription = new Subscription;

  constructor(private commoditiesService: CommoditiesService) {}

  ngOnInit(): void {
 
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
