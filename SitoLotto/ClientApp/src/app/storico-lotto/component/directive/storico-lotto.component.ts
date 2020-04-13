
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericLottoDataService, lottoDetailes, lottoDetailesArr } from '../service/GenericLottoData.service';
import { tap } from 'rxjs/operators';

@Component({
    selector: 'app-mybootstrap-modal-component',
    templateUrl: '../template/Estrazione-template.html'
})



export class MyBootstrapModalComponent implements OnInit {

    @Input() fromParent: any;

    constructor(
        public activeModal: NgbActiveModal,
        public dataService: GenericLottoDataService) { }

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
            });
    }

    closeModal(sendData) {
        console.log("closeModal" + this.fromParent);
    }

}
