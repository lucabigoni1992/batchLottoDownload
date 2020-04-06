
import { Component, Inject, OnInit } from '@angular/core';
import { State, process } from '@progress/kendo-data-query';
import { CallRest } from '../Access/CallRest.services';
import { DataService } from '../storico-lotto/data.service';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-storico-lotto-component',
    templateUrl: './storico-lotto.component.html',
})

export class StoricoLottoComponent implements OnInit {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    };
    public title = 'Hello World!';

    public onButtonClick() {
        this.title = 'Hello from Kendo UI!';
    }
    constructor(private dataService: DataService) {

    }
    public view: Observable<any>;
    public formGroup: FormGroup;
    public Lotto: Object;
    public gridData: any;

    public ngOnInit(): void {
        this.view = this.dataService;
        this.dataService.read();
    }



    public gridState: State = {
        sort: [],
        skip: 0,
        take: 10
    };

    public onStateChange(state: State) {
        this.dataService.read(state);
        this.gridState = state;
    }
}


