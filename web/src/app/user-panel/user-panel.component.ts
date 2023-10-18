import {Component, OnInit} from '@angular/core';
import {UserPanelService} from "./service/user-panel.service";
import {PostToAdd, Tags} from "./model/user-panel.model";
import {Router} from "@angular/router";
import {EventPost} from "../events/model/event.model";
import {PageResultModel} from "../events/model/pageResult.model";
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
    title: '',
    description: '',
    image: '',
    place: '',
    eventDate: '',
    tags: this.tags,
    link: ''
  }
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
    this.getAllPosts();
  }
  getAllPosts()
  {
    this.userPanelService.getAllPosts()
      .subscribe(response=>{
        this.userEvents = response;
        console.log(this.userEvents);
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
  onSubmit()
  {
    this.userPanelService.addNewPost(this.postToAdd).subscribe(response=>{
      console.log(response);
      console.log(this.postToAdd.eventDate);
    });
  }
  logOff()
  {
    localStorage.clear();
    window.location.reload();
  }
}
