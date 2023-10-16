import {Component, OnInit} from '@angular/core';
import {EventPost} from "./model/event.model";
import {EventsService} from "./service/events.service";
import {PageResultModel} from "./model/pageResult.model";
@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit{

  events: EventPost[] = [];
  pageResult: PageResultModel={
    Items: [],
    TotalPages:0,
    ItemFrom:0,
    ItemTo:0,
    TotalItemsCount:0
  };
  constructor(private eventsService: EventsService) {
  }
  ngOnInit() {
    this.getAllPosts();
  }
  getAllPosts(){
    this.eventsService.getAllPosts().subscribe(response => {
      console.log(response);
      console.log(response.Items);
      this.pageResult = response;
      console.log(this.pageResult.TotalPages);
    })
  }
  testFunc(){
    console.log(typeof this.pageResult.Items)
  }
}
