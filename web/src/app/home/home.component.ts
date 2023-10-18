import { Component, OnInit } from '@angular/core';
import {EventPost} from "../events/model/event.model";
import {ShortEventsService} from "./service/home.service";
import {PageResultModel} from "../events/model/pageResult.model";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  shortEvents: EventPost[] = [];
  eventsToShow: EventPost[] = [];
  pageResult: PageResultModel={
    items: [],
    totalPages:0,
    itemFrom:0,
    itemTo:0,
    totalItemsCount:0
  };
  currentIndex: number = 0;
  isWideScreen: boolean = window.innerWidth >= 800;
  constructor(private shortEventsService:ShortEventsService, private datePipe: DatePipe) {
  }
  ngOnInit() {
    window.addEventListener('resize', this.onResize.bind(this));
    this.shortEventsService.getAllPosts().subscribe(response => {
      this.pageResult = response;
      this.shortEvents = this.pageResult.items;
      // this.eventsToShow = this.shortEvents.slice(this.indexDown,this.indexUp);
      if(this.isWideScreen){
        this.eventsToShow = [this.shortEvents[this.currentIndex],this.shortEvents[this.currentIndex+1],this.shortEvents[this.currentIndex+2]];
      }else{
        this.eventsToShow = [this.shortEvents[this.currentIndex]];
      }
      this.changeDateFormat();
    })

  }
  changeDateFormat()
  {
    for(let i=0;i<this.shortEvents.length;i++)
    {
      this.shortEvents[i].eventDate = <string>this.datePipe.transform(new Date(), 'dd.MM.yyyy hh:mm');
    }
  }

  onResize() {
    this.isWideScreen = window.innerWidth >= 800;
  }

  prevEvent() {
    this.currentIndex = (this.currentIndex - 1 + this.shortEvents.length) % this.shortEvents.length;
    this.isWideScreen 
    ? this.eventsToShow = [this.shortEvents[this.currentIndex%this.shortEvents.length],this.shortEvents[(this.currentIndex+1)%this.shortEvents.length],this.shortEvents[(this.currentIndex+2)%this.shortEvents.length]] 
    : this.eventsToShow = [this.shortEvents[this.currentIndex - 1]];
  }

  nextEvent() {
    this.currentIndex = (this.currentIndex + 1) % this.shortEvents.length;
    this.isWideScreen 
    ? this.eventsToShow = [this.shortEvents[this.currentIndex%this.shortEvents.length],this.shortEvents[(this.currentIndex+1)%this.shortEvents.length],this.shortEvents[(this.currentIndex+2)%this.shortEvents.length]]
    : this.eventsToShow = [this.shortEvents[this.currentIndex + 1]];
  }

}
