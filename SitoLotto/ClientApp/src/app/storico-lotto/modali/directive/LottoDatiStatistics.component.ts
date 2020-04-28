
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { kendoGridDataService } from '../../service/kendoGridData.service';
import { CDataStatistics } from '../modelData/LottoModelData';
import { ChartComponent } from '@progress/kendo-angular-charts';
import { saveAs } from '@progress/kendo-file-saver';
import { exportPDF } from '@progress/kendo-drawing';



@Component({
    selector: 'app-LottoDatiStatistics',
    templateUrl: '../template/LottoDatiStatistics.template.html'
})


export class LottoDatiStatisticsComponent implements OnInit {
    @ViewChild('graficoPalleNumeroUscite', { static: false })

    @Input() fromParent: any;

    public loading: number;
    private graficoPalleNumeroUscite: ChartComponent;

    constructor(
        public activeModal: NgbActiveModal,
        public viewBalls: CDataStatistics,
        public viewQuote: CDataStatistics,
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
            .subscribe(ris => {
                this.loading = this.loading + 1;
                var data = ris.data;
                this.viewBalls.Data = data.data;
                this.viewBalls.Min = data.Min;
                this.viewBalls.Max = data.Max;
                this.viewBalls.Average = data.Average;
            });
        this.dataService.readStatisticsQuote()
            .pipe(
                tap(data => {
                    //  this.view = data;
                })
            )
            .subscribe(ris => {
                this.loading = this.loading + 1;
                var data = ris.data;
                this.viewQuote.Data = data.data;
                this.viewQuote.Min = data.Min;
                this.viewQuote.Max = data.Max;
                this.viewQuote.Average = data.Average;
            });
    }

    public exportSvgChart(grafico: ChartComponent, nomeFile: string): void {
        grafico.exportSVG().then(
            (dataURI) => {
                saveAs(dataURI, nomeFile + '.svg');
            });
    }
    public exportPdfChart(grafico: ChartComponent, nomeFile: string): void {
        const visual = grafico.exportVisual();
        exportPDF(visual, {
            paperSize: "A4",
            landscape: true
        }).then((dataURI) => {
            saveAs(dataURI, nomeFile + '.pdf');
        });
    }
    public exportImgChart(grafico: ChartComponent, nomeFile: string): void {
        grafico.exportImage().then((dataURI) => {
            saveAs(dataURI, nomeFile + '.png');
        });
    }
}
