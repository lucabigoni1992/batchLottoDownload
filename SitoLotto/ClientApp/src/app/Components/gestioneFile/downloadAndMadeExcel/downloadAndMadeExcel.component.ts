import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpEventType } from '@angular/common/http';
import { ProgressStatus, ProgressStatusEnum } from 'src/app/models/progress-status.model';
import { UploadDownloadService } from '../../../services/gestioneFile/upload-download.service';

@Component({
    selector: 'app-downloadAndMadeExcel',
    templateUrl: 'downloadAndMadeExcel.component.html'
})

export class DownloadAndMadeExcel {
    @Input() public urlApi: string;
    @Input() public fileName: string;
    @Input() public id: number;
    @Output() public downloadStatus: EventEmitter<ProgressStatus>;
    @Output() public currStatus: ProgressStatus;
    constructor(private service: UploadDownloadService) {
        this.downloadStatus = new EventEmitter<ProgressStatus>();
        this.currStatus = { status: ProgressStatusEnum.ERROR, percentage: 0 };
    }

    public download() {
        this.downloadStatus.emit({ status: ProgressStatusEnum.START });
        this.service.madeAndDownloadExcelFile(this.urlApi, this.fileName, this.id).subscribe(
            data => {
                switch (data.type) {
                    case HttpEventType.DownloadProgress:
                        this.currStatus = { status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded / data.total) * 100) };                    
                        this.downloadStatus.emit(this.currStatus );
                        break;
                    case HttpEventType.Response:
                        this.currStatus = { status: ProgressStatusEnum.COMPLETE };
                        this.downloadStatus.emit(this.currStatus);
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
                this.currStatus = { status: ProgressStatusEnum.ERROR };
                this.downloadStatus.emit(this.currStatus);
            }
        );
    }
}
