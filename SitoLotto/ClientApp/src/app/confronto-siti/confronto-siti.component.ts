
import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';
import { tap } from 'rxjs/internal/operators/tap';
import { BehaviorSubject } from 'rxjs';
import { ConfrontositiGridData } from './service/confronto-siti-GridData.service';

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
    selector: 'app-confronto-siti.component',
    templateUrl: './confronto-siti.component.html',
})


export class ConfrontoSitiComponent extends BehaviorSubject<any> implements OnInit {

    constructor(private dataService: ConfrontositiGridData, private modalService: NgbModal) {
        super(null);
    }
    public loading: boolean;
    public view: GridDataResult = null;
    public formGroup: FormGroup;

    public ngOnInit(): void {
        this.loading = true;
        this.ricarica();
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
        this.ricarica(state);
        this.gridState = state;
    }

    //funzioni

    private ricarica = function (state?: State) {
        this.loading = true;
        this.dataService.read(state)
            .pipe(
                tap(data => {
                    this.view = data;
                })
            )
            .subscribe(data => {
                this.loading = false;
            });
    }

    //modali



}
