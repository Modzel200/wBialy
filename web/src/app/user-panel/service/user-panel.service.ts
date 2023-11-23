import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PostToAdd, gastroPostToAdd, lfPostToAdd, Tags} from "../model/user-panel.model";
import {Observable} from "rxjs";
import {PageResultModel} from "../../events/model/pageResult.model";
import {EventPost} from "../../events/model/event.model";
import {map} from "rxjs/operators";
import {UploadImgModel} from "../model/uploadImg.model";

@Injectable({
  providedIn: 'root'
})
export class UserPanelService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/eventposts/';
  lfUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/lfposts/';
  gastroUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/gastroposts';
  userUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/usereventposts/';
  deleteUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/';
  adminUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/';
  apiKey = '0044368c0f15bd2f0120f0819f511ee9';
  public event: EventPost={
    postId: 0,
    title:'',
    description:'',
    image:'',
    confirmed: false,
    place:'',
    location:'',
    eventDate:'',
    day:'',
    tags:[],
    link:'',
  };
  returnData = '';
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
  addNewLfPost(postToAdd: lfPostToAdd):Observable<object>
  {
    return this.http.post(this.lfUrl,postToAdd,this.options);
  }
  addNewGastroPost(postToAdd: gastroPostToAdd):Observable<object>
  {
    return this.http.post(this.gastroUrl,postToAdd,this.options);
  }
  getAllPosts()
  {
    return this.http.get<EventPost[]>(this.userUrl,this.options);
  }
  getAllTags()
  {
    return this.http.get<Tags[]>(this.deleteUrl+"eventtags",this.options);
  }
  getAllGastroTags()
  {
    return this.http.get<Tags[]>(this.deleteUrl+"gastrotags",this.options);
  }
  getAllLFTags()
  {
    return this.http.get<Tags[]>(this.deleteUrl+"lftags",this.options);
  }
  deleteEvent(id:number)
  {
    return this.http.delete(this.deleteUrl+id,this.options);
  }
  editEvent(event: EventPost, id: number)
  {
    return this.http.put(this.baseUrl+id,event,this.options);
  }
  isAdmin()
  {
    return this.http.get<boolean>(this.adminUrl,this.options);
  }
}
