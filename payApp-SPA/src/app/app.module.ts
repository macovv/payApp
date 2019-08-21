import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthService } from './services/auth.service';
import { RegisterComponent } from './register/register.component';
import { UserService } from './services/user.service';
import { UserComponent } from './user/user.component';
import { routes } from './routes';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from 'resolvers/user-edit.resolver';
import { EditGuard } from './guards/edit.guard';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    UserComponent,
    HomeComponent,
    UserListComponent,
    UserDetailComponent,
    UserEditComponent,
    // FormsModule.fo
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    // RouterModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
         // tslint:disable-next-line:object-literal-shorthand
         tokenGetter: tokenGetter,
         whitelistedDomains: ['localhost:5000'],
         blacklistedRoutes: ['localhost:5000/api/auth']
      }
   })

  ],
  providers: [
    AuthService,
    UserService,
    UserEditResolver,
    EditGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
