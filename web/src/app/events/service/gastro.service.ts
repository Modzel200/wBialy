import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";
import { gastroPost } from "../model/gastro.model";

@Injectable({
  providedIn: 'root'
})
export class gastroService{
  baseUrl = 'https://localhost:7012/api/post/gastroposts/';
  number = 1;
  public event: gastroPost={
    postId: 0,
    title:'',
    description:'',
    image:'',
    confirmed: false,
    place:'',
    location:'',
    day:'',
    tags:[],
    link:'',
  };
  constructor(private http: HttpClient) {
  }
  getAllGastroPosts(number: number):Observable<PageResultModel>
  {
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber="+number+"&DateFilter=");
  }
  getDayPosts(day:string, number: number)
  {
    console.log(day);
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber="+number+"&DateFilter="+day);
  }
}
