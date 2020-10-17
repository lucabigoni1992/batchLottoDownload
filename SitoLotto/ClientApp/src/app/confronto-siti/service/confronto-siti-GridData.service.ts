import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { ServiceSettings } from '../../services/ServiceConst';
import { Data } from 'popper.js';



@Injectable()
export class ConfrontositiGridDataService extends BehaviorSubject<any> {

    constructor(private http: HttpClient) {
        super(null);
    }

    public read(): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Changed_WebPage_GetAllSite)
            .pipe(
                map(response => (<any>{
                    data: response['results'],
                })),
                tap()
            );
    }

}
