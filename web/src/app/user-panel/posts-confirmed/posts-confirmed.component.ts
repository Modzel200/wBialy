import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { EditPostFormComponent } from "../posts-unconfirmed/edit-post-form/edit-post-form.component";
import { Dialog } from "@angular/cdk/dialog";
import { EditGastroPostComponent } from '../posts-unconfirmed/edit-gastro-post/edit-gastro-post.component';
import { EditLfPostComponent } from '../posts-unconfirmed/edit-lf-post/edit-lf-post.component';

@Component({
  selector: 'app-posts-confirmed',
  templateUrl: './posts-confirmed.component.html',
  styleUrls: ['./posts-confirmed.component.scss']
})
export class PostsConfirmedComponent implements OnChanges {
  tags: Tags[] = [{
    name: 'pub'
  }
  ]
  userEvents: EventPost[] = [];
  postToAdd: PostToAdd = {
    postId: 0,
    title: '',
    description: '',
    image: '',
    place: '',
    location: '',
    eventDate: '',
    tags: this.tags,
    link: ''
  }
  @Input() type = '';
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe, private dialog: Dialog) {
  }
  isDarkMode = false;
  ngOnInit() {
    console.log(this.type);
    if (localStorage.getItem("Authorization") == null) {
      this.router.navigate(['/']);
    }
    this.useFunction();
    if (localStorage.getItem("DarkMode") == 'true') {
      this.isDarkMode = true;
    }
    else {
      this.isDarkMode = false;
    }
  }
  ngOnChanges() {
    this.useFunction()
  }
  useFunction() {
    if (this.type === "lf") {

      this.getAllLF();
    }
    else if (this.type === "gastro") {
      this.getAllGastro();
    }
    else {
      this.getAllPosts();
    }
  }

  getAllPosts() {
    this.userPanelService.getAllPosts()
      .subscribe(response => {
        this.userEvents = response;
        this.changeDateFormat();
      })
  }

  changeDateFormat() {
    for (let i = 0; i < this.userEvents.length; i++) {
      this.userEvents[i].eventDate = <string>this.datePipe.transform(this.userEvents[i].eventDate, 'dd.MM.yyyy hh:mm');
    }
  }

  deleteEvent(id: number) {
    this.userPanelService.deleteEvent(id).subscribe(response => {
      this.useFunction();
    })
  }

  editEvent(event: EventPost) {
    const classmode = this.isDarkMode ? 'dark-mode' : '';
    this.userPanelService.event = event;
    if (this.type === "lf") {

      const dialogRef = this.dialog.open(EditLfPostComponent, {
        autoFocus: false,
        panelClass: classmode,
      });
    }
    else if (this.type === "gastro") {
      const dialogRef = this.dialog.open(EditGastroPostComponent, {
        autoFocus: false,
        panelClass: classmode,
      });
    }
    else {
      const dialogRef = this.dialog.open(EditPostFormComponent, {
        autoFocus: false,
        panelClass: classmode,
      });
    }

  }
  getAllGastro() {
    console.log("gastro");
    this.userPanelService.getAllGastro()
      .subscribe(response => {
        console.log(response);
        this.userEvents = response;
      })
  }
  getAllLF() {
    console.log("lf");
    this.userPanelService.getAllLF()
      .subscribe(response => {
        console.log(response);
        this.userEvents = response;
      })
  }
  changeType() {

  }

  truncateDescription(description: string, maxLength: number): string {
    if (description.length <= maxLength) {
      return description;
    } else {
      return description.slice(0, maxLength) + '...';
    }
  }
}
