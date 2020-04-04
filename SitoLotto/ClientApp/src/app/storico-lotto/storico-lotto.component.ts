import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { State, process } from '@progress/kendo-data-query';
import { CallRest } from '../Access/CallRest.services'

@Component({
    selector: 'app-storico-lotto-component',
    templateUrl: './storico-lotto.component.html',
})

export class StoricoLottoComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    };
    public title = 'Hello World!';

    public onButtonClick() {
        this.title = 'Hello from Kendo UI!';
        this.showConfig();
    }

    public Lotto: Object;
    public gridData: any;

    showConfig() {
        CallRest.CallRest_Lotto_Get().subscribe(result => {
            this.gridData = result;
        }, error => console.error(error));
    }
    showConfigpost(State) {
        CallRest.CallRest_Lotto_Get_state(State).subscribe(result => {
            this.gridData = result;
        }, error => console.error(error));
    }


    public gridState: State = {
        sort: [],
        skip: 0,
        take: 10
    };

    public onStateChange(state: State) {
        this.showConfigpost(state);
        this.gridState = state;
        this.gridData = CallRest["CallRest_Lotto_Get"];
    }
}


