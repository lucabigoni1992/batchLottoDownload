


export class ServiceSettings {
    private static getBaseUrl() {
        return document.getElementsByTagName('base')[0].href;
    }
    public static BASE_URL = ServiceSettings.getBaseUrl();
    public static BASE_URL_API = ServiceSettings.getBaseUrl() + 'api/';
    public static BASE_URL_API_Lotto = ServiceSettings.getBaseUrl() + 'api/lotto/{KendoData}';
    public static BASE_URL_API_Lotto_Detailes = ServiceSettings.getBaseUrl() + 'api/lotto/detailes/{id}';
    public static BASE_URL_API_Lotto_Detailes_Quote = ServiceSettings.getBaseUrl() + 'api/lotto/quote/{id}';
    public static BASE_URL_API_FileDispenser_Download = ServiceSettings.getBaseUrl() + 'api/FileDispenser/download';
    public static BASE_URL_API_FileDispenser_Upload = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Upload';
    public static BASE_URL_API_FileDispenser_Files = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Files';
    public static BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalle = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Made/excel/lotto';
    
}
