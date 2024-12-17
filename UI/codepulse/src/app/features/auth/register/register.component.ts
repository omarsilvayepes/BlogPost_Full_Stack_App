import { Component } from '@angular/core';
import { RegisterRequest } from '../models/register-request.model';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  model:RegisterRequest;
  
    constructor(private authService:AuthService,
      private router:Router
    ) {
      this.model={
        email:'',
        password:''
      }
    }
      onFormSubmit():void{
        this.authService.Register(this.model)
        .subscribe({
          next:(response)=>{
            //Redirect  to Login Page once Register it is success
            alert(response.message);
            if(response.isSuccess){
              this.router.navigateByUrl('/login');
            }
          }
        });
      }

}
