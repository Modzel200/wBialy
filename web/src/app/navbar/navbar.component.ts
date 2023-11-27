import {Component, OnInit, Input, HostListener} from '@angular/core';
import { AddPostFormComponent } from '../user-panel/add-post-form/add-post-form.component';
import { Dialog } from '@angular/cdk/dialog';
import { faUser, faRightFromBracket, faPlus ,faUserPlus, faSignIn } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit{
  constructor(private dialog : Dialog){

  }

  faUser = faUser;
  faRightFromBracket = faRightFromBracket;
  faPlus = faPlus;
  faUserPlus= faUserPlus;
  faSignIn=faSignIn;

  isSmallScreen = window.innerWidth <= 1000;

  @HostListener('window:resize', ['$event'])
  onResize(event:any) {
    this.isSmallScreen = window.innerWidth <= 1000;
  }

  @Input() 'isDarkMode' : boolean;
  isMenuOpen = false;
  isLogged = localStorage.getItem('Authorization');
  ngOnInit() {
    this.isLogged = localStorage.getItem('Authorization');
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;

  }

  logOff()
  {
    localStorage.removeItem("Authorization");
    window.location.reload();
  }
  showEvent(){
    const classmode = this.isDarkMode ? 'dark-mode' : '';
    const dialogRef = this.dialog.open(AddPostFormComponent,{
      autoFocus: false,
      panelClass: classmode,
    });
    //this.router.navigate(['/event']);
  }
}
