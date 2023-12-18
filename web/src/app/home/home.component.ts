import { Component, HostListener, OnInit } from '@angular/core';
import { EventPost } from "../events/model/event.model";
import { ShortEventsService } from "./service/home.service";
import { PageResultModel } from "../events/model/pageResult.model";
import { DatePipe } from "@angular/common";
import { EventComponent } from "../events/event/event.component";
import { EventsService } from "../events/service/events.service";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  shortEvents: EventPost[] = [];
  eventsToShow: EventPost[] = [];
  pageResult: PageResultModel = {
    items: [],
    totalPages: 0,
    itemFrom: 0,
    itemTo: 0,
    totalItemsCount: 0
  };
  currentIndex: number = 0;
  isWideScreen: boolean = window.innerWidth >= 800;
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.isWideScreen = window.innerWidth >= 800;
  }
  constructor(private shortEventsService: ShortEventsService, private datePipe: DatePipe, private eventsService: EventsService, public dialog: MatDialog) {
  }
  ngOnInit() {
    window.addEventListener('resize', this.onResize.bind(this));
    this.shortEventsService.getAllPosts().subscribe(response => {
      this.pageResult = response;
      this.shortEvents = this.pageResult.items;
      // this.eventsToShow = this.shortEvents.slice(this.indexDown,this.indexUp);
      if (this.isWideScreen) {
        this.eventsToShow = [this.shortEvents[this.currentIndex], this.shortEvents[this.currentIndex + 1], this.shortEvents[this.currentIndex + 2]];
      } else {
        this.eventsToShow = [this.shortEvents[this.currentIndex]];
      }
      this.changeDateFormat();
    })

  }
  changeDateFormat() {
    for (let i = 0; i < this.shortEvents.length; i++) {
      this.shortEvents[i].eventDate = <string>this.datePipe.transform(new Date(), 'dd.MM.yyyy hh:mm');
    }
  }

  prevEvent() {
    this.currentIndex = (this.currentIndex - 1 + this.shortEvents.length) % this.shortEvents.length;
    this.isWideScreen
      ? this.eventsToShow = [this.shortEvents[this.currentIndex % this.shortEvents.length], this.shortEvents[(this.currentIndex + 1) % this.shortEvents.length], this.shortEvents[(this.currentIndex + 2) % this.shortEvents.length]]
      : this.eventsToShow = [this.shortEvents[this.currentIndex - 1]];
  }

  nextEvent() {
    this.currentIndex = (this.currentIndex + 1) % this.shortEvents.length;
    this.isWideScreen
      ? this.eventsToShow = [this.shortEvents[this.currentIndex % this.shortEvents.length], this.shortEvents[(this.currentIndex + 1) % this.shortEvents.length], this.shortEvents[(this.currentIndex + 2) % this.shortEvents.length]]
      : this.eventsToShow = [this.shortEvents[this.currentIndex + 1]];
  }
  showEvent(event: EventPost) {
    const classmode = localStorage.getItem('DarkMode') === 'true' ? 'dark-mode' : '';
    this.eventsService.event = event;
    const dialogRef = this.dialog.open(EventComponent, {
      autoFocus: false,
      panelClass: classmode
    });
    //this.router.navigate(['/event']);
  }

  truncateDescription(description: string, maxLength: number): string {
    if (description.length <= maxLength) {
      return description;
    } else {
      return description.slice(0, maxLength) + '...';
    }
  }
}
