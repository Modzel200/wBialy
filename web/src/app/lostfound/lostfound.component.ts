import { Component } from '@angular/core';
import { PageResultModel } from '../events/model/pageResult.model';
import { DatePipe, ViewportScroller } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { lfPost } from '../events/model/lostfound.model';
import { lfService } from '../events/service/lf.service';

@Component({
  selector: 'app-lostfound',
  templateUrl: './lostfound.component.html',
  styleUrls: ['./lostfound.component.scss']
})
export class LostfoundComponent {
  number = 1;
  events: lfPost[] = [];
  pageResult: PageResultModel={
    items: [],
    totalPages:0,
    itemFrom:0,
    itemTo:0,
    totalItemsCount:0
  };
  constructor(private eventsService: lfService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog,private scroller: ViewportScroller) {
  }
  ngOnInit() {
    this.getAllGastroPosts();
  }

getDayName(dateStr: string | number | Date, locale: Intl.LocalesArgument)
{
    var date = new Date(dateStr);
    return date.toLocaleDateString(locale, { weekday: 'long' });        
}

 dateStr = new Date();
 day = this.getDayName(this.dateStr, "pl-PL");

 selectedToggleValue: string = this.day;

 getAllGastroPosts(){
  this.eventsService.getAllLfPosts(this.number)
    .subscribe(response => {
    this.pageResult = response;
    console.log(this.pageResult);
    if(this.pageResult.items.length>0)
    {
      this.events = this.pageResult.items;
    }
  });

}
}
