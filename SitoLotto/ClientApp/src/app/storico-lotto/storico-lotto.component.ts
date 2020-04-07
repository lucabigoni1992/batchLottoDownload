
import { Component, Inject, OnInit } from '@angular/core';
import { State, process } from '@progress/kendo-data-query';
import { CallRest } from '../Access/CallRest.services';
import { DataService } from '../storico-lotto/data.service';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';

interface ColumnSetting {
    field: string;
    title: string;    
    format  ?: string;
    type: 'text' | 'numeric' | 'boolean' | 'date';
}

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
    public columns: ColumnSetting[] = [

        {
            field: 'azioni',
            title: 'Azioni',
            type: 'text'
        },
        {
            field: 'data',
            title: 'Data estrazione',
            type: 'text'
        },
        {
            field: 'id',
            title: 'Estrazione n°',
            type: 'text'
        },
        {
            field: 'nVincitori',
            title: 'N°vincitori Montepremi',
            format: '{0:0}',
            type: 'numeric'
        },
        {
            field: 'premio6Punti',
            title: 'Montepremi',
            type: 'text'
        }
    ];

    public onStateChange(state: State) {
        this.dataService.read(state);
        this.gridState = state;
    }
}


