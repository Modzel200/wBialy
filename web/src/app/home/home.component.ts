import { Component } from '@angular/core';
import {ShortEventModel} from "./shortEvent.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  shortEvents: ShortEventModel[] = [
    new ShortEventModel('tytul','opis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date()),
    new ShortEventModel('tytul','opis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date()),
    new ShortEventModel('tytul','opis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date())
  ];

}
