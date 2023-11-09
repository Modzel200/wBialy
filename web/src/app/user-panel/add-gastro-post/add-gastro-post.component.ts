import { Component } from '@angular/core';
import { PostToAdd, Tags, gastroPostToAdd, lfPostToAdd } from '../model/user-panel.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { gastroPost } from 'src/app/events/model/gastro.model';
import {EventPost} from "../../events/model/event.model";

@Component({
  selector: 'app-add-gastro-post',
  templateUrl: './add-gastro-post.component.html',
  styleUrls: ['./add-gastro-post.component.scss']
})
export class AddGastroPostComponent {
  tags: Tags[] =[{
    name: 'burger'
  }
  ]
  userEvents: gastroPost[] =[];
  postToAdd: gastroPostToAdd = {
    postId: 5,
    title: '',
    description: '',
    image: '',
    place: '',
    location:'',
    day: '',
    tags: this.tags,
    link: '',
  }
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
  }
  selectedFile: File = {} as File;
  onFileSelected(event : any){
    this.selectedFile = <File>event.target.files[0]
    this.userPanelService.uploadImg(this.selectedFile).subscribe(url=>
    {
      console.log(url.data.url);
      this.postToAdd.image = url.data.url;
    }
    );
  }

  onSubmit()
  {
    console.log(this.postToAdd)
    this.userPanelService.addNewGastroPost(this.postToAdd).subscribe(response=>{
      console.log(response);
    });
    window.location.reload();
  }
  changeDay(event: Event)
  {
    console.log(event);
  }
  public handleAddressChange(place: google.maps.places.PlaceResult) {
    console.log(place);
    if (place.formatted_address != null) {
      this.postToAdd.place = place.formatted_address;
    }
    if (place.url != null) {
      this.postToAdd.location = place.url;
    }
  }
}
