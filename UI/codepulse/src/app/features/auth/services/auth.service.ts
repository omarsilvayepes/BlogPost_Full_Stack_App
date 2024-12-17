import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';
import { RegisterRequest } from '../models/register-request.model';
import { RegisterResponse } from '../models/register-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user=new BehaviorSubject<User | undefined>(undefined);

  constructor(private http:HttpClient,
    private cookieService:CookieService) { }

  Login(request:LoginRequest):Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/Auth/login`,{
      Email:request.email,
      Password:request.password
    });
  }

  Register(request:RegisterRequest):Observable<RegisterResponse>{
    return this.http.post<RegisterResponse>(`${environment.apiBaseUrl}/api/Auth/register`,{
      Email:request.email,
      Password:request.password
    });
  }

  LogOut():void{
    localStorage.clear();
    this.cookieService.delete('Authorization','/');
    this.$user.next(undefined);
  }

  setUser(user:User):void{
    this.$user.next(user); // emit the user when login
    localStorage.setItem('user-email',user.email);
    localStorage.setItem('user-roles',user.roles.join(','));
  }

  getUser():User | undefined{
    const email=localStorage.getItem('user-email');
    const roles=localStorage.getItem('user-roles');

    if(email && roles){
      const user:User={
        email:email,
        roles:roles.split(',')
      };
      return user;
    }
    return undefined;
  }

  user():Observable<User| undefined>{
    return this.$user.asObservable(); //recieve the user
  }

}
