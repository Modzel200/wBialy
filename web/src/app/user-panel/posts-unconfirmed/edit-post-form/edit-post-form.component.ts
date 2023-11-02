import {Component, OnInit} from '@angular/core';
import {EventPost} from "../../../events/model/event.model";
import {UserPanelService} from "../../service/user-panel.service";
import {Tags} from "../../model/user-panel.model";

@Component({
  selector: 'app-edit-post-form',
  templateUrl: './edit-post-form.component.html',
  styleUrls: ['./edit-post-form.component.scss']
})
export class EditPostFormComponent implements OnInit{
  tags: Tags[] =[{
    name: 'pub'
  }]
  event: EventPost={
    postId:0,
    title:'',
    description:'',
    image:'',
    place:'',
    location:'',
    confirmed: false,
    eventDate:'',
    day:'',
    tags:[],
    link:'',
  }

  constructor(private userPanelService: UserPanelService) {
  }
  ngOnInit() {
    this.event = this.userPanelService.event;
    console.log(this.event.title);
  }
  onSubmit(event: EventPost){
    this.event.tags=this.tags;
    console.log(event);
    this.userPanelService.editEvent(event,event.postId).subscribe(response=>{
      console.log(response);
    });
    //window.location.reload();
  }
  selectedFile: File = {} as File;
  onFileSelected(event : any){
    this.selectedFile = <File>event.target.files[0]
    console.log("test");
    this.userPanelService.uploadImg(this.selectedFile).subscribe(url=>
      {
        console.log(url.data.url);
        this.event.image = url.data.url;
      }
    );
  }
}
