import {HttpClient} from "@angular/common/http";
import {RegisterUser} from "../model/signup-form.model";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
@Injectable({
  providedIn: 'root'
})
export class SignupFormService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/register'
  constructor(private http: HttpClient) {
  }
  //signup
  signUpUser(user: RegisterUser): Observable<RegisterUser>{
    return this.http.post<RegisterUser>(this.baseUrl,user);
  }
}
