import { Component } from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-add-event-post',
  templateUrl: './add-event-post.component.html',
  styleUrls: ['./add-event-post.component.scss']
})
export class AddEventPostComponent {
  tags: Tags[] =[{
    name: 'pub'
  }
  ]
  userEvents: EventPost[] =[];
  postToAdd: PostToAdd = {
    postId: 5,
    title: '',
    description: '',
    image: '',
    place: '',
    location:'',
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
  selectedFile: File = {} as File;
  onFileSelected(event : any){
    this.selectedFile = <File>event.target.files[0]
    console.log("test");
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
    this.userPanelService.addNewPost(this.postToAdd).subscribe(response=>{
      console.log(response);
      console.log(this.postToAdd.eventDate);
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
