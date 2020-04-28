
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericLottoDataService,  lottoDetailesArr } from '../../service/GenericLottoData.service';
import { tap } from 'rxjs/operators';
import { ProgressStatus, ProgressStatusEnum } from '../../../models/progress-status.model';
import { UploadDownloadService } from '../../../services/gestioneFile/upload-download.service';
import { ServiceSettings } from '../../../services/ServiceConst';

@Component({
    selector: 'app-LottoDatiEstrazione-component',
    templateUrl: '../template/LottoDatiEstrazione-template.html'
})



export class LottoDatiEstrazioneComponent implements OnInit {

    @Input() fromParent: any;

    constructor(
        public activeModal: NgbActiveModal,
        public dataService: GenericLottoDataService,
        private service: UploadDownloadService) { }

    public dataitem;
    private loading = false;
    public dataDetailes: lottoDetailesArr;
    public urlApi = ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalle;

    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" + this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.dataDetailes = new lottoDetailesArr();
        this.dataitem = this.fromParent.dataitem;
        this.loading = true
        this.dataService.readDetailesCall(this.fromParent.dataitem.id)
            .pipe(
                tap(data => data)
            )
            .subscribe(data => {
                this.dataDetailes = data;
                this.loading = false;
            }); 
    }



    closeModal() {
        this.activeModal.close();

    }


 
}
