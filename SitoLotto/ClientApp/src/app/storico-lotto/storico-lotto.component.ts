
import { Component, Inject, OnInit } from '@angular/core';
import { State, process } from '@progress/kendo-data-query';
import { CallRest } from '../Access/CallRest.services';
import { DataService } from '../storico-lotto/data.service';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { PageAction } from '@progress/kendo-angular-grid/dist/es2015/scrolling/scroller.service';

interface ColumnSetting {
    field: string;
    title: string;
    format?: string;
    type: 'text' | 'numeric' | 'boolean' | 'date';
    width?: string;
}
interface pageable {
    buttonCount: number,
    info: boolean,
    type:' text' | 'numeric' | 'boolean' | 'date',
    pageSizes: boolean,
    previousNext: boolean
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
    public pageable: pageable = {
        buttonCount: 5,
        info: true,
        type: 'numeric' ,
        pageSizes: true,
        previousNext: true
    };

    public gridState: State = {
        sort: [],
        skip: 0,
        take: 10
    };
    public columns: ColumnSetting[] = [

        {
            field: 'azioni',
            title: 'Azioni',
            type: 'text',
            width: '100px'
        },
        {
            field: 'data',
            title: 'Data estrazione',
            type: 'text',
            width: '100px'
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

    //funzioni

    public GetOnlyNumberOfExtraction = function (id) {
        var s_id = id.toString().substring(4);
        for (var i = 0; i < s_id.length; i++) {
            if (s_id.charAt(i) != '0')
                return s_id.substring(i);
        }
    }

}


