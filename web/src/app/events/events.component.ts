import {Component, OnInit} from '@angular/core';
import {EventPost} from "./model/event.model";
import {EventsService} from "./service/events.service";
import {PageResultModel} from "./model/pageResult.model";
import {map} from 'rxjs/operators'
import {EventComponent} from "./event/event.component";
@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit{
  events: EventPost[] = [];
  pageResult: PageResultModel={
    items: [],
    totalPages:0,
    itemFrom:0,
    itemTo:0,
    totalItemsCount:0
  };
  constructor(private eventsService: EventsService) {
  }
  ngOnInit() {
    this.getAllPosts();
  }
  getAllPosts(){
    this.eventsService.getAllPosts()
      .subscribe(response => {
      console.log(response);
      console.log(response.totalItemsCount);
      this.pageResult = response;
      this.events = this.pageResult.items;
    });
  }
  testFunc(){
    console.log(this.pageResult?.totalPages);
  }
}

