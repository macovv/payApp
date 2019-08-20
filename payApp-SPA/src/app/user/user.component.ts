import { Component, OnInit, EventEmitter } from '@angular/core';
import { User } from '../Models/user';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: User;
  model: any = {};

  constructor(private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getUser(this.authService.currentUser.userName).subscribe((res: User) => {
      this.user = res;
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  addWish() {
    this.userService.addWish(this.user.userName, this.model).subscribe(() => {});
  }
}