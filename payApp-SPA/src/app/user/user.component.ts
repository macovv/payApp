import { Component, OnInit, EventEmitter } from '@angular/core';
import { User } from '../Models/user';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { WishService } from '../services/wish.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: User;
  model: any = {};

  constructor(private authService: AuthService, private userService: UserService,
              private router: Router, private route: ActivatedRoute, private wishService: WishService) { }

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
    this.wishService.addWish(this.model, this.user.userName).subscribe(() => {});
    window.location.reload();
  }

  edit() {
    this.router.navigate([this.user.userName, 'edit'], {relativeTo: this.route});
  }
}
