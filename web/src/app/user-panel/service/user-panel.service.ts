import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PostToAdd} from "../model/user-panel.model";
import {Observable} from "rxjs";
import {PageResultModel} from "../../events/model/pageResult.model";
import {EventPost} from "../../events/model/event.model";

@Injectable({
  providedIn: 'root'
})
export class UserPanelService{
  baseUrl = 'https://localhost:7012/api/post/eventposts/';
  userUrl = 'https://localhost:7012/api/post/usereventposts/';
  deleteUrl = 'https://localhost:7012/api/post/';
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
  getAllPosts()
  {
    return this.http.get<EventPost[]>(this.userUrl,this.options);
  }
  deleteEvent(id:number)
  {
    return this.http.delete(this.deleteUrl+id,this.options);
  }
}
