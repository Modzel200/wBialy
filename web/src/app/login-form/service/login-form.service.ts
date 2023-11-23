import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {UserLogin} from "../model/login-form.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LoginFormService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/login'

  constructor(private http: HttpClient) {
  }
  loginUser(user: UserLogin):Observable<string>{
    return this.http.post(this.baseUrl,user, {responseType: 'text'});
  }
}
