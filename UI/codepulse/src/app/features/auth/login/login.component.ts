import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model:LoginRequest;

  constructor(private authService:AuthService,
    private cookieService:CookieService,
    private router:Router
  ) {
    this.model={
      email:'',
      password:''
    }
  }
    onFormSubmit():void{
      this.authService.Login(this.model)
      .subscribe({
        next:(response)=>{
          //Store Auth Cookie using ngx-cookie-service library
          this.cookieService.set('Authorization',`Bearer ${response.token}`,
            undefined,'/',undefined,true,'Strict');

            //set the user on local storage and emit it
            this.authService.setUser({
              email:response.email,
             roles:response.roles 
            });

          //Redirect  to Home Page once Login it is success
          this.router.navigateByUrl('/');
        },
        error:(response)=>{
          const firstErrorMessage = response.error.errors.e;
          alert(firstErrorMessage);
        }
      });
    }
    
}
