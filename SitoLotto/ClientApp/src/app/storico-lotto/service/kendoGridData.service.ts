import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';

import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { ServiceSettings } from '../../services/ServiceConst';


@Injectable()
export class kendoGridDataService extends BehaviorSubject<any> {
    public loading: boolean;
    constructor(private http: HttpClient) {
        super(null);
    }

    private data: GridDataResult = null;


    public read(state?: any) {
        this.fetch(state)
            .pipe(
                tap(data => {
                    this.data = data;
                })
            )
            .subscribe(data => {
                super.next(data);
            });
    }
    public readDetailes(id?: any) {
        this.fetch(id, "Detailes")
            .pipe(
                tap(data => {
                    this.data = data;
                })
            )
            .subscribe(data => {
                super.next(data);
            });
    }

    private fetch(value?: any, action: string = ''): Observable<any> {
        this.loading = true;
        switch (action) {
            case '': {
                return this.http.get(ServiceSettings.BASE_URL_API_Lotto.replace('{KendoData}', (value ? JSON.stringify(value) : '')))
                    .pipe(
                        map(response => (<GridDataResult>{
                            data: response['results'],
                            total: parseInt(response['count'], 10)
                        })),
                        tap(() => this.loading = false)
                    );
            }
            case 'Detailes': {
                return this.http.get(ServiceSettings.BASE_URL_API_Lotto_Detailes_Quote.replace('{id}', (value ? JSON.stringify(value) : '')))
                    .pipe(
                        map(response => (<GridDataResult>{
                            data: response['results'],
                            total: parseInt(response['count'], 10)
                        })),
                        tap(() => this.loading = false)
                    );
            }
            //case 'create': {
            //    return this.http.post(`${this.BASE_URL}`, dataItem);
            //}
            //case 'edit': {
            //    return this.http.put(`${this.BASE_URL}/${dataItem.blogId}`, dataItem);
            //}
            //case 'delete': {
            //    const options = {
            //        headers: {},
            //        body: dataItem,
            //    };

            //    return this.http.delete(`${this.BASE_URL}/${dataItem.blogId}`, options);
            //}
        }
    }

    //public save(dataItem: any, isNew?: boolean) {
    //    if (isNew) {
    //        const newBlog = { Url: dataItem.url };
    //        this.fetch(newBlog, 'create').subscribe(() => this.read(), () => this.read());
    //    } else {
    //        this.fetch(dataItem, 'edit').subscribe(() => this.read(), () => this.read());
    //    }
    //}

    //public delete(dataItem: any) {
    //    this.fetch(dataItem, 'delete').subscribe(() => this.read(), () => this.read());
    //}
}
