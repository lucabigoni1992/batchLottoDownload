import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';

import { GridDataResult } from '@progress/kendo-angular-grid';
import {
    State,
    toDataSourceRequestString,
    translateDataSourceResultGroups,
    DataSourceRequestState,
} from '@progress/kendo-data-query';

@Injectable()
export class DataService extends BehaviorSubject<GridDataResult> {
    public loading: boolean;
    constructor(private http: HttpClient) {
        super(null);
    }

    private BASE_URL = 'api/Lotto/';
    private data: GridDataResult = null;


    public read(state?: any) {
        //if (this.data) {
        //    return super.next(this.data);
        //}

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

    public fetch(state?:any, dataItem?: any,action: string = ''): Observable<any> {
        this.loading = true;
        switch (action) {
            case '': {
                //  const queryStr = `${toDataSourceRequestString(this.state)}`;
                //  const hasGroups = this.state.group && this.state.group.length;
           //     var test =  `${toDataSourceRequestString(state)}`;
                return this.http.get(this.BASE_URL + (state ? JSON.stringify(state) : ''))
                    .pipe(                    //
                        map(response => (<GridDataResult>{
                            data: response['results'],
                            total: parseInt(response['count'], 10)
                        })),
                        tap(() => this.loading = false)
                );
            }
            case 'create': {
                return this.http.post(`${this.BASE_URL}`, dataItem);
            }
            case 'edit': {
                return this.http.put(`${this.BASE_URL}/${dataItem.blogId}`, dataItem);
            }
            case 'delete': {
                const options = {
                    headers: {},
                    body: dataItem,
                };

                return this.http.delete(`${this.BASE_URL}/${dataItem.blogId}`, options);
            }
        }
    }

    public save(dataItem: any, isNew?: boolean) {
        if (isNew) {
            const newBlog = { Url: dataItem.url };
            this.fetch(newBlog, 'create').subscribe(() => this.read(), () => this.read());
        } else {
            this.fetch(dataItem, 'edit').subscribe(() => this.read(), () => this.read());
        }
    }

    public delete(dataItem: any) {
        this.fetch(dataItem, 'delete').subscribe(() => this.read(), () => this.read());
    }
}
