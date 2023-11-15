import {Component, OnInit, Input} from '@angular/core';
import { AddPostFormComponent } from '../user-panel/add-post-form/add-post-form.component';
import { Dialog } from '@angular/cdk/dialog';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit{
  constructor(private dialog : Dialog){

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
      height:'1000px',
      autoFocus: false,
      panelClass: classmode,
    });
    //this.router.navigate(['/event']);
  }
}
