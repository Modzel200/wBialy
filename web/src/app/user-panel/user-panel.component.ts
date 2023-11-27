import {Component, OnInit} from '@angular/core';
import {UserPanelService} from "./service/user-panel.service";
import {PostToAdd, Tags} from "./model/user-panel.model";
import {Router} from "@angular/router";
import {EventPost} from "../events/model/event.model";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent implements OnInit{
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
    location: '',
    eventDate: '',
    tags: this.tags,
    link: ''
  }
  isAdmin = true;
  selectedValue = "";
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
    this.getAllPosts();
    this.userPanelService.isAdmin()
      .subscribe(response =>{
        this.isAdmin = response;
      })
  }
  getAllPosts()
  {
    this.userPanelService.getAllPosts()
      .subscribe(response=>{
        console.log(response);
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
  changeType()
  {
    console.log(this.selectedValue);
  }
}
