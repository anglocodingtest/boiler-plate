import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

import { of, Observable, BehaviorSubject } from 'rxjs';
import { ITradeAction } from './models/trade-action';
import { IModelCommodity } from './models/model-commodity';
import { IChartPoint } from './models/chart-point';

@Injectable({
    providedIn: 'root'
})
export class CommoditiesService {
    private baseUrl = environment.commoditiesApi;

    private commodityModels = new BehaviorSubject<IModelCommodity[]>([]);

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Access-Control-Max-Age': '86400'
        })
    };

    constructor(private http: HttpClient) { }

    init(): Observable<IModelCommodity[]> {
        var obs =this.http.get<IModelCommodity[]>(this.baseUrl, this.httpOptions)
        .pipe(
            tap(_ => this.log('fetched ModelCommodities')),
            catchError(this.handleError<IModelCommodity[]>('getCommodityModels', []))
        );
        obs.subscribe(x => this.commodityModels.next(x));
        return obs;
    }

    getTradeActions(): Observable<ITradeAction[]> {
        return this.http.get<ITradeAction[]>(this.baseUrl + '/tradeactions', this.httpOptions)
            .pipe(
                tap(_ => this.log('fetched TradeActions')),
                catchError(this.handleError<ITradeAction[]>('getTradeActions', []))
            );
    }

    getChartData(id: number): Observable<IChartPoint[]> {
        return this.http.get<IChartPoint[]>(this.baseUrl + '/chartdata/' + id, this.httpOptions)
            .pipe(
                tap(_ => this.log('fetched ChartData')),
                catchError(this.handleError<IChartPoint[]>('getChartData', []))
            );
    }

    getModelCommodities(): Observable<IModelCommodity[]> {
        return this.commodityModels.asObservable();
    }

    private log(message: string) {
        console.log(`CommoditiesService: ${message}`);
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

}