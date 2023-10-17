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
  get visibleEvents(): EventPost[] {
    const wrappedIndex = this.currentIndex % this.shortEvents.length;
    if (this.isWideScreen) {
      const newEvent = this.shortEvents[wrappedIndex];
      const prevEvents = [
        this.shortEvents[(wrappedIndex - 1 + this.shortEvents.length) % this.shortEvents.length],
        this.shortEvents[(wrappedIndex - 2 + this.shortEvents.length) % this.shortEvents.length],
      ];
      return [newEvent, ...prevEvents];
    } else {
      return this.shortEvents.slice(wrappedIndex, wrappedIndex + 1);
    }
  }

  ngOnInit() {
    window.addEventListener('resize', this.onResize.bind(this));
    this.shortEventsService.getAllPosts().subscribe(response => {
      console.log(response);
      console.log(response.totalItemsCount);
      this.pageResult = response;
      this.shortEvents = this.pageResult.items;
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
  }

  nextEvent() {
    this.currentIndex = (this.currentIndex + 1) % this.shortEvents.length;
  }

}
