


export class ServiceSettings {
    private static getBaseUrl() {
        return document.getElementsByTagName('base')[0].href;
    }
    public static BASE_URL = ServiceSettings.getBaseUrl();
    public static BASE_URL_API = ServiceSettings.getBaseUrl() + 'api/';
    public static BASE_URL_API_Lotto = ServiceSettings.getBaseUrl() + 'api/lotto/{KendoData}';
    public static BASE_URL_API_Lotto_Detailes = ServiceSettings.getBaseUrl() + 'api/lotto/detailes/{id}';
    public static BASE_URL_API_Lotto_Detailes_Quote = ServiceSettings.getBaseUrl() + 'api/lotto/quote/{id}';
    public static BASE_URL_API_Lotto_Detailes_Statistics_Quote = ServiceSettings.getBaseUrl() + 'api/lotto/statistics/Quote';
    public static BASE_URL_API_Lotto_Detailes_Statistics_Balls = ServiceSettings.getBaseUrl() + 'api/lotto/statistics/Balls';
    public static BASE_URL_API_Changed_WebPage_GetAllSite = ServiceSettings.getBaseUrl() + 'api/ChangedWebPage/GetAllSite';
    public static BASE_URL_API_Changed_WebPage_AddSite = ServiceSettings.getBaseUrl() + 'api/ChangedWebPage/AddSite';
    public static BASE_URL_API_Changed_WebPage_ChangeSite = ServiceSettings.getBaseUrl() + 'api/ChangedWebPage/ChangeSite';
    public static BASE_URL_API_FileDispenser_Download = ServiceSettings.getBaseUrl() + 'api/FileDispenser/download';
    public static BASE_URL_API_FileDispenser_Upload = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Upload';
    public static BASE_URL_API_FileDispenser_Files = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Files';
    public static BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalle = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Made/excel/lotto';
    public static BASE_URL_API_FileDispense_MadeAndDownloadExcelLottoPalleDetailes = ServiceSettings.getBaseUrl() + 'api/FileDispenser/Made/excel/lotto/Detailes';
    
}
