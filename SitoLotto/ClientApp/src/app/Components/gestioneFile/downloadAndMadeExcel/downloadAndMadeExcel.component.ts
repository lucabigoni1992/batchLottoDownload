import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpEventType } from '@angular/common/http';
import { ProgressStatus, ProgressStatusEnum } from 'src/app/models/progress-status.model';
import { UploadDownloadService } from '../../../services/gestioneFile/upload-download.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertErrorComponent, AlertError } from '../../Alert/Error/alertError.component';

@Component({
    selector: 'app-downloadAndMadeExcel',
    templateUrl: 'downloadAndMadeExcel.template.html'
})

export class DownloadAndMadeExcelModule {
    @Input() public urlApi: string;
    @Input() public fileName: string;
    @Input() public id: number;
    @Output() public currStatus: ProgressStatus;
    constructor(private service: UploadDownloadService, private modalService: NgbModal) {
        this.currStatus = { status: ProgressStatusEnum.START };
    }

    public download() {
        this.currStatus = { status: ProgressStatusEnum.IN_PROGRESS, percentage: -1 };
        this.service.madeAndDownloadExcelFile(this.urlApi, this.fileName, this.id).subscribe(
            data => {
                switch (data.type) {
                    case HttpEventType.DownloadProgress:
                        this.currStatus = { status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded / data.total) * 100) };
                        break;
                    case HttpEventType.Response:
                        this.currStatus = { status: ProgressStatusEnum.COMPLETE };
                        const downloadedFile = new Blob([data.body], { type: data.body.type });
                        const a = document.createElement('a');
                        a.setAttribute('style', 'display:none;');
                        document.body.appendChild(a);
                        a.download = this.fileName;
                        a.href = URL.createObjectURL(downloadedFile);
                        a.target = '_blank';
                        a.click();
                        document.body.removeChild(a);
                        break;
                }
            },
            error => {
                var a: string;
                this.currStatus = { status: ProgressStatusEnum.ERROR };
                var modalRef: NgbModalRef = this.modalService.open(AlertErrorComponent,
                    {
                        scrollable: false,
                        size: 'l'
                    });
                modalRef.componentInstance.inpVal = <AlertError>{
                    title: error.name,
                    text: '<strong>Error status</strong>: ' + error.status
                        + '<br />'
                        + '<strong>Url</strong>         : ' + error.url.toString().replace(/\//g, '/ ') 
                };
                modalRef.result.then((result) => {
                    console.log(result);
                }, (reason) => {
                });
            }
        );
    }
}
