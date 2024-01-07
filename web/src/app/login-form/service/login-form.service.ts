import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {UserLogin} from "../model/login-form.model";
import {Observable} from "rxjs";
import {ResetPassword} from "../model/reset-password.model";

@Injectable({
  providedIn: 'root'
})
export class LoginFormService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/login'
  forgetUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/forgotpassword/'
  resetUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/resetpassword'
  constructor(private http: HttpClient) {
  }
  loginUser(user: UserLogin):Observable<string>{
    return this.http.post(this.baseUrl,user, {responseType: 'text'});
  }
  sendKey(email:string){
    return this.http.post(this.forgetUrl+email,'');
  }
  resetPassword(newPassword: ResetPassword){
    return this.http.post(this.resetUrl,newPassword);
  }
}
