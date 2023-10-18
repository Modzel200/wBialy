import { Component } from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-posts-confirmed',
  templateUrl: './posts-confirmed.component.html',
  styleUrls: ['./posts-confirmed.component.scss']
})
export class PostsConfirmedComponent {
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
    console.log(id);
    this.userPanelService.deleteEvent(id).subscribe(response=>{
      this.getAllPosts();
    })
  }

}
