import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class EventsService{
  baseUrl = 'https://localhost:7012/api/post/eventposts/';
  public event: EventPost={
    title:'',
    description:'',
    image:'',
    confirmed: false,
    place:'',
    eventDate:'',
    day:'',
    tags:[],
    link:'',
  };
  constructor(private http: HttpClient) {
  }
  getAllPosts():Observable<PageResultModel>
  {
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber=1&sortBy=Title");
  }
}
