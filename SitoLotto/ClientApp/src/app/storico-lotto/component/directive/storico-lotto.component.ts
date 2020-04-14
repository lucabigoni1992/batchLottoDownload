
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericLottoDataService,  lottoDetailesArr } from '../service/GenericLottoData.service';
import { tap } from 'rxjs/operators';
import { ProgressStatus, ProgressStatusEnum } from '../../../models/progress-status.model';
import { UploadDownloadService } from '../../../services/upload-download.service';

@Component({
    selector: 'app-mybootstrap-modal-component',
    templateUrl: '../template/Estrazione-template.html'
})



export class MyBootstrapModalComponent implements OnInit {

    @Input() fromParent: any;

    constructor(
        public activeModal: NgbActiveModal,
        public dataService: GenericLottoDataService,
        private service: UploadDownloadService) { }

    private dataitem;
    private loading = false;
    private dataDetailes: lottoDetailesArr;


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
            }); this.getFiles();
    }

    public files: string[];
    public fileInDownload: string;
    public percentage: number;
    public showProgress: boolean;
    public showDownloadError: boolean;
    public showUploadError: boolean;


    private getFiles() {
        this.service.getFiles().subscribe(
            data => {
                this.files = data;
            }
        );
    }

    closeModal(sendData) {
        console.log("closeModal" + this.fromParent);
    }
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

    public uploadStatus(event: ProgressStatus) {
        switch (event.status) {
            case ProgressStatusEnum.START:
                this.showUploadError = false;
                break;
            case ProgressStatusEnum.IN_PROGRESS:
                this.showProgress = true;
                this.percentage = event.percentage;
                break;
            case ProgressStatusEnum.COMPLETE:
                this.showProgress = false;
                this.getFiles();
                break;
            case ProgressStatusEnum.ERROR:
                this.showProgress = false;
                this.showUploadError = true;
                break;
        }
    }

}
