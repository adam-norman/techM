
import { NotificationViewModel } from '@/interfaces/NotificationViewModel';
import {Component, OnInit} from '@angular/core';
import { AuthenticationService } from '@services/authentication.service';
import { NotificationsService } from '@services/notifications.service';
@Component({
    selector: 'app-notifications',
    templateUrl: './notifications.component.html',
    styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  notOpenedNotifications :number=0;
  notifications: NotificationViewModel[]=[];
  userId: string;
  constructor(private _notificationService:NotificationsService,private authService: AuthenticationService){

  }
  ngOnInit(): void {
    this.userId= this.authService.getUserId();
    if(this.authService.getUserRole()=="employee"){
      this.loadNotifications({pageSize:10, pageNumber: 1});
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
