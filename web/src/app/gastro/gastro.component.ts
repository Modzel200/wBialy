import { Component } from '@angular/core';
import { PageResultModel } from '../events/model/pageResult.model';
import { gastroPost } from '../events/model/gastro.model';
import { gastroService } from '../events/service/gastro.service';
import { DatePipe, ViewportScroller } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-gastro',
  templateUrl: './gastro.component.html',
  styleUrls: ['./gastro.component.scss']
})
export class GastroComponent {
  number = 1;
  events: gastroPost[] = [];
  pageResult: PageResultModel={
    items: [],
    totalPages:0,
    itemFrom:0,
    itemTo:0,
    totalItemsCount:0
  };
  constructor(private eventsService: gastroService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog,private scroller: ViewportScroller) {
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
  this.eventsService.getAllGastroPosts(this.number)
    .subscribe(response => {
    this.pageResult = response;
    console.log(this.pageResult);
    if(this.pageResult.items.length>0)
    {
      this.events = this.pageResult.items;
      this.changeDateFormat();
    }
  });

}

changeDateFormat()
{
  for(let i=0;i<this.events.length;i++)
  {
    this.events[i].day = <string>this.datePipe.transform(this.events[i].day, 'dd.MM.yyyy hh:mm');
  }
}
}

