import {Injectable} from "@angular/core";
import {EventPost} from "../../../events/model/event.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AdminService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/admin';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': localStorage.getItem("Authorization")!
  });
  options = {headers: this.headers};
  constructor(private http: HttpClient) {
  }
  getAllPostsToAccept()
  {
    return this.http.get<EventPost[]>(this.baseUrl,this.options);
  }
  acceptPost(id: number)
  {
    return this.http.put(this.baseUrl+"/"+id,null,this.options);
  }
  deletePost(id: number)
  {
    return this.http.delete(this.baseUrl+"/"+id,this.options);
  }
}
