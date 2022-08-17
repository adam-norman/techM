import { RequestFormViewModel } from '@/interfaces/RequestFormViewModel';
import { Component, OnInit } from '@angular/core';
import { RequestFormService } from '@services/requestForm.service';

@Component({
  selector: 'app-update-requests',
  templateUrl: './update-requests.component.html',
  styleUrls: ['./update-requests.component.scss']
})
export class UpdateRequestsComponent implements OnInit {
  requestTypes = [{id:1,type:'Promotion'}, {id:2,type:'Annual leave'},
  {id:3,type:'Sick leave'}, {id:4,type:'Resign'}];
  numberOfRequests: number;
  constructor(private _requestService: RequestFormService) { }
  requests:RequestFormViewModel[]=[];
  ngOnInit(): void {
    this._requestService.getAllRequests({pageSize:10, pageNumber: 1})
    .subscribe(res=>{
      this.requests=res.data;
      this.numberOfRequests=this.requests.length;
    });
  }
  onClick(req:RequestFormViewModel,status){
    req.hasApproved=status;
    this._requestService.updateRequest(req)
    .subscribe(res=>{ });
  }
}
