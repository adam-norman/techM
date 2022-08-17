import { RequestFormViewModel } from '@/interfaces/RequestFormViewModel';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@services/authentication.service';
import { RequestFormService } from '@services/requestForm.service';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss']
})
export class MyRequestsComponent implements OnInit {

  requestTypes = [{id:1,type:'Promotion'}, {id:2,type:'Annual leave'},
  {id:3,type:'Sick leave'}, {id:4,type:'Resign'}];
  numberOfRequests: number;
  userId:string='';
  constructor(private _requestService: RequestFormService,private authService: AuthenticationService,
    ) { }
  requests:RequestFormViewModel[]=[];
  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(u=>{
      this.userId=u.userId;
    });
    this.loadRequests({ pageSize: 10, pageNumber: 1 });
  }

  private loadRequests(param:any) {
    this._requestService.getMyRequests(this.userId,param)
      .subscribe(res => {
        this.requests = res.data;
        this.numberOfRequests = this.requests.length;
      });
  }
}
