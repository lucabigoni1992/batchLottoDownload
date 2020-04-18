
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { ProgressStatus, ProgressStatusEnum } from '../../../models/progress-status.model';
import { ServiceSettings } from '../../../services/ServiceConst';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, GroupDescriptor, process } from '@progress/kendo-data-query';

import { kendoGridDataService } from '../../service/kendoGridData.service';
import { interval, Subscription } from 'rxjs';


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
    selector: 'app-LottoDatiVincite',
    templateUrl: '../template/LottoDatiVincite-template.html'
})



export class LottoDatiVinciteComponent implements OnInit {

    @Input() fromParent: any;
    private inDataItem: any;
    public LottoVincite: Object;
    public loading: boolean;
    private view: GridDataResult = null;
    public urlApi: string = ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalleDetailes;
    constructor(
        public activeModal: NgbActiveModal,
        private dataService: kendoGridDataService) { }


    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" + this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.loading = true;
        this.inDataItem = this.fromParent.dataitem;
        this.caricaDettagliLotto(this.inDataItem.id);
    }

    closeModal() {
        this.activeModal.close();
    }
    //griglia
    public groupable = true;
    public groups: GroupDescriptor[] = [{ field: 'enumTipoVincita' }];
    public pageable: pageable = {
        buttonCount: 10,
        info: false,
        type: 'numeric',
        pageSizes: false,
        previousNext: false
    };

    public gridState: State = {
        sort: [],
        skip: 0,
        take: 20,
        filter: {
            logic: 'and',
            filters: []
        },
        group: this.groups
    };
    public columns: ColumnSetting[] = [
        {
            field: 'enumTipoVincita',
            title: 'Tipo Estrazione',
            format: '{0:0}',
            type: 'numeric',
            width: '175px'
        },
        {
            field: 'valore',
            title: 'Valore',
            type: 'text',
            width: '200px'
        },
        {
            field: 'vincitori',
            title: 'N° Vincitori',
            type: 'text',
            width: '200px'
        },
        {
            field: 'premio',
            title: 'Premio',
            type: 'text',
            width: '200px'
        }
    ];

    public groupChange(groups: GroupDescriptor[]): void {
        this.groups = groups;
        console.log("groupChange" + groups);
        this.caricaDettagliLotto(this.inDataItem.id);
    }
    public valueChange(value: any): void {
        console.log('valueChange', value);
        if (!value) {
            this.groupable = false;
            this.groupChange(<GroupDescriptor[]>[{}]);
        }
        else {
            this.groupable = true;
            this.groupChange(<GroupDescriptor[]>[{ field: value.field }]);
        }
    }
    //funzioni

    caricaDettagliLotto(id: number) {
        this.loading = true;
        this.dataService.readDetailes(id)
            .pipe(
                tap(data => {
                    this.view = data;
                })
            )
            .subscribe(data => {
                this.loading = false;
                //     this.view = data;
                this.view = process(this.view.data, { group: this.groups });

            });
    }


}
