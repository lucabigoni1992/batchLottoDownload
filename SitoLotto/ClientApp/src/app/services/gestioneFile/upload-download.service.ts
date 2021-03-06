import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse, HttpParams } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { ServiceSettings } from '../ServiceConst';

@Injectable()
export class UploadDownloadService {
    private apiDownloadUrl: string;
    private apiUploadUrl: string;
    private apiFileUrl: string;

    constructor(private httpClient: HttpClient) {
        this.apiDownloadUrl = ServiceSettings.BASE_URL_API_FileDispenser_Download;
        this.apiUploadUrl = ServiceSettings.BASE_URL_API_FileDispenser_Upload;
        this.apiFileUrl = ServiceSettings.BASE_URL_API_FileDispenser_Files;

    }
    public madeAndDownloadExcelFile(apiMadeAndDownloadExcel: string, file:string, id:number): Observable<HttpEvent<Blob>> {

        return this.httpClient.request(new HttpRequest(
            'GET',
            `${apiMadeAndDownloadExcel}?file=${file}&idLotto=${id}`,
            {
                reportProgress: true,
                responseType: 'blob'
            }));
    }


    public downloadFile(file: string): Observable<HttpEvent<Blob>> {
        return this.httpClient.request(new HttpRequest(
            'GET',
            `${this.apiDownloadUrl}?file=${file}`,
            null,
            {
                reportProgress: true,
                responseType: 'blob'
            }));
    }

    public uploadFile(file: Blob): Observable<HttpEvent<void>> {
        const formData = new FormData();
        formData.append('file', file);

        return this.httpClient.request(new HttpRequest(
            'POST',
            this.apiUploadUrl,
            formData,
            {
                reportProgress: true
            }));
    }

    public getFiles(): Observable<string[]> {
        return this.httpClient.get<string[]>(this.apiFileUrl);
    }
}
