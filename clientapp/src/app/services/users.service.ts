
import { NotificationViewModel } from '@/interfaces/NotificationViewModel';
import { PagedResponse } from '@/interfaces/PagedResponse';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private baseUrl = environment.urlAddress + 'users';

  constructor(private http: HttpClient) { }
  getParams(params:any){
  return Object.keys(params).map((el)=>( `${el}=${params[el]}` )).join("&");
  }
  getUserNotifications(params:any): Observable<PagedResponse<NotificationViewModel[]>> {
    const queryStr = this.getParams(params);
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    return this.http.get<PagedResponse<NotificationViewModel[]>>(this.baseUrl + `/getUserNotifications/?${queryStr}`,{ 'headers': headers });
  }


}
