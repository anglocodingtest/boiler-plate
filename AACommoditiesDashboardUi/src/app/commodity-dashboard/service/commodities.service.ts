import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError, catchError, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { chartdetail } from '../models/chart-detail';
import { Commodity } from '../models/commodity';
import { CommoditySummary } from '../models/commodity-summary';
import { Model } from '../models/model';
import { TradeAction } from '../models/trade-actions';

@Injectable({
  providedIn: 'root'
})
export class CommoditiesService {

  private baseUrl = environment.commoditiesApi;

  commodities$ = this.http.get<Commodity[]>(this.baseUrl)
  .pipe(catchError(this.handleError)
  );

  models$ = this.http.get<Model[]>(this.baseUrl + '/models')
  .pipe(catchError(this.handleError)
  );
  
  commoditySummary$ = this.http.get<CommoditySummary[]>(this.baseUrl + '/summary')
  .pipe(catchError(this.handleError)
  );

  tradeActions$ = this.http.get<TradeAction[]>(this.baseUrl + '/tradeactions')
  .pipe(catchError(this.handleError)
  );

  constructor(private http: HttpClient) { }

  getChartDetail(commodityId: number, modelId: number,): Observable<chartdetail[]>{
    return this.http.get<chartdetail[]>(this.baseUrl + `/chartdetail/${commodityId}/${modelId}`)
    .pipe(catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse): Observable<never> {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned error: ${err.message}`;
    }
    console.error(err);
    return throwError(() => errorMessage);
  }
}
