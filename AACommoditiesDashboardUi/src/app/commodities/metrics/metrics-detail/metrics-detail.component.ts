import { Component, Input, OnInit } from '@angular/core';
import { IModelCommodity } from '../../api/models/model-commodity';

@Component({
  selector: 'app-metrics-detail',
  templateUrl: './metrics-detail.component.html',
  styleUrls: ['./metrics-detail.component.css']
})
export class MetricsDetailComponent implements OnInit {
  @Input()
  model?: IModelCommodity;
  constructor() { }

  ngOnInit(): void {
  }

}
