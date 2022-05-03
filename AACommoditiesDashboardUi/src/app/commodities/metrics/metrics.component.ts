import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommoditiesService } from '../api/commodities.service';
import { IModelCommodity } from '../api/models/model-commodity';

@Component({
  selector: 'app-metrics',
  templateUrl: './metrics.component.html',
  styleUrls: ['./metrics.component.css']
})
export class MetricsComponent implements OnInit, OnDestroy {
  private _selectedModelName!: string;
  private _selectedCommodityName!: string;
  subscription: Subscription = new Subscription;
  selectedModel?: IModelCommodity;
  modelNames: string[] = [];
  commodityNames: string[] = [];
  models: IModelCommodity[] = [];

  constructor(private commoditiesService: CommoditiesService) {}

  ngOnInit(): void {
    this.subscription = this.commoditiesService.init()
    .subscribe((models: IModelCommodity[]) => {
      this.models = models;
      this.modelNames = Array.from(new Set(models.map(item => item.modelName)));
      this.commodityNames = Array.from(new Set(models.map(item => item.commodityName)));
      this._selectedModelName = this.modelNames[0];
      this._selectedCommodityName = this.commodityNames[0];
      this.selectedModel = this.findModel();
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  modelNameChange(event: string) 
  {
    this._selectedModelName = event;
    this.selectedModel = this.findModel();
  }

  commodityNameChange(event: string) 
  {
    this._selectedCommodityName = event;
    this.selectedModel = this.findModel();
  }

  private findModel() 
  {
    return this.models.find(x => x.modelName === this._selectedModelName && x.commodityName === this._selectedCommodityName);
  }
}
