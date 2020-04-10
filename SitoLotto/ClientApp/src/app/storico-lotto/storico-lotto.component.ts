
import { Component, Inject, OnInit, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { State, process } from '@progress/kendo-data-query';
import { DataService } from '../storico-lotto/data.service';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';
import { Container } from '@angular/compiler/src/i18n/i18n_ast';
import { MyBootstrapModalComponent } from './component/directive/storico-lotto.component';

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
    type: ' text' | 'numeric' | 'boolean' | 'date',
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
    constructor(private dataService: DataService, private modalService: NgbModal) {

    }
    public view: DataService;
    public formGroup: FormGroup;
    public Lotto: Object;
    public gridData: GridDataResult;

    public ngOnInit(): void {
        this.view = this.dataService;
        this.view.loading = true;
        this.view.read();
    }
    public pageable: pageable = {
        buttonCount: 5,
        info: true,
        type: 'numeric',
        pageSizes: true,
        previousNext: true
    };

    public gridState: State = {
        sort: [{ dir: "desc", field: "data" }],
        skip: 0,
        take: 10,
        filter: {
            logic: 'and',
            filters: []
        },
        group: []
    };
    public columns: ColumnSetting[] = [

        {
            field: 'azioni',
            title: 'Azioni',
            type: 'text',
            width: '75px'
        },
        {
            field: 'data',
            title: 'Data estrazione',
            type: 'text',
            width: '150px'
        },
        {
            field: 'id',
            title: 'Estrazione n°',
            type: 'text',
            width: '120px'
        },
        {
            field: 'nVincitori',
            title: 'N°vincitori Montepremi',
            format: '{0:0}',
            type: 'numeric',
            width: '175px'
        },
        {
            field: 'premio6Punti',
            title: 'Montepremi',
            type: 'text',
            width: '200px'
        }
    ];

    public onStateChange(state: State) {
        this.dataService.read(state);
        this.gridState = state;
        //this.view = this.dataService;
        //this.view.loading = true;
        //this.view.read();
    }

    //funzioni

    public GetOnlyNumberOfExtraction = function (id) {
        var s_id = id.toString().substring(4);
        for (var i = 0; i < s_id.length; i++) {
            if (s_id.charAt(i) != '0')
                return s_id.substring(i);
        }
    }


    //modali
    public modaleNumeri(dataitem, str, content) {
        var modalRef: NgbModalRef = this.modalService.open(MyBootstrapModalComponent,
            {
                scrollable: false,
                size: 'lg'
            });
        modalRef.componentInstance.fromParent = {
            dataitem: dataitem,
            str1: str,
            str2: 'This Can be anything'
        };
        modalRef.result.then((result) => {
            console.log(result);
        }, (reason) => {
        });

    }
}
