
import { Component,  OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericLottoDataService, lottoDetailes } from '../service/GenericLottoData.service';

@Component({
    selector: 'app-mybootstrap-modal-component',
    templateUrl: '../template/Estrazione-template.html'
})



export class MyBootstrapModalComponent implements OnInit {

    @Input() fromParent: any;
    constructor(
        public activeModal: NgbActiveModal,
        public dataService: GenericLottoDataService) { }

    private loading = false;
    private dataDetailes: lottoDetailes;

    ngAfterViewInit(): void {
        console.log("ngAfterViewInit" +this.fromParent);
    }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.loading = true
        this.dataService.readDetailes(this.fromParent.dataitem.id, this.dataDetailes);
           
    }

    closeModal(sendData) {
        console.log("closeModal" + this.fromParent);
    }

}
