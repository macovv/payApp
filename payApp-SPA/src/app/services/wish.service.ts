import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Wish } from '../Models/wish';

@Injectable({
  providedIn: 'root'
})
export class WishService {

  constructor(private http: HttpClient) { }


  getWishes(): Observable<Wish[]> {
    return this.http.get<Wish[]>('http://localhost:5000/api/wishes/list');
  }

  getUserWishes(name: string): Observable<Wish[]> {
    return this.http.get<Wish[]>('http://localhost:5000/api/wishes/user/' + name);
  }

  getWish(id: number): Observable<Wish> {
    return this.http.get<Wish>('http://localhost:5000/api/wishes/' + id);
  }

  addWish(model: any, name: string) {
    return this.http.post('http://localhost:5000/api/wishes/user/' + name + '/add', model);
  }

}
