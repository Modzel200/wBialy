import { Component } from '@angular/core';
import { faPalette } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'web';
  isDarkMode: boolean = Boolean(localStorage.getItem("DarkMode"));
  faPalette=faPalette;
  toggleDarkMode(): void {
    this.isDarkMode = !this.isDarkMode;
    localStorage.setItem("DarkMode",String(this.isDarkMode));
  }
}
