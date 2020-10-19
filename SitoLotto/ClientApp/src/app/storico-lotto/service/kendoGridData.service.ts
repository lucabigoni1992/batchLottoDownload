/// <reference path="../storico-lotto.component.ts" />
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';

import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { ServiceSettings } from '../../services/ServiceConst';
import { State } from '@progress/kendo-data-query';



@Injectable()
export class kendoGridDataService extends BehaviorSubject<any> {

    constructor(private http: HttpClient) {
        super(null);
    }

    public read(state?: State): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Lotto.replace('{KendoData}', (state ? JSON.stringify(state) : '')))
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['results'],
                    total: parseInt(response['count'], 10)
                })),
                tap()
            );
    }
    public readDetailes(id: number): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Lotto_Detailes_Quote.replace('{id}', id.toString()))
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['results'],
                    total: parseInt(response['count'], 10)
                })),
                tap(() => { })
            );
    }
    public readStatisticsQuote(): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Lotto_Detailes_Statistics_Quote)
            .pipe(
                map(response => (<any>{
                    data: response
                })),
                tap(() => { })
            );
    }
    public readStatisticsBalls(): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Lotto_Detailes_Statistics_Balls)
            .pipe(
                map(response => (<any>{
                    data: response
                })),
                tap(() => { })
            );
    }
}
