import { Component, AfterViewInit } from '@angular/core';

@Component({
    selector: 'app-counter-component',
    templateUrl: './counter.component.html'
})
export class CounterComponent implements AfterViewInit {
    public currentCount = 0;
    public loading = false;
    public typeProgress = 1;

    ngAfterViewInit() {

    }

    public incrementCounter(type:number) {
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
