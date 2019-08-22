import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WishService {

  constructor(private http: HttpClient) { }


  getWishes(name: string) {
    return this.http.get('http://localhost:5000/api/user' + name + '/wish/list');
  }

  getWish(name: string, id: number) {
    return this.http.get('http://localhost:5000/api/user' + name + '/wish/' + id);
  }

  addWish(model: any, name: string) {
    return this.http.post('http://localhost:5000/api/user/' + name + '/wish/add', model);
  }

}
