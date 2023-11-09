import { Component } from '@angular/core';
import { PostToAdd, Tags, gastroPostToAdd, lfPostToAdd } from '../model/user-panel.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { gastroPost } from 'src/app/events/model/gastro.model';
import {EventPost} from "../../events/model/event.model";
import {FormControl} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-add-gastro-post',
  templateUrl: './add-gastro-post.component.html',
  styleUrls: ['./add-gastro-post.component.scss']
})
export class AddGastroPostComponent {
  tags: Tags[] =[]
  allTags: Tags[] = []
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
  toppings = new FormControl();
  selectedToppings = [];
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe, private _snackBar: MatSnackBar) {
  }
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
    this.getAllTags();
  }
  selectedFile: File = {} as File;
  getAllTags()
  {
    this.userPanelService.getAllGastroTags()
      .subscribe(response=>{
        this.allTags = response;
      })
  }
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
    for(let i=0;i<this.selectedToppings.length;i++)
    {
      this.tags.push({name:this.selectedToppings[i]});
    }
    console.log(this.tags);
    console.log(this.postToAdd)
    this.userPanelService.addNewGastroPost(this.postToAdd).subscribe(response=>{
      console.log(response);
      if(response==null)
      {
        //window.location.reload();
        let snackBarRef = this._snackBar.open("Post utworzony","Zamknij");
        snackBarRef.onAction().subscribe(()=> window.location.reload());
      }
    });
    //window.location.reload();
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
