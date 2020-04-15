import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StoricoLottoComponent, } from './storico-lotto/storico-lotto.component'
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UploadDownloadService } from './services/upload-download.service'
import { MyBootstrapModalComponent } from './storico-lotto/modali/directive/storico-lotto.component';
import { FileManagerComponent } from './Components/gestioneFile/file-manager/file-manager.component';
import { UploadComponent } from './Components/gestioneFile/upload/upload.component';
import { DownloadComponent } from './Components/gestioneFile/download/download.component';
import { DownloadAndMadeExcel } from './Components/gestioneFile/downloadAndMadeExcel/downloadAndMadeExcel.component';
import { kendoGridDataService } from './storico-lotto/service/kendoGridData.service';
import { GenericLottoDataService } from './storico-lotto/service/GenericLottoData.service';




@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        StoricoLottoComponent,
        MyBootstrapModalComponent,
        FileManagerComponent,
        UploadComponent,
        DownloadComponent,
        DownloadAndMadeExcel
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
    entryComponents: [MyBootstrapModalComponent],
    providers: [kendoGridDataService, GenericLottoDataService, UploadDownloadService],
    bootstrap: [AppComponent]
})
export class AppModule { }
