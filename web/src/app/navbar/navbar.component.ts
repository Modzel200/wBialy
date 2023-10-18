import {Component, OnInit} from '@angular/core';
import { AddPostFormComponent } from '../user-panel/add-post-form/add-post-form.component';
import { Dialog } from '@angular/cdk/dialog';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{
  constructor(private dialog : Dialog){
    
  }
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
    localStorage.clear();
    window.location.reload();
  }

  showEvent(){
    const dialogRef = this.dialog.open(AddPostFormComponent,{
      height:'80%',
      autoFocus: false,
    });
    //this.router.navigate(['/event']);
  }
}
