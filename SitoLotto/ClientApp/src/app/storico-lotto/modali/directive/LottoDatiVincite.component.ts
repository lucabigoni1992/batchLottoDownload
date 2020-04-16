
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericLottoDataService,  lottoDetailesArr } from '../../service/GenericLottoData.service';
import { tap } from 'rxjs/operators';
import { ProgressStatus, ProgressStatusEnum } from '../../../models/progress-status.model';
import { UploadDownloadService } from '../../../services/gestioneFile/upload-download.service';
import { ServiceSettings } from '../../../services/ServiceConst';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
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
    selector: 'app-LottoDatiVincite-component',
    templateUrl: '../template/LottoDatiVincite-template.html'
})



export class LottoDatiVinciteComponent implements OnInit {

    @Input() fromParent: any;
    private inDataItem: any;
    public LottoVincite: Object;
    public loading: boolean;
    private view: GridDataResult = null;

    constructor(
        public activeModal: NgbActiveModal,
        private dataService: kendoGridDataService) { }


    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" + this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.loading = true;
        this. inDataItem = this.fromParent.dataitem; 
        this.caricaDettagliLotto(this.inDataItem.id);
    }



    closeModal() {
        this.activeModal.close();
    }
    //griglia

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
        group: []
    };
    public columns: ColumnSetting[] = [

        
        {
            field: 'nEstrazione',
            title: 'nEstrazione',
            type: 'text',
            width: '150px'
        },
        {
            field: 'anno',
            format: '{0:0}',
            title: 'anno',
            type: 'numeric',
            width: '120px'
        },
        {
            field: 'enumTipoVincita',
            title: 'enumTipoVincita',
            format: '{0:0}',
            type: 'numeric',
            width: '175px'
        },
        {
            field: 'valore',
            title: 'valore',
            type: 'text',
            width: '200px'
        },
        {
            field: 'vincitori',
            title: 'vincitori',
            type: 'text',
            width: '200px'
        },
        {
            field: 'premio',
            title: 'premio',
            type: 'text',
            width: '200px'
        }
    ];

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
                this.view = data;
            });
    }
    //componenti
    //Download button

    public percentage: number;
    public showProgress: boolean;
    public showDownloadError: boolean;

    public downloadStatus(event: ProgressStatus) {
        switch (event.status) {
            case ProgressStatusEnum.START:
                this.showDownloadError = false;
                break;
            case ProgressStatusEnum.IN_PROGRESS:
                this.showProgress = true;
                this.percentage = event.percentage;
                break;
            case ProgressStatusEnum.COMPLETE:
                this.showProgress = false;
                break;
            case ProgressStatusEnum.ERROR:
                this.showProgress = false;
                this.showDownloadError = true;
                break;
        }
    }
}
