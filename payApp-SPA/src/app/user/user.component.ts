import { Component, OnInit } from '@angular/core';
import { User } from '../Models/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: User;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.user = this.authService.currentUser;
  }
}
