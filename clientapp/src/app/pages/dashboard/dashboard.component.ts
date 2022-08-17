import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@services/authentication.service';
import { RequestFormService } from '@services/requestForm.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  numberOfRequest: number;
  isAdmin=false;
  userId: string;
  role: string;
constructor(private requestService:RequestFormService, private _authService : AuthenticationService){}
  ngOnInit(): void {
    this._authService.getCurrentUser().subscribe(u=>{
      this.userId=u.userId;
      this.role=u.role;
    });
    this.isAdmin=  this.role==="administrator";
    if(!this.isAdmin){
      this.requestService.getMyRequests(this.userId,{pageSize:10, pageNumber: 1})
        .subscribe(res=>{
          this.numberOfRequest = res.data.length;
        });
    }
    else{

        this.requestService.getAllRequests({pageSize:10, pageNumber: 1})
        .subscribe(res=>{
          this.numberOfRequest = res.data.length;
        });

      }
  }

}
