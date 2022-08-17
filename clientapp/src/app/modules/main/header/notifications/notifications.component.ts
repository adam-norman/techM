
import { NotificationViewModel } from '@/interfaces/NotificationViewModel';
import {Component, OnInit} from '@angular/core';
import { AuthenticationService } from '@services/authentication.service';
import { NotificationsService } from '@services/notifications.service';
import * as signalR from '@microsoft/signalr';
import { environment } from 'environments/environment.prod';

@Component({
    selector: 'app-notifications',
    templateUrl: './notifications.component.html',
    styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  notOpenedNotifications :number=0;
  notifications: NotificationViewModel[]=[];
  userId: string;
  role: string;
  constructor(private _notificationService:NotificationsService,private authService: AuthenticationService){

  }
  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(u=>{
      console.log(u);
      this.userId=u.userId;
      this.role=u.role;
    });
    this.loadNotifications({pageSize:10, pageNumber: 1});
    if(this.role=="employee"){
      const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.baseUrl + 'notify',{
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("BroadcastMessage", () => {
      this.loadNotifications({pageSize:10, pageNumber: 1});
    });

    }
  }

  loadNotifications(param:any){
    this._notificationService.getUserNotifications(this.userId,param).subscribe(res=>{
      if(res.data){
       this.notifications= res.data;
       localStorage.setItem('numberOfRequests', this.notifications.length.toString());
       for (let index = 0; index <  this.notifications.length; index++) {
        const notification =  this.notifications[index];
        if(notification.opened!=1){
          this.notOpenedNotifications++;
        }
       }
      }
    });
  }
}


