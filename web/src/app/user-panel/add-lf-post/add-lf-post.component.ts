import { Component } from '@angular/core';
import { PostToAdd, Tags, lfPostToAdd } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { lfPost } from 'src/app/events/model/lostfound.model';

@Component({
  selector: 'app-add-lf-post',
  templateUrl: './add-lf-post.component.html',
  styleUrls: ['./add-lf-post.component.scss']
})
export class AddLfPostComponent {
  tags: Tags[] =[{
    name: 'telefon'
  }
  ]
  userEvents: lfPost[] =[];
  postToAdd: lfPostToAdd = {
    postId: 5,
    title: '',
    description: '',
    image: '',
    place: '',
    location:'',
    tags: this.tags,
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
    this.userPanelService.addNewLfPost(this.postToAdd).subscribe(response=>{
    });
    window.location.reload();
  }
  public handleAddressChange(place: google.maps.places.PlaceResult) {
    // Do some stuff
    console.log(place);
    if (place.formatted_address != null) {
      this.postToAdd.place = place.formatted_address;
    }
    if (place.url != null) {
      this.postToAdd.location = place.url;
    }
  }
}
