import { Component } from '@angular/core';
import {RegisterUser} from "./model/signup-form.model";
import {SignupFormService} from "./service/signup-form.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss']
})
export class SignupFormComponent {
  user: RegisterUser = {
    Email:'',
    Password:'',
    PasswordConfirm:''
  }

  constructor(private signupFormService: SignupFormService, private _snackBar: MatSnackBar, private router: Router) {
  }

  onSubmit(){
    this.signupFormService.signUpUser(this.user).subscribe(response=>{
      console.log(response)
      if(response==null)
      {
        this._snackBar.open("Konto utworzone","dzięki");
        this.router.navigate(['/login']);
      }
    });

  }
}
