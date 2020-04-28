"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ServiceSettings = /** @class */ (function () {
    function ServiceSettings() {
    }
    ServiceSettings.getBaseUrl = function () {
        return document.getElementsByTagName('base')[0].href;
    };
    ServiceSettings.BASE_URL = ServiceSettings.getBaseUrl();
    ServiceSettings.BASE_URL_API = ServiceSettings.getBaseUrl() + 'api/';
    ServiceSettings.BASE_URL_API_Lotto = ServiceSettings.getBaseUrl() + 'api/lotto/{KendoData}';
    ServiceSettings.BASE_URL_API_Lotto_Detailes = ServiceSettings.getBaseUrl() + 'api/lotto/detailes/{id}';
    ServiceSettings.BASE_URL_API_Lotto_Detailes_Quote = ServiceSettings.getBaseUrl() + 'api/lotto/quote/{id}';
    ServiceSettings.BASE_URL_API_Lotto_Detailes_Statistics_Quote = ServiceSettings.getBaseUrl() + 'api/lotto/statistics/Quote';
    ServiceSettings.BASE_URL_API_Lotto_Detailes_Statistics_Balls = ServiceSettings.getBaseUrl() + 'api/lotto/statistics/Balls';
    ServiceSettings.BASE_URL_API_FileDispenser_Download = ServiceSettings.getBaseUrl() + 'api/FileDispenser/download';
    ServiceSettings.BASE_URL_API_FileDispenser_Upload = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Upload';
    ServiceSettings.BASE_URL_API_FileDispenser_Files = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Files';
    ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalle = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Made/excel/lotto';
    ServiceSettings.BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalleDetailes = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Made/excel/lotto/Detailes';
    return ServiceSettings;
}());
exports.ServiceSettings = ServiceSettings;
//# sourceMappingURL=ServiceConst.js.map