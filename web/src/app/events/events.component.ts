import {Component, OnInit} from '@angular/core';
import {EventPost} from "./model/event.model";
import {EventsService} from "./service/events.service";
import {PageResultModel} from "./model/pageResult.model";
import {map} from 'rxjs/operators'
import {EventComponent} from "./event/event.component";
import {DatePipe} from "@angular/common";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
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
  constructor(private eventsService: EventsService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog) {
  }
  ngOnInit() {
    this.getAllPosts();
  }
  getAllPosts(){
    this.eventsService.getAllPosts()
      .subscribe(response => {
      this.pageResult = response;
      this.events = this.pageResult.items;
      this.changeDateFormat();
    });
  }
  changeDateFormat()
  {
    for(let i=0;i<this.events.length;i++)
    {
      this.events[i].eventDate = <string>this.datePipe.transform(this.events[i].eventDate, 'dd.MM.yyyy hh:mm');
    }
  }
  testFunc(){
    console.log(this.pageResult?.totalPages);
  }
  showEvent(event: EventPost){
    this.eventsService.event = event;
    const dialogRef = this.dialog.open(EventComponent,{
      height:'80%',
      autoFocus: false,
    });
    //this.router.navigate(['/event']);
  }
}

