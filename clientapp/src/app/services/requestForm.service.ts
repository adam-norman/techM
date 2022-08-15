
import { PagedResponse } from '@/interfaces/PagedResponse';
import { RequestFormViewModel } from '@/interfaces/RequestFormViewModel';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestFormService {
  private baseUrl = environment.urlAddress + 'RequestForm';

  constructor(private http: HttpClient) { }
  getParams(params:any){
    return Object.keys(params).map((el)=>( `${el}=${params[el]}` )).join("&");
    }
  addRequest(requestForm:any): Observable<any> {
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    const body = JSON.stringify(requestForm);
    return this.http.post<any>(this.baseUrl + `/addRequest`,body,{ 'headers': headers });
  }

  getMyRequests(userId:string , params:any): Observable<PagedResponse<RequestFormViewModel[]>> {
    const queryStr = this.getParams(params);
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    return this.http.get<PagedResponse<RequestFormViewModel[]>>(this.baseUrl + `/getRequestByUserId/${userId}?${queryStr}`,{ 'headers': headers });
  }
  getAllRequests(params:any): Observable<PagedResponse<RequestFormViewModel[]>> {
    const queryStr = this.getParams(params);
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    return this.http.get<PagedResponse<RequestFormViewModel[]>>(this.baseUrl + `/getAllRequests?${queryStr}`,{ 'headers': headers });
  }
  updateRequest(requestForm:any): Observable<PagedResponse<RequestFormViewModel[]>> {
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    const body = JSON.stringify(requestForm);
    return this.http.post<any>(this.baseUrl + `/processRequest`,body,{ 'headers': headers });
  }
}
