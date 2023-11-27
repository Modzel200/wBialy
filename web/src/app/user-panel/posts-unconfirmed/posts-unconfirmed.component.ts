import { Component } from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import {AddPostFormComponent} from "../add-post-form/add-post-form.component";
import {Dialog} from "@angular/cdk/dialog";
import {EditPostFormComponent} from "./edit-post-form/edit-post-form.component";

@Component({
  selector: 'app-posts-unconfirmed',
  templateUrl: './posts-unconfirmed.component.html',
  styleUrls: ['./posts-unconfirmed.component.scss']
})
export class PostsUnconfirmedComponent {
  tags: Tags[] =[{
    name: 'pub'
  }
  ]
  userEvents: EventPost[] =[];
  postToAdd: PostToAdd = {
    postId: 0,
    title: '',
    description: '',
    image: '',
    place: '',
    location:'',
    eventDate: '',
    tags: this.tags,
    link: ''
  }
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe, private dialog: Dialog) {
  }
  isDarkMode = false;
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
    this.getAllPosts();
    if(localStorage.getItem("DarkMode")=='true')
    {
      this.isDarkMode = true;
    }
    else
    {
      this.isDarkMode = false;
    }
  }
  getAllPosts()
  {
    this.userPanelService.getAllPosts()
      .subscribe(response=>{
        this.userEvents = response;
        this.changeDateFormat();
      })
  }
  changeDateFormat()
  {
    for(let i=0;i<this.userEvents.length;i++)
    {
      this.userEvents[i].eventDate = <string>this.datePipe.transform(this.userEvents[i].eventDate, 'dd.MM.yyyy hh:mm');
    }
  }

  deleteEvent(id: number){
    this.userPanelService.deleteEvent(id).subscribe(response=>{
      this.getAllPosts();
    })
  }
  // editEvent(event: EventPost)
  // {
  //   this.userPanelService.editEvent(event)
  // }
  editEvent(event: EventPost){
    const classmode = this.isDarkMode ? 'dark-mode' : '';
    this.userPanelService.event = event;
    const dialogRef = this.dialog.open(EditPostFormComponent, {
      autoFocus: false,
      panelClass: classmode,
    });
    //this.router.navigate(['/event']);
  }
  // parseToText(text: string)
  // {
  //   var doc = new DOMParser().parseFromString(text,"text/html");
  //   console.log(doc.body.innerHTML)
  //   return doc.body.innerHTML
  // }
}
