
import { OnInit, Input, ViewChild, Component, Directive } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { ConfrontositiGridDataService } from '../../service/confronto-siti-GridData.service';
import { ChartComponent } from '@progress/kendo-angular-charts';
import { saveAs } from '@progress/kendo-file-saver';
import { exportPDF } from '@progress/kendo-drawing';
import { inputData } from '../modelData/InputData';
import { FormControl, Validators, FormGroup, ValidatorFn, AbstractControl, FormBuilder } from '@angular/forms';
import {  MustMatch } from '../modelData/controlData';

@Component({
    selector: 'app-ConfrontoSitiAddRecord',
    templateUrl: '../template/confrontoSitiAddRecord.template.html'
})
export class ConfrontoSitiAddRecordComponent implements OnInit {

    @Input() fromParent: any;

    submitted = false;
    public loading: number;
    constructor(
        public activeModal: NgbActiveModal,
        private formBuilder: FormBuilder,
        private dataService: ConfrontositiGridDataService) { }

    registerForm: FormGroup;

    ngAfterViewInit(): void {

        console.log("ngAfterViewInit" + this.fromParent);
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    ngOnInit() {
        console.log("ngOnInit" + this.fromParent);
        this.loading = 2;
        this.registerForm = this.formBuilder.group({
            url: ['', Validators.required],
            //Validators.pattern("^\d{1,4}$") // wrong
            email: ['luca.bigoni@live.it', [Validators.required, Validators.pattern("^[\\w-\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]],
            ore: ['24', Validators.min(12)],
            tag: ['', Validators.required]
            //password: ['', [Validators.required, Validators.minLength(6)]],
            //confirmPassword: ['', Validators.required],
            //acceptTerms: [false, Validators.requiredTrue]
        }, /*{
            validator: MustMatch('password', 'confirmPassword')
        }*/);

    }

   


    //funzioni

    //public exportImgChart(grafico: ChartComponent, nomeFile: string): void {
    //    grafico.exportImage().then((dataURI) => {
    //        saveAs(dataURI, nomeFile + '.png');
    //    });
    //}
    public saveData(): void {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        // display form values on success
        alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value, null, 4));
    }

    onReset() {
        this.submitted = false;
        this.registerForm.reset();
    }

    closeModal() {
        this.activeModal.close();
    }
}
