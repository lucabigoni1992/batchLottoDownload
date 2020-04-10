import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StoricoLottoComponent,  } from './storico-lotto/storico-lotto.component'
import { CallRest } from './Access/CallRest.services'
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from './storico-lotto/data.service'
import { HttpClientModule } from '@angular/common/http';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MyBootstrapModalComponent } from './storico-lotto/component/directive/storico-lotto.component';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        StoricoLottoComponent, MyBootstrapModalComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'storico-lotto', component: StoricoLottoComponent }
        ]),
        ButtonsModule,
        BrowserAnimationsModule,
        GridModule,
        AngularFontAwesomeModule,
        NgbModule,
        
    ],
    entryComponents:[MyBootstrapModalComponent],
    providers: [DataService],
    bootstrap: [AppComponent]
})
export class AppModule { }
