
import { Component,  OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';
import { MyBootstrapModalComponent } from './modali/directive/storico-lotto.component';
import { kendoGridDataService } from './service/kendoGridData.service';

interface ColumnSetting {
    field: string,
    title: string,
    format?: string,
    type: 'text' | 'numeric' | 'boolean' | 'date',
    width?: string,
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
    constructor(private dataService: kendoGridDataService, private modalService: NgbModal) {

    }
    public view: kendoGridDataService;
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
            field: 'nEstrazione',
            format: '{0:0}',
            title: 'Estrazione n°',
            type: 'numeric',
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
    }

    //funzioni



    //modali
    public modaleNumeri(dataitem) {
        var modalRef: NgbModalRef = this.modalService.open(MyBootstrapModalComponent,
            {
                scrollable: false,
                size: 'xl'
            });
        modalRef.componentInstance.fromParent = {
            dataitem: dataitem
        };
        modalRef.result.then((result) => {
            console.log(result);
        }, (reason) => {
        });

    }
}
