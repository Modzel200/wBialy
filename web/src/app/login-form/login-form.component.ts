import { Component, OnInit } from '@angular/core';
import { RegisterUser } from "../signup-form/model/signup-form.model";
import { UserLogin } from "./model/login-form.model";
import { LoginFormService } from "./service/login-form.service";
import { Router } from '@angular/router';
import {EventComponent} from "../events/event/event.component";
import {MatDialog} from "@angular/material/dialog";
import {ForgetPassComponent} from "./forget-pass/forget-pass.component";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {
  user: UserLogin = {
    Email: '',
    Password: ''
  }
  constructor(private loginFormService: LoginFormService, private router: Router, public dialog: MatDialog) {
  }
  ngOnInit() {
    if (localStorage.getItem("Authorization") != null) {
      this.router.navigate(["/"]);
    }
  }

  errorMessage = '';
  onSubmit() {
    this.errorMessage = "";
    this.loginFormService.loginUser(this.user).subscribe(token => {
      localStorage.setItem('Authorization', 'Bearer ' + token);
      window.location.reload();
    }, () => {
      this.errorMessage = "Niepoprawne dane logowania"
    })

  }
  forgetPassword(){
    const classmode = localStorage.getItem('DarkMode') === 'true' ? 'dark-mode' : '';
    const dialogRef = this.dialog.open(ForgetPassComponent, {
      autoFocus: false,
      panelClass: classmode
    });
  }
}
