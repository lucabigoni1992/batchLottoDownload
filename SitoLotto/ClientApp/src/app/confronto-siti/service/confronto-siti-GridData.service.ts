import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { ServiceSettings } from '../../services/ServiceConst';
import { Data } from 'popper.js';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';

const headers = new HttpHeaders()
    .set("Content-Type", "application/json");
@Injectable()
export class ConfrontositiGridDataService extends BehaviorSubject<any> {


    constructor(private http: HttpClient) {
        super(null);
    }

    public read(): Observable<any> {
        return this.http.get(ServiceSettings.BASE_URL_API_Changed_WebPage_GetAllSite)
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['results'],
                    total: parseInt(response['count'], 10)
                })),
                tap()
            );
    }
    public add(data: FormGroup): Observable<any> {

        return this.http.get(ServiceSettings.BASE_URL_API_Changed_WebPage_AddSite.replace("{SiteData}", JSON.stringify(data.value)))
            .pipe(
                map(response => (<any>{
                    data: response['results'],
                })),
                tap()
            );
    }
    public azione(azione: number, url: string): Observable<any> {

        return this.http.put(ServiceSettings.BASE_URL_API_Changed_WebPage_ChangeSite,
            JSON.stringify({ Action: azione, url: url }),
            { headers })
            .pipe(map(response => (<any>{
              //  data: response['results'],
            })),
                tap()
            );
    }

}
