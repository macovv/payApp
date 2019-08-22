import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../Models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  user: User;

  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  register() {
    this.authService.register(this.model).subscribe(data => {
      console.log();
    });
    this.route.navigate(['/user/list']);
    window.location.reload();
  }

}
