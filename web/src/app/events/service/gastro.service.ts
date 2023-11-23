import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";
import { gastroPost } from "../model/gastro.model";
import {Tags} from "../../user-panel/model/user-panel.model";

@Injectable({
  providedIn: 'root'
})
export class gastroService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/gastroposts/';
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
  getDayPosts(day:string, number: number,toppings:string[])
  {
    let string = "";
    for(let i=0;i<toppings.length;i++)
    {
      string+="&TagFilter="+toppings[i];
    }
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber="+number+"&DateFilter="+day+string);
  }
  getAllGastroTags()
  {
    return this.http.get<Tags[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/gastrotags');
  }

}
