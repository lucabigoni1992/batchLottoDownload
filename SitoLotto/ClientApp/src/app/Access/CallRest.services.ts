import { Component, Inject, Injectable} from '@angular/core';
import { HttpClient, HttpHeaders, HttpXhrBackend } from '@angular/common/http';

const httpClient = new HttpClient(new HttpXhrBackend({ build: () => new XMLHttpRequest() }));
const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/xml',
        'Authorization': 'jwt-token'
    })
};

@Injectable()
export class CallRest {

    constructor(private  http: HttpClient) { }
    configUrl = 'assets/config.json';
    public static CallRest_Lotto_Get() {

        return httpClient.get<Object>('api/Lotto')
    }
    public static CallRest_Lotto_Get_state(state) {

        return httpClient.get<Object>('api/Lotto/' + JSON.stringify(state))
    }

}
