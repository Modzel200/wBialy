import { Component } from '@angular/core';
import {EventPost} from "./event.model";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {
  events: EventPost[] = [
    new EventPost(1,'tytul','opis','https://static.android.com.pl/uploads/2022/11/Shrek-animacja-bajka.jpg',new Date(),'Bialy',1,true,'link',new Date(),['bialy','test']),
    new EventPost(1,'tytul','opis','https://static.android.com.pl/uploads/2022/11/Shrek-animacja-bajka.jpg',new Date(),'Bialy',1,true,'link',new Date(),['bialy','test'])
  ]
}
