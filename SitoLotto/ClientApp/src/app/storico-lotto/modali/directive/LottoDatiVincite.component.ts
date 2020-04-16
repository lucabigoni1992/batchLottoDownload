
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

    constructor(
        public activeModal: NgbActiveModal,
        public id: GenericLottoDataService,
        private dataService: kendoGridDataService) { }

    private loading = false;
    private dataDetailes: lottoDetailesArr;
    public urlApi = ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalle;

    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" + this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.dataDetailes = new lottoDetailesArr();
        this.loading = true
        this.dataService.readDetailes(this.fromParent.id);
    }



    closeModal() {
        this.activeModal.close();

    }
    //griglia
    public LottoVincite: Object;
    public gridData: GridDataResult;
    public onStateChange(state: State) {
        this.dataService.read(state);
        this.gridState = state;
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
