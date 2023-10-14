import { Component } from '@angular/core';
import {ShortEventModel} from "./shortEvent.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  shortEvents: ShortEventModel[] = [
    new ShortEventModel('tytul','opisopisopis opis opisopisopis opis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date()),
    new ShortEventModel('tytul','opisopisopis opisopisopisopisopisopisopisopisopis opisopisopisopisopisopisopisopis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date()),
    new ShortEventModel('tytul','opisopisopisopis opisopisopisopis opisopisopisopisopis opisopisopis','https://upload.wikimedia.org/wikipedia/commons/7/7a/Rynek_Ko%C5%9Bciuszki%2C_Bia%C5%82ystok_%282%29.jpg',new Date())
  ];

  currentIndex: number = 0;
  isWideScreen: boolean = window.innerWidth >= 800;

  get visibleEvents(): ShortEventModel[] {
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
