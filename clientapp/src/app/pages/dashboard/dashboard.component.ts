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
constructor(private requestService:RequestFormService, private _authService : AuthenticationService){}
  ngOnInit(): void {
    debugger;
    this.isAdmin=  this._authService.getUserRole()==="administrator";
    if(!this.isAdmin){
      this.requestService.getMyRequests(this._authService.getUserId(),{pageSize:10, pageNumber: 1})
        .subscribe(res=>{
          this.numberOfRequest = res.data.length;
          console.log(res.data);
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
