import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {PageResultModel} from "../../events/model/pageResult.model";

@Injectable({
  providedIn: 'root'
})
export class ShortEventsService{
  baseUrl = 'https://localhost:7012/api/post/eventposts/'
  constructor(private http: HttpClient) {
  }
  getAllPosts():Observable<PageResultModel>
  {
    return this.http.get<PageResultModel>(this.baseUrl+"?pageSize=5&pageNumber=1&sortDirection=0");
  }
}
