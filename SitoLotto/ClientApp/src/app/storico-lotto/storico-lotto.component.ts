import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { State, process } from '@progress/kendo-data-query';

@Component({
    selector: 'app-storico-lotto-component',
    templateUrl: './storico-lotto.component.html'
})

export class StoricoLottoComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    };
    public title = 'Hello World!';

    public onButtonClick() {
        this.title = 'Hello from Kendo UI!';
    }

    public Lotto: Object;
    public gridData: any;
     @Inject('BASE_URL') public baseUrl: string;
    constructor(private http2: HttpClient) {
        this.http2.get<Object>( 'api/Lotto').subscribe(result => {
            this.gridData = result;
        }, error => console.error(error));}


    public gridState: State = {
        sort: [],
        skip: 0,
        take: 10
    };

    public onStateChange(state: State) {
        this.gridState = state;
        this.http2.get<Object>( 'api/Lotto').subscribe(result => {
            this.gridData = result;
        }, error => console.error(error));
    }
}


