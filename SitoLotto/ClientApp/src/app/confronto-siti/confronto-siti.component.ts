
import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';
import { tap } from 'rxjs/internal/operators/tap';
import { BehaviorSubject } from 'rxjs';
import { ConfrontositiGridDataService } from './service/confronto-siti-GridData.service';
import { ConfrontoSitiAddRecordComponent } from './modali/directive/confrontoSitiAddRecord.component';
import { NumericFilterCellComponent } from '@progress/kendo-angular-grid';

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

    constructor(private dataService: ConfrontositiGridDataService, private modalService: NgbModal) {
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
            field: 'Url',
            title: 'Url',
            type: 'text',
            width: '75px'
        },
        {
            field: 'Email',
            title: 'Email',
            type: 'text',
            width: '150px'
        },
        {
            field: 'Ore',
            title: 'Ore',
            type: 'numeric',
            width: '120px'
        },
        {
            field: 'Tag',
            title: 'Tag',
            type: 'text',
            width: '175px'
        },
        {
            field: 'Active',
            title: 'Active',
            type: 'boolean',
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

    public azioneSito(azione: number, dataitem: any) {
        this.loading = true;
        this.dataService.azione(azione, dataitem.Url)
            .pipe(
                tap(data => {
                  //  this.view = data;
                })
            )
            .subscribe(data => {
                this.ricarica(this.gridState);
                this.loading = false;
            });
    }


    //modali

    public showAddSite(dataitem) {
        var modalRef: NgbModalRef = this.modalService.open(ConfrontoSitiAddRecordComponent,
            {
                scrollable: true,
                size: 'xl'
            });
        modalRef.componentInstance.fromParent = {
            isNew: true,
            dataitem: dataitem
        };
        modalRef.result.then((result) => {
            this.ricarica(this.gridState);
            console.log(result);
        });
    }

}
