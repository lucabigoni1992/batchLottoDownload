
import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { FormGroup } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GridDataResult } from '@progress/kendo-angular-grid/dist/es2015/data/data.collection';
import { tap } from 'rxjs/internal/operators/tap';
import { BehaviorSubject } from 'rxjs';

interface ColumnSetting {
    field: string,
    title: string,
    format?: string,
    type: 'text' | 'numeric' | 'boolean' | 'date',
    width?: string,
}
interface pageable {
    buttonCount: number,
    info: boolean,
    type: ' text' | 'numeric' | 'boolean' | 'date',
    pageSizes: boolean,
    previousNext: boolean
}

@Component({
    selector: 'app-fiscal-code-component',
    templateUrl: './fiscal-code.component.html',
})


export class FiscalCodeComponent extends BehaviorSubject<any> implements OnInit {

    constructor(  private modalService: NgbModal) {
        super(null);
    }
    ngOnInit(): void {
        this.loading = false;
    }
    public loading: boolean;
}
