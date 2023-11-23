import {Component, OnInit} from '@angular/core';
import {EventPost} from "../../../events/model/event.model";
import {UserPanelService} from "../../service/user-panel.service";
import {Tags} from "../../model/user-panel.model";
import {imgbbUpload} from "imgbb-image-uploader";
import {UploadImgModel} from "../../model/uploadImg.model";

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
  }
  onSubmit(event: EventPost){
    this.event.tags=this.tags;
    this.userPanelService.editEvent(event,event.postId).subscribe(response=>{
    });
  }
  selectedFile: File = {} as File;
  onFileSelected(event : any){
    this.selectedFile = <File>event.target.files[0]
    imgbbUpload({
      key: '0044368c0f15bd2f0120f0819f511ee9',
      image: this.selectedFile,
    })
      .then((data : UploadImgModel) => {
        this.event.image = data.data.url;
      })
  }
}
