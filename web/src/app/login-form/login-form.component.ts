import { Component } from '@angular/core';
import {RegisterUser} from "../signup-form/model/signup-form.model";
import {UserLogin} from "./model/login-form.model";
import {LoginFormService} from "./service/login-form.service";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  user: UserLogin = {
    Email: '',
    Password: ''
  }
  constructor(private loginFormService: LoginFormService) {
  }
  onSubmit()
  {
    this.loginFormService.loginUser(this.user).subscribe(token=>{
      localStorage.setItem('Authorization','Bearer '+token);
      window.location.reload();
    });
  }
}
