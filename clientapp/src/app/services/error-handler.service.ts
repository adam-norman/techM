import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})

export class ErrorHandlerService implements HttpInterceptor {
  constructor( private authService:AuthenticationService,private _router: Router, private alertify: ToastrService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
        if ([401, 403].indexOf(err.status) !== -1) {
            // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
            this.authService.logout();
        }

        const error = err.error.message || err.statusText;
        return throwError(error);
    }))
}

}
