import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EventPost} from "../model/event.model";
import {PageResultModel} from "../model/pageResult.model";
import {Observable} from "rxjs";
import { lfPost } from "../model/lostfound.model";
import {Tags} from "../../user-panel/model/user-panel.model";

@Injectable({
  providedIn: 'root'
})
export class lfService{
  baseUrl = 'https://wbialyamogus-001-site1.atempurl.com/api/post/lfposts/';
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
  getAllGastroTags()
  {
    return this.http.get<Tags[]>('https://wbialyamogus-001-site1.atempurl.com/api/post/lftags');
  }
}
