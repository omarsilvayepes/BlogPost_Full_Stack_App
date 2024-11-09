import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user=new BehaviorSubject<User | undefined>(undefined);

  constructor(private http:HttpClient) { }

  Login(request:LoginRequest):Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/Auth/login`,{
      Email:request.email,
      Password:request.password
    });
  }

  setUser(user:User):void{
    this.$user.next(user); // emit the user when login
    localStorage.setItem('user-email',user.email);
    localStorage.setItem('user-roles',user.roles.join(','));
  }

  user():Observable<User| undefined>{
    return this.$user.asObservable(); //recieve the user
  }

}