import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyRequestsComponent } from './my-requests/my-requests.component';
import { RequestFormComponent } from './request-form/request-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RequestTypePipe } from '@/pipes/request-type.pipe';



@NgModule({
  declarations: [
    MyRequestsComponent,
    RequestFormComponent,
    RequestTypePipe,

  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
  ]
})
export class EmployeeModule { }
