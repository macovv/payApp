import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../Models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  basicUrl = 'localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;


  login(model: any) {
    const url = this.basicUrl + 'login';
    console.log(url);
    console.log(model);
    return this.http.post('http://localhost:5000/api/auth/login', model)
      .pipe(map((res: any) => {
        const user = res;
        if (user) {
          // tslint:disable-next-line:no-shadowed-variable
          const user = res;
          if (user) {
            this.currentUser = user.user;
            // console.log(user.user);
            // console.log(this.currentUser);
            // console.log(this.currentUser.income);
            // console.log(this.currentUser.userName);
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.user));
          }
        }
      }));
  }

  register(model: any) {
    return this.http.post('http://localhost:5000/api/auth/register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
