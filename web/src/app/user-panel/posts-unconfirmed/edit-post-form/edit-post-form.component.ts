import {Component, OnInit} from '@angular/core';
import {EventPost} from "../../../events/model/event.model";
import {UserPanelService} from "../../service/user-panel.service";
import {Tags} from "../../model/user-panel.model";
import {imgbbUpload} from "imgbb-image-uploader";
import {UploadImgModel} from "../../model/uploadImg.model";
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { BlurEvent } from '@ckeditor/ckeditor5-angular';
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { CustomSnackbarComponent } from 'src/app/custom-snackbar/custom-snackbar.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-edit-post-form',
  templateUrl: './edit-post-form.component.html',
  styleUrls: ['./edit-post-form.component.scss']
})
export class EditPostFormComponent implements OnInit{
  tags: Tags[] =[]
  allTags: Tags[] = []
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
    tags: this.tags,
    link:'',
  }

  newEvent: EventPost={
    postId:0,
    title:'',
    description:'',
    image:'',
    place:'',
    location:'',
    confirmed: false,
    eventDate:'',
    day:'',
    tags: this.tags,
    link:'',
  }

  public Editor = ClassicEditor
  public config = {
    toolbar: [
      'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote'
    ]
  };

  constructor(private userPanelService: UserPanelService, private _snackBar: MatSnackBar) {
  }
  ngOnInit() {
    this.getAllTags();
    this.event = this.userPanelService.event;
    this.newEvent =  {
      postId:this.event.postId,
      title:this.event.title,
      description:this.event.description,
      image:this.event.image,
      place:this.event.place,
      location:this.event.location,
      confirmed: false,
      eventDate:this.event.eventDate,
      day:this.event.day,
      tags: this.event.tags,
      link:this.event.link,
    }
  }

  onSubmit(newEvent: EventPost){
    if(this.toppings.errors || this.place.errors || this.title.errors || this.description.errors || this.date.errors){
      return;
    }
    for(let i=0;i<this.selectedToppings.length;i++)
    {
      this.tags.push({name:this.selectedToppings[i]});
    }
    this.newEvent.tags=this.tags;
    console.log(newEvent);
    this.userPanelService.editEvent(newEvent,newEvent.postId).subscribe(response=>{
      if(response==null)
      {
        this._snackBar.openFromComponent(CustomSnackbarComponent, {
          panelClass: ['snackbar'],
          data: { message: 'Post został edytowany' },
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'top',
        });
      }
    });
  }
  selectedFile: File = {} as File;
  onFileSelected(newEvent : any){
    this.selectedFile = <File>newEvent.target.files[0]
    imgbbUpload({
      key: '0044368c0f15bd2f0120f0819f511ee9',
      image: this.selectedFile,
    })
      .then((data : UploadImgModel) => {
        this.newEvent.image = data.data.url;
      })
  }

  public handleAddressChange(place: google.maps.places.PlaceResult) {
    if (place.formatted_address != null) {
      this.newEvent.place = place.formatted_address;
    }
    if (place.url != null) {
      this.newEvent.location = place.url;
    }
  }

  public onReady(editor: ClassicEditor) {
  }
  public onChange({ editor }: BlurEvent) {
  this.newEvent.description = editor.data.get();
  }

  atLeastOneSelectedValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const selectedOptions = control.value;

      if (!selectedOptions || selectedOptions.length === 0) {
        return { atLeastOneSelected: true };
      }
      return null;
    };
  }

  toppings = new FormControl('', [Validators.required, this.atLeastOneSelectedValidator.bind(this)]);
  selectedToppings = [];
  getErrorToppingsMessage() {
    if(this.toppings.root.touched) {return this.toppings.hasError('atLeastOneSelectedValidator') ? '' : 'Wybierz conajmniej 1 kategorię';}
    return ;
  }

  title = new FormControl('title', [
    (c: AbstractControl) => Validators.required(c),
    Validators.pattern(/^.{1,50}$/),
  ]);
  getErrorTitleMessage() {
    if (this.title.hasError('required') && this.title.root.touched) {
      return 'Musisz wprowadzić tytuł';
    }
    return this.title.hasError('pattern') ? 'Za długi tytuł' : '';
  }

  description = new FormControl('description', [
    (c: AbstractControl) => Validators.required(c),
    Validators.pattern(/^.{1,600}$/),
  ]);
  getErrorDescriptionMessage() {
    if (this.description.hasError('required') && this.description.root.touched) {
      return 'Musisz wprowadzić opis';
    }
    return this.description.hasError('pattern') ? 'Za długi opis' : '';
  }

  place = new FormControl('place', Validators.required);
  getErrorPlaceMessage() {
    if (this.place.hasError('required') && this.place.root.touched) {
      return 'Musisz wprowadzić miejsce';
    }
    return;
  }

  date = new FormControl('date', Validators.required);
  getErrorDateMessage() {
    if (this.date.hasError('required') && this.date.root.touched) {
      return 'Musisz wprowadzić datę';
    }
    return;
  }
  getAllTags()
  {
    this.userPanelService.getAllTags()
      .subscribe(response=>{
        this.allTags = response;
      })
  }
}
