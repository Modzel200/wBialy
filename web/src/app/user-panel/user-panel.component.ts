import {Component, OnInit} from '@angular/core';
import {UserPanelService} from "./service/user-panel.service";
import {PostToAdd, Tags} from "./model/user-panel.model";
import {Router} from "@angular/router";

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
  postToAdd: PostToAdd = {
    title: '',
    description: '',
    image: '',
    place: '',
    eventDate: '',
    tags: this.tags,
    link: ''
  }
  constructor(private userPanelService: UserPanelService, private router: Router) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
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
