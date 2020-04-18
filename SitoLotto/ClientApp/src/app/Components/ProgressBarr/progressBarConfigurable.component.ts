import { Component, OnInit, Input } from '@angular/core';
import { MatSliderChange } from '@angular/material';

@Component({
    selector: 'app-progressBarConfigurable',
    templateUrl: './progressBarConfigurable.template.html'
})

export class ProgressBarConfigurableModule {
  
    onSliderChange(event: MatSliderChange) {
        this.value = event.value;
    }
    onSliderChangebuffer(event: MatSliderChange) {
        this.bufferValue = event.value;
    }
    @Input() public type: number = -1;
    @Input() public debugmode = true;
    @Input() public color: string = 'primary';
    @Input() public mode: string= 'buffer';
    @Input() public value: number= 50;
    @Input() public bufferValue: number= 75;

}

