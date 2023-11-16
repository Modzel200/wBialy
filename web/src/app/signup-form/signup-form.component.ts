import {Component, OnInit} from '@angular/core';
import {RegisterUser} from "./model/signup-form.model";
import {SignupFormService} from "./service/signup-form.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { CustomSnackbarComponent } from '../custom-snackbar/custom-snackbar.component';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss']
})
export class SignupFormComponent implements OnInit{
  user: RegisterUser = {
    Email:'',
    Password:'',
    PasswordConfirm:''
  }

  constructor(private signupFormService: SignupFormService, private _snackBar: MatSnackBar, private router: Router) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")!=null)
    {
      this.router.navigate(["/"]);
    }
  }

  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [
    (c: AbstractControl) => Validators.required(c),
    Validators.pattern(
      /.{8,}/
    ),
  ]);
  passwordConfirm = new FormControl('', [
    Validators.required,
    this.passwordMatchValidator.bind(this)
  ]);

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = this.password.root.value;
    const confirmPassword = control.value;
    if (password !== confirmPassword) {
      return { passwordMismatch: true };
    }

    return null;
  }

  getErrorMessage() {
    if (this.email.hasError('required') && this.email.root.touched) {
      return 'Musisz wprowadzić wartość';
    }
    return this.email.hasError('email') ? 'Nieprawidłowy email' : '';
  }

  getPasswordMessage() {
    if (this.password.hasError('required') && this.password.root.touched) {
      return 'Musisz wprowadzić wartość';
    }
    return this.password.hasError('pattern') ? 'Za krótkie hasło' : '';
  }

  getPasswordConfirmMessage() {
    if (this.passwordConfirm.hasError('required') && this.passwordConfirm.root.touched) {
      return 'Musisz wprowadzić wartość';
    }
    return this.passwordConfirm.hasError('passwordMismatch') ? 'Hasła nie zgadzają się' : '';
  }

  onSubmit(){
    if(this.passwordConfirm.errors || this.password.errors || this.email.errors){
      return;
    }
    this.signupFormService.signUpUser(this.user).subscribe(response=>{
      if(response==null)
      {
        this.openCustomSnackbar("Konto utworzone");
        this.router.navigate(['/login'  ]);
      }
    });

  }

  openCustomSnackbar(message: string): void {
      this._snackBar.openFromComponent(CustomSnackbarComponent, {
      panelClass: ['snackbar'],
      data: { message },
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

}
