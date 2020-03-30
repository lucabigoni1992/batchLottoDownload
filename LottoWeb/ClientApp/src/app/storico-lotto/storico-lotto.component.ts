import { Component } from '@angular/core';

@Component({
    selector: 'app-storico-lotto-component',
    templateUrl: './storico-lotto.component.html'
})
export class StoricoLottoComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    };
    public title = 'Hello World!';

    public onButtonClick() {
        this.title = 'Hello from Kendo UI!';
    }
}
