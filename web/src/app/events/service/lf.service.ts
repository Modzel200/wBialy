import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";
import { lfPost } from "../model/lostfound.model";

@Injectable({
  providedIn: 'root'
})
export class lfService{
  baseUrl = 'https://localhost:7012/api/post/lfposts/';
  number = 1;
  public event: lfPost={
    postId: 0,
    title:'',
    description:'',
    image:'',
    confirmed: false,
    place:'',
    location:'',
    tags:[],
  };
  constructor(private http: HttpClient) {
  }
  getAllLfPosts(number: number):Observable<PageResultModel>
  {
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber="+number+"&sortBy=Title");
  }
}
