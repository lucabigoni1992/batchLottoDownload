
import { Component, Inject, OnInit, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-mybootstrap-modal-component',
    templateUrl: '../template/Estrazione-template.html'
})
export class MyBootstrapModalComponent implements OnInit {

    @Input() fromParent: any;
    constructor(
        public activeModal: NgbActiveModal
    ) { }
    ngAfterViewInit(): void {
        console.log(this.fromParent);
    }

    ngOnInit() {
        console.log(this.fromParent);
    }

    closeModal(sendData) {
        this.activeModal.close(sendData);
    }

}
