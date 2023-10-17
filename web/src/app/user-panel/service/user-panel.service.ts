import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PostToAdd} from "../model/user-panel.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserPanelService{
  baseUrl = 'https://localhost:7012/api/post/eventposts/'
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': localStorage.getItem("Authorization")!
  });
  options = {headers: this.headers};
  constructor(private http: HttpClient) {
  }
  addNewPost(postToAdd: PostToAdd):Observable<object>
  {
    return this.http.post(this.baseUrl,postToAdd,this.options);
  }
}
