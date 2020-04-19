
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { ProgressStatus, ProgressStatusEnum } from '../../../models/progress-status.model';
import { ServiceSettings } from '../../../services/ServiceConst';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, GroupDescriptor, process } from '@progress/kendo-data-query';

import { kendoGridDataService } from '../../service/kendoGridData.service';
import { interval, Subscription } from 'rxjs';


@Component({
    selector: 'app-LottoDatiStatistics',
    templateUrl: '../template/LottoDatiVincite-template.html'
})



export class LottoDatiStatisticsComponent implements OnInit {

    @Input() fromParent: any;
    public loading: number;
    public viewQuote: any;
    public viewBalls: any;
    constructor(
        public activeModal: NgbActiveModal,
        private dataService: kendoGridDataService) { }


    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" + this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.loading = 0;
        this.caricaStatisticsLotto();
    }

    closeModal() {
        this.activeModal.close();
    }
    //componenti visivi griglia
   

    //funzioni

    caricaStatisticsLotto() {
        this.loading = 0;
        this.dataService.readStatisticsBalls()
            .pipe(
                tap(data => {
                    //  this.view = data;
                })
            )
            .subscribe(data => {
                this.loading = this.loading + 1;
                this.viewBalls = data;
            });
        this.dataService.readStatisticsQuote()
            .pipe(
                tap(data => {
                    //  this.view = data;
                })
            )
            .subscribe(data => {
                this.loading = this.loading + 1;
                this.viewQuote = data;
            });
    }
}
