import { Component, AfterViewInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ServiceSettings } from '../services/ServiceConst';

@Component({
    selector: 'app-testModali-component',
    templateUrl: './testModali.template.html'
})
export class testModaliComponent implements AfterViewInit {
    public currentCount = 0;
    public loading = false;
    public typeProgress = 1;
    public urlApi: string = ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalleDetailes;
    ngAfterViewInit() {

    }

    public incrementCounter(type: number) {
        this.currentCount = 1;
        this.typeProgress = type;
        this.partiCaricamento();
    }


    public partiCaricamento() {
        this.loading = true;
        setTimeout(() => {
            if (this.currentCount == 100) {
                this.loading = false;
                return;
            }
            this.currentCount++;
            this.partiCaricamento();
        }, 100)
    }
}
