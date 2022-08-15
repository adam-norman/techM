
import { NotificationViewModel } from '@/interfaces/NotificationViewModel';
import { PagedResponse } from '@/interfaces/PagedResponse';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private baseUrl = environment.urlAddress + 'notification';

  constructor(private http: HttpClient) { }

  getUserNotifications(userId:string, params:any): Observable<PagedResponse<NotificationViewModel[]>> {
    const queryStr = Object.keys(params).map((el)=>( `${el}=${params[el]}` )).join("&");
    const headers = { 'content-type': 'application/json; charset=utf-8' , 'accept': 'application/json' };
    return this.http.get<PagedResponse<NotificationViewModel[]>>(this.baseUrl + `/getUserNotifications/${userId}/?${queryStr}`,{ 'headers': headers });
  }
}
