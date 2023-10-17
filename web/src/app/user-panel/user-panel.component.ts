import { Component } from '@angular/core';
import {UserPanelService} from "./service/user-panel.service";
import {PostToAdd, Tags} from "./model/user-panel.model";

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent {
  tags: Tags[] =[{
    name: 'pub'
  }
  ]
  postToAdd: PostToAdd = {
    title: '',
    description: '',
    image: '',
    place: '',
    day: '',
    tags: this.tags,
    link: ''
  }
  constructor(private userPanelService: UserPanelService) {
  }
  onSubmit()
  {
    this.userPanelService.addNewPost(this.postToAdd).subscribe(response=>{
      console.log(response);
    });
  }
}
