import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  isMenuOpen = false;
  isLogged = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
