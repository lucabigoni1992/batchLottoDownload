import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

export interface AlertError {
    title: string;
    text: string;
}
@Component({
    selector: 'app-alertError',
    templateUrl: 'alertError-template.html'
})  
export class AlertErrorComponent {
    @Input() public inpVal: AlertError;

    constructor(public activeModal: NgbActiveModal) {
    }
    closeModal() {
        this.activeModal.close();
    }
}
