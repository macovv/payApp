import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/Models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  user: User;
  @ViewChild('editForm', {static: false}) editForm: NgForm;

  constructor(private route: ActivatedRoute, private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user; // ??
    });
    console.log(this.user.userName);
    console.log(this.user.costs);
  }

  updateUser() {
    this.userService.updateUser(this.user, this.user.userName).subscribe(next => {
      this.editForm.reset(this.user);
    }, err => {
      console.log(err);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
