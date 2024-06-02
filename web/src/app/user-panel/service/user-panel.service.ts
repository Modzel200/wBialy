import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { PostToAdd, gastroPostToAdd, lfPostToAdd, Tags } from "../model/user-panel.model";
import { Observable } from "rxjs";
import { PageResultModel } from "../../events/model/pageResult.model";
import { EventPost } from "../../events/model/event.model";
import { map } from "rxjs/operators";
import { UploadImgModel } from "../model/uploadImg.model";
import { gastroPost } from "src/app/events/model/gastro.model";
import { lfPost } from "src/app/events/model/lostfound.model";

@Injectable({
  providedIn: 'root'
})
export class UserPanelService {
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/eventposts/';
  lfUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/lfposts/';
  gastroUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/gastroposts/';
  userUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/usereventposts/';
  deleteUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/';
  adminUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/account/';
  likeUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/like/';
  isLikedUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/';
  apiKey = '';

  public event: EventPost = {
    postId: 0,
    title: '',
    description: '',
    image: '',
    confirmed: false,
    found: false,
    place: '',
    location: '',
    eventDate: '',
    day: '',
    tags: [],
    link: '',
    isLiked: false,
    likeCount: 0
  };
  returnData = '';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': localStorage.getItem("Authorization")!
  });
  options = { headers: this.headers };
  constructor(private http: HttpClient) {
  }
  addNewPost(postToAdd: PostToAdd): Observable<object> {
    return this.http.post(this.baseUrl, postToAdd, this.options);
  }
  addNewLfPost(postToAdd: lfPostToAdd): Observable<object> {
    return this.http.post(this.lfUrl, postToAdd, this.options);
  }
  addNewGastroPost(postToAdd: gastroPostToAdd): Observable<object> {
    return this.http.post(this.gastroUrl, postToAdd, this.options);
  }
  getAllPosts() {
    return this.http.get<EventPost[]>(this.userUrl, this.options);
  }
  getAllGastro() {
    return this.http.get<EventPost[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/usergastroposts', this.options);
  }
  getAllLF() {
    return this.http.get<EventPost[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/userlfposts', this.options);
  }
  getAllTags() {
    return this.http.get<Tags[]>(this.deleteUrl + "eventtags", this.options);
  }
  getAllGastroTags() {
    return this.http.get<Tags[]>(this.deleteUrl + "gastrotags", this.options);
  }
  getAllLFTags() {
    return this.http.get<Tags[]>(this.deleteUrl + "lftags", this.options);
  }
  deleteEvent(id: number) {
    return this.http.delete(this.deleteUrl + id, this.options);
  }
  editEvent(event: EventPost, id: number) {
    return this.http.put(this.baseUrl + id, event, this.options);
  }
  editGastro(event: gastroPost, id: number) {
    return this.http.put(this.gastroUrl + id, event, this.options);
  }
  editLf(event: lfPost, id: number) {
    return this.http.put(this.lfUrl + id, event, this.options);
  }
  isAdmin() {
    return this.http.get<boolean>(this.adminUrl, this.options);
  }
  likePost(postId: number) {
    return this.http.post(this.likeUrl + postId, postId, this.options);
  }
  isLikedPost(postId: number) {
    return this.http.get(this.isLikedUrl + postId, this.options);
  }
}
