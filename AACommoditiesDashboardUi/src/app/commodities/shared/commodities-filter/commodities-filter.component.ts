import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-commodities-filter',
  templateUrl: './commodities-filter.component.html',
  styleUrls: ['./commodities-filter.component.css']
})
export class CommoditiesFilterComponent implements OnInit {
  @Input()
  modelNames: string[] = [];
  @Input()
  commodityNames: string[] = [];
  
  @Output() commodityNameChanged = new EventEmitter<string>();

  @Output() modelNameChanged = new EventEmitter<string>();
  constructor() { }

  ngOnInit(): void {
  }

  onModelNameChanged(value:string){
    this.modelNameChanged.emit(value);
  }

  onCommodityNameChanged(value:string){
    this.commodityNameChanged.emit(value);
  }
}
