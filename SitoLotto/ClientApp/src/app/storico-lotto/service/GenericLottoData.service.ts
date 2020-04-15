import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, throwError, Observable } from 'rxjs';
import { map, tap, catchError, retry } from 'rxjs/operators';
import { conSolution } from '../../../main';


export class lottoDetailes {
    id: number;
    nPalla: number;
    tipoPalla: number;
}
export class lottoDetailesArr {
    Palle: lottoDetailes[] ;
}

@Injectable()
export class GenericLottoDataService extends BehaviorSubject<any> {
    public loading: boolean;
    constructor(private http: HttpClient) {
        super(null);
    }

    private BASE_URL = conSolution.BASE_URL_API_Lotto_Detailes;
    private data: lottoDetailesArr = null;


    public readDetailes(id: number, dataIn: lottoDetailesArr) {
        this.loading = true;
        this.readDetailesCall(id)
            .pipe(
                tap(data => {
                    dataIn = data;
                })
            )
            .subscribe(data => {
                super.next(data);
                this.loading = false;
            });
    }
    public readDetailesCall(id: number): Observable<lottoDetailesArr> {
        return this.http.get(this.BASE_URL.replace('{id}', (id ? id.toString() : '')))
            .pipe(
                map(response =>
                    (<lottoDetailesArr>{
                        Palle: response
                    })),
                retry(3), // retry a failed request up to 3 times
                catchError(this.handleError));
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
