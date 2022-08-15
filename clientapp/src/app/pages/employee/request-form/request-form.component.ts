import { RequestForm } from '@/models/RequestForm';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/authentication.service';
import { RequestFormService } from '@services/requestForm.service';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {
  submitted = false;
  requestTypes = [{id:1,type:'Promotion'}, {id:2,type:'Annual leave'},
  {id:3,type:'Sick leave'}, {id:4,type:'Resign'}];
  registrationForm = new FormGroup(
    {
      subject: new FormControl<string>("", Validators.required),
      body: new FormControl<string>("", Validators.required),
      requestTypeId: new FormControl<number>(0, Validators.required),
      employeeId: new FormControl<string>("", Validators.required),
    },
    {
      validators: Validators.compose([
        this.selectDefaultRequestTypeOption("requestTypeId"),
      ]),
    }
  );
  userId: string;

  constructor(private requestService:RequestFormService,private authService: AuthenticationService,
    private _router: Router,) { }



  ngOnInit(): void {
    this.userId= this.authService.getUserId();
    this.registrationForm.controls.employeeId.setValue(this.userId);

  }
  showFormControls(form: any) {
    return form && form.controls['body'] &&
    form.controls['body'].value;
  }

  register() {
    this.registrationForm.controls.employeeId.setValue(this.userId);
    this.submitted = true;
    if (this.registrationForm.valid) {
      let createRequestDto= new RequestForm(0,this.registrationForm.value.subject,this.registrationForm.value.body,
        this.registrationForm.value.requestTypeId
        );
        this.requestService.addRequest(this.registrationForm.value).subscribe(res=>{

          this._router.navigate(['/']);
        });
    }
}
get controls() {
  return this.registrationForm.controls;
}
selectDefaultRequestTypeOption(requestTypeId: string): ValidatorFn {
  return (formGroup: AbstractControl): { [key: string]: any } | null => {
    const requestTypeIdControl = formGroup.get(requestTypeId);

    if (!requestTypeIdControl) {
      return null;
    }

    if (
      requestTypeIdControl.errors &&
      !requestTypeIdControl.errors["selectDefaultRequestTypeOption"]
    ) {
      return null;
    }

    if (requestTypeIdControl.value === 0) {
      requestTypeIdControl.setErrors({ selectDefaultRequestTypeOption: true });
      return { selectDefaultRequestTypeOption: true };
    } else {
      requestTypeIdControl.setErrors(null);
      return null;
    }
  };
}
}
