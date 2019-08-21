import { CanActivate, Router, ActivatedRoute, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { routes } from '../routes';

@Injectable({
    providedIn: 'root'
  })
export class EditGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        console.log(this.authService.currentUser.userName);
        console.log(route.params.username);
        if (this.authService.currentUser.userName === route.params.username) {
            return true;
        }
        return false;
    }
}



