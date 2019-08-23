import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from 'resolvers/user-edit.resolver';
import { EditGuard } from './guards/edit.guard';
import { WishComponent } from './wish/wish.component';
import { WishListComponent } from './wish/wish-list/wish-list.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, children: [
        {path: 'user', component: UserComponent},
        {path: 'user/list', component: UserListComponent},
        {path: 'user/:username/edit', component: UserEditComponent, resolve: {user: UserEditResolver}, canActivate: [EditGuard]},
        {path: 'user/details/:username', component: UserDetailComponent},
        {path: 'wish/list', component: WishListComponent},
        {path: 'wish/:id', component: WishComponent},
    ]},
    { path: '**', redirectTo: '', pathMatch: 'full'},
];
