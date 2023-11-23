import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";
import {Tags} from "../../user-panel/model/user-panel.model";

@Injectable({
  providedIn: 'root'
})
export class EventsService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/eventposts/';
  number = 1;
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
  constructor(private http: HttpClient) {
  }
  getAllPosts(toppings: string[],date: string|Date|null,number: number)
  {
    let string = "";
    for(let i=0;i<toppings.length;i++)
    {
      string+="&TagFilter="+toppings[i];
    }
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5"+"&pageNumber="+number+"&sortDirection=0"+string+"&DateFilter="+date);
  }
  getAllTags()
  {
    return this.http.get<Tags[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/eventtags/');
  }
}
