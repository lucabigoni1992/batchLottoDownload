import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, throwError } from 'rxjs';
import { conSolution } from '../../../../main';
import { map, tap, catchError, retry } from 'rxjs/operators';

export interface lottoDetailes {
    id: number;
    nPalla: number;
    tipoPalla: number;
}

@Injectable()
export class GenericLottoDataService extends BehaviorSubject<any> {
    public loading: boolean;
    constructor(private http: HttpClient) {
        super(null);
    }

    private BASE_URL = conSolution.BASE_URL_API_Lotto_Detailes;


    public readDetailes(id: number, dataIn: lottoDetailes) {
        this.loading = true;
        return this.http.get(this.BASE_URL.replace('{id}', (id ? id.toString() : '')))

            .pipe(
                map(response =>
                    (<lottoDetailes>{
                        id: response[0].id,
                        nPalla: response[0].nPalla,
                        tipoPalla: response[0].tipoPalla
                    })),
                map(response => response),
                retry(3), // retry a failed request up to 3 times
                catchError(this.handleError))
            .subscribe(data => {
                dataIn = data;
                this.loading = false;
            });
    };
    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(
                `Backend returned code ${error.status}, ` +
                `body was: ${error.error}`);
        }
        // return an observable with a user-facing error message
        return throwError(
            'Something bad happened; please try again later.');
    };
}
