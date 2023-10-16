import { Component } from '@angular/core';
import {RegisterUserDto} from "./model/signup-form.model";
import {SignupFormService} from "./service/signup-form.service";

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss']
})
export class SignupFormComponent {
  user: RegisterUserDto = {
    Email:'',
    Password:'',
    PasswordConfirm:''
  }

  constructor(private signupFormService: SignupFormService) {
  }

  onSubmit(){
    this.signupFormService.signUpUser(this.user).subscribe(response=>{console.log(response)});
    console.log(this.user.Email,this.user.Password);
    console.log("Test test");
  }
}
