import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {UserLogin} from "../model/login-form.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LoginFormService{
  baseUrl = 'https://localhost:7012/api/account/login'

  constructor(private http: HttpClient) {
  }
  loginUser(user: UserLogin):Observable<string>{
    return this.http.post<string>(this.baseUrl,user);
  }
}
