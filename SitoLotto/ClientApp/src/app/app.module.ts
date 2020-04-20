import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { testModaliComponent } from './testModali/testModali.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StoricoLottoComponent, } from './storico-lotto/storico-lotto.component'
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownListModule,ComboBoxModule} from '@progress/kendo-angular-dropdowns';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChartsModule } from '@progress/kendo-angular-charts';
import { LabelModule } from '@progress/kendo-angular-label';
import { HttpClientModule } from '@angular/common/http';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UploadDownloadService } from './services/gestioneFile/upload-download.service'
import { LottoDatiEstrazioneComponent } from './storico-lotto/modali/directive/LottoDatiEstrazione.component';
import { FileManagerComponent } from './Components/gestioneFile/file-manager/file-manager.component';
import { UploadComponent } from './Components/gestioneFile/upload/upload.component';
import { DownloadComponent } from './Components/gestioneFile/download/download.component';
import { DownloadAndMadeExcelModule } from './Components/gestioneFile/downloadAndMadeExcel/downloadAndMadeExcel.component';
import { kendoGridDataService } from './storico-lotto/service/kendoGridData.service';
import { GenericLottoDataService } from './storico-lotto/service/GenericLottoData.service';
import { LottoDatiVinciteComponent } from './storico-lotto/modali/directive/LottoDatiVincite.component';
import { AlertErrorComponent } from './Components/Alert/Error/alertError.component';
import { LottoDatiStatisticsComponent } from './storico-lotto/modali/directive/LottoDatiStatistics.component';
import { ProgressBarConfigurableModule } from './Components/ProgressBarr/progressBarConfigurable.component';
import { InternalClassModule } from './app.module.class';
import { MaterialModule } from './app.module.material';




@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        testModaliComponent,
        FetchDataComponent,
        StoricoLottoComponent,
        LottoDatiEstrazioneComponent,
        LottoDatiVinciteComponent,
        LottoDatiStatisticsComponent,
        FileManagerComponent,
        UploadComponent,
        DownloadComponent,
        DownloadAndMadeExcelModule,
        ProgressBarConfigurableModule,
        AlertErrorComponent

    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'testModali', component: testModaliComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'storico-lotto', component: StoricoLottoComponent }
        ]),
        ButtonsModule,
        BrowserAnimationsModule,
        GridModule,
        DropDownListModule,
        ComboBoxModule,
        ChartsModule,
        LabelModule,
        AngularFontAwesomeModule,
        NgbModule,
        MaterialModule,
        InternalClassModule
    ],
    entryComponents: [LottoDatiEstrazioneComponent, LottoDatiVinciteComponent, LottoDatiStatisticsComponent, AlertErrorComponent],
    providers: [kendoGridDataService, GenericLottoDataService, UploadDownloadService],
    bootstrap: [AppComponent]
})
export class AppModule { }

