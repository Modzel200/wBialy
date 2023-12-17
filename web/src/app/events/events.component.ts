import {Component, OnInit, } from '@angular/core';
import {EventPost} from "./model/event.model";
import {EventsService} from "./service/events.service";
import {PageResultModel} from "./model/pageResult.model";
import {map} from 'rxjs/operators'
import {EventComponent} from "./event/event.component";
import {DatePipe, ViewportScroller} from "@angular/common";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import { FormControl } from '@angular/forms';
import { UserPanelService } from '../user-panel/service/user-panel.service';
import { Tags } from '../user-panel/model/user-panel.model';
import { faFilter } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit{
  number = 1;
  allTags: Tags[] = []
  events: EventPost[] = [];
  pageResult: PageResultModel={
    items: [],
    totalPages:0,
    itemFrom:0,
    itemTo:0,
    totalItemsCount:0
  };
  toppings = new FormControl();
  selectedToppings = [];
  selectedToppingsString:string[] = [];
  faFilter = faFilter;

  constructor(private eventsService: EventsService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog,private scroller: ViewportScroller,private userPanelService: UserPanelService,) {
  }
  ngOnInit() {
    this.getAllPosts();
    this.getAllTags();
  }
  canGoNext: boolean = false;
  canGoPrev: boolean = false;
  classmode = localStorage.getItem('DarkMode') === 'true'? 'dark-mode' : '';
  selected: Date | null | string = null;

  formatDate(){
    this.selected = this.datePipe.transform(this.selected, 'dd.MM.yyyy');
  }
  getAllTags()
  {
    this.eventsService.getAllTags()
      .subscribe(response=>{
        this.allTags = response;
      })
  }

  getAllPosts(){
    this.canGoNextPage();
    this.canGoPrevPage();
    this.eventsService.getAllPosts(this.selectedToppingsString,this.selected,this.number)
      .subscribe(response => {
      this.pageResult = response;
      this.events = this.pageResult.items;
      this.changeDateFormat();
    });
  }

  sendFilters(){
    this.selectedToppingsString = this.selectedToppings;
    this.getAllPosts();
  }
  clearTags()
  {
    this.selectedToppings = [];
    this.selected = "";
    this.getAllPosts();
  }
  canGoNextPage(): void {
    this.eventsService.getAllPosts(this.selectedToppingsString,this.selected,(this.number) + 1)
      .subscribe(response => {
        this.canGoNext = response.items.length === 0 ? true : false;
      });
  }

  canGoPrevPage(): void {
    this.canGoPrev = this.number === 1? true : false;
  }

  nextPage(){
    this.number++;
    this.getAllPosts();
    this.scroller.scrollToAnchor("eventsScroll");
  }
  backPage(){
    this.number--;
    this.getAllPosts();
    this.scroller.scrollToAnchor("eventsScroll");
  }
  changeDateFormat()
  {
    for(let i=0;i<this.events.length;i++)
    {
      this.events[i].eventDate = <string>this.datePipe.transform(this.events[i].eventDate, 'dd.MM.yyyy hh:mm');
    }
  }
  showEvent(event: EventPost){
    const classmode = localStorage.getItem('DarkMode') === 'true'? 'dark-mode' : '';
    this.eventsService.event = event;
    const dialogRef = this.dialog.open(EventComponent,{
      height:'80%',
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

