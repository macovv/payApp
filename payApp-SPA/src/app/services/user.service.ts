import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { User } from '../Models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  userDetailed: User;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getUser(name: string): Observable<User> {
    return this.http.get<User>('http://localhost:5000/api/user/' + name);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>('http://localhost:5000/api/user');
  }

  updateUser(model: any, name: string): Observable<User> {
    return this.http.put<User>('http://localhost:5000/api/user/' + name, model);

  }

  // addWish(model: any, name: string) {
  //   return this.http.post('http://localhost:5000/api/user/' + name + '/wish/add', model);
  // }
}
