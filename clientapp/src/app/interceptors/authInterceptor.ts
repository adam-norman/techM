import { finalize } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoaderServiceService } from '../services/loader-service.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

constructor(public _loader:LoaderServiceService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this._loader.isLoading.next(true);
    const token = localStorage['token']; // you probably want to store it in localStorage or something

    if (!token) {
      return next.handle(req);
    }

    const req1 = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`),
    });

    return next.handle(req1).pipe(
      finalize(()=>{
        this._loader.isLoading.next(false);
      })
    );
  }

}
