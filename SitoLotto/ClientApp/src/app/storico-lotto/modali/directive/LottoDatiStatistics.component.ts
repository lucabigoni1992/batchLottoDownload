
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

    public banksData = [
        { name: "JP Morgan", pre: 116, post: 64 },
        { name: "HSBC", pre: 165, post: 85 },
        { name: "Credit Suisse", pre: 215, post: 97 },
        { name: "Goldman Sachs", pre: 75, post: 27 },
        { name: "Morgan Stanley", pre: 100, post: 16 },
        { name: "Societe Generale", pre: 49, post: 26 },
        { name: "UBS", pre: 80, post: 35 },
        { name: "BNP Paribas", pre: 116, post: 32 },
        { name: "Unicredit", pre: 108, post: 26 },
        { name: "Credit Agricole", pre: 90, post: 17 },
        { name: "Deutsche Bank", pre: 67, post: 10 },
        { name: "Barclays", pre: 76, post: 7 },
        { name: "Citigroup", pre: 91, post: 19 },
        { name: "RBS", pre: 255, post: 5 },
        { name: "RBS1", pre: 255, post: 5 },
        { name: "RBS2", pre: 255, post: 55 },
        { name: "RBS3", pre: 255, post: 5 },
        { name: "RBS4", pre: 255, post: 5 },
        { name: "RBS5", pre: 255, post: 5 },
        { name: "RBS6", pre: 255, post: 5 }
    ];
}
