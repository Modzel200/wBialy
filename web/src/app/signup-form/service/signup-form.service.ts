import {HttpClient} from "@angular/common/http";
import {RegisterUserDto} from "../model/signup-form.model";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
@Injectable({
  providedIn: 'root'
})
export class SignupFormService{
  baseUrl = 'https://localhost:7012/api/account/register'
  constructor(private http: HttpClient) {
  }
  //signup
  signUpUser(user: RegisterUserDto): Observable<RegisterUserDto>{
    console.log("serwis");
    return this.http.post<RegisterUserDto>(this.baseUrl,user);
  }
}
