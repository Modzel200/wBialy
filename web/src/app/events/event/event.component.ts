import {Component, OnInit} from '@angular/core';
import {EventPost} from "../model/event.model";
import {EventsService} from "../service/events.service";

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss']
})
export class EventComponent implements OnInit{
  constructor(private eventsService: EventsService) {
  }
  event: EventPost={
    postId:0,
    title:'',
    description:'',
    found:false,
    image:'',
    place:'',
    location:'',
    confirmed: false,
    eventDate:'',
    day:'',
    tags:[],
    link:'',
  }
  ngOnInit() {
    this.event = this.eventsService.event;
  }

  //event: EventPost = new EventPost(1,'tytul','opis','https://static.android.com.pl/uploads/2022/11/Shrek-animacja-bajka.jpg',new Date(),'Bialy',1,true,'link',new Date(),['bialy','test'], new Date())
}
