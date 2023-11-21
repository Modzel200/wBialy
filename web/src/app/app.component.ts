import {Component, OnInit} from '@angular/core';
import { faPalette } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'web';
  isDarkMode: boolean = true;
  faPalette=faPalette;
  toggleDarkMode(): void {
    this.isDarkMode = !this.isDarkMode;
    localStorage.setItem("DarkMode",String(this.isDarkMode));
  }
  ngOnInit() {
    if(localStorage.getItem("DarkMode")=='true')
    {
      this.isDarkMode = true;
    }
    else
    {
      this.isDarkMode = false;
    }
  }
}
