import { UserForAuthenticationDto } from '@/interfaces/UserForAuthenticationDto';
import { User } from '@/models/User';
import { CustomEncoder } from '@/utils/CustomEncoder';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { EnvironmentUrlService } from './environment-url.service';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  jwtHelper = new JwtHelperService();

  private _returnUrl: string = "";

  private _authChangeSub = new Subject<boolean>();

  constructor(private _http: HttpClient, private _envUrl: EnvironmentUrlService, public toastr: ToastrService,
    private _router: Router, private _route: ActivatedRoute) {
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this._authChangeSub.next(isAuthenticated);
  }
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}${route}`;
  }

  public loginUser = (body: UserForAuthenticationDto) => {
    return this._http.post(this._envUrl.urlAddress + 'accounts/login', body).pipe(
      map((res: any) => {
        const user = res;
        if (user && user.token) {
          const decodedToken = this.jwtHelper.decodeToken(user.token);
          localStorage.setItem('token', user.token);
        }
        this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
        this._router.navigate([this._returnUrl]);
      }));
  }
  public confirmEmail = (route: string, token: string, email: string) => {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', token);
    params = params.append('email', email);
    return this._http.get(this.createCompleteRoute(route, this._envUrl.urlAddress), { params: params });
  }

  getCurrentUser(): Observable<User> {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      let user: User = { userId: '', userName: '', fullName: '', role: '' };
      user.role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      user.fullName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"];
      user.userName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      user.userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      let currentUserSubject = new BehaviorSubject<User>(user);
      return currentUserSubject.asObservable();
    } else{
      this.logout();
    }

  }
  public logout = () => {
    localStorage.removeItem('token');
    this._router.navigate(['/login']);
  }
  loggedIn() {
    const token = localStorage.getItem('token') ?? "";
    return !this.jwtHelper.isTokenExpired(token);
  }
  getUserName() {
    if (this.loggedIn()) {
      return localStorage.getItem('username');
    } else {
      return "";
    }
  }
  getUserId() {
    if (this.loggedIn()) {
      return localStorage.getItem('userid');
    } else {
      return "";
    }
  }
}
