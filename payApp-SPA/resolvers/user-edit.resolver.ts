import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from 'src/app/Models/user';
import { UserService } from 'src/app/services/user.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class UserEditResolver implements Resolve<User> {
    constructor(private userService: UserService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUser(route.paramMap.get('username')).pipe(catchError(err => {
            this.router.navigate(['/user']);
            return of(null);
        }));
    }
}
