import { Injectable } from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { EventPost } from "../model/event.model";
import { PageResultModel } from "../model/pageResult.model";
import { Observable } from "rxjs";
import { Tags } from "../../user-panel/model/user-panel.model";

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/eventposts/';
  likedPosts = 'https://wbialyamogus-001-site1.atempurl.com/api/post/userlikedposts/'
  number = 1;
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': localStorage.getItem("Authorization")!
  });
  options = { headers: this.headers };
  public event: EventPost = {
    postId: 0,
    title: '',
    description: '',
    found: false,
    image: '',
    confirmed: false,
    place: '',
    location: '',
    eventDate: '',
    day: '',
    tags: [],
    link: '',
    isLiked: false,
    likeCount: 0
  };
  constructor(private http: HttpClient) {
  }
  getAllPosts(toppings: string[], date: string | Date | null, number: number, selectedSort: string) {
    let string = "";
    for (let i = 0; i < toppings.length; i++) {
      string += "&TagFilter=" + toppings[i];
    }
    return this.http.get<PageResultModel>(this.baseUrl + "?pageSize=5" + "&pageNumber=" + number + "&sortDirection=" + selectedSort + string + "&DateFilter=" + date);
  }
  getAllTags() {
    return this.http.get<Tags[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/eventtags/');
  }
  getLikedPosts(toppings: string[], date: string | Date | null, number: number, selectedSort: string){
    return this.http.get<EventPost[]>(this.likedPosts, this.options);
  }
}
