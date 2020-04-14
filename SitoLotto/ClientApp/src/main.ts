import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export const conSolution = {
    BASE_URL: getBaseUrl(),
    BASE_URL_API: getBaseUrl() + 'api/',
    BASE_URL_API_Lotto: getBaseUrl() + 'api/lotto/{KendoData}',
    BASE_URL_API_Lotto_Detailes: getBaseUrl() + 'api/lotto/detailes/{id}',
    BASE_URL_API_FileDispenser_Download: getBaseUrl() + 'api/FileDispenser/download',
    BASE_URL_API_FileDispenser_Upload: getBaseUrl() + 'api/FileDispenser/Upload'
};
function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
    enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
    .catch(err => console.log(err));
