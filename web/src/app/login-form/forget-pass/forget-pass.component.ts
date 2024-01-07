import { Component } from '@angular/core';
import {LoginFormService} from "../service/login-form.service";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {ResetPassword} from "../model/reset-password.model";

@Component({
  selector: 'app-forget-pass',
  templateUrl: './forget-pass.component.html',
  styleUrls: ['./forget-pass.component.scss']
})
export class ForgetPassComponent {
  email='';
  newPass: ResetPassword = {
    ResetToken:'',
    Password:'',
    PasswordConfirm:''
  }
  constructor(private loginFormService: LoginFormService, private router: Router, public dialog: MatDialog) {
  }
  sendKey(){
    this.loginFormService.sendKey(this.email).subscribe(response => {
    });
  }

  onSubmit(){
    this.loginFormService.resetPassword(this.newPass).subscribe(response =>{
      console.log(response);
    })
  }
}
