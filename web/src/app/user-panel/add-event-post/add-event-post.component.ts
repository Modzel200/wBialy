import { Component } from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import {AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialogRef} from "@angular/material/dialog";
import {AddPostFormComponent} from "../add-post-form/add-post-form.component";

@Component({
  selector: 'app-add-event-post',
  templateUrl: './add-event-post.component.html',
  styleUrls: ['./add-event-post.component.scss']
})
export class AddEventPostComponent {
  tags: Tags[] =[]
  allTags: Tags[] = []
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
  constructor(private userPanelService: UserPanelService, private router: Router, private datePipe: DatePipe, private _snackBar: MatSnackBar) {
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
  
  ngOnInit() {
    if(localStorage.getItem("Authorization")==null)
    {
      this.router.navigate(['/']);
    }
    this.getAllPosts();
    this.getAllTags();
  }
  getAllPosts()
  {
    this.userPanelService.getAllPosts()
      .subscribe(response=>{
        this.userEvents = response;
        this.changeDateFormat();
      })
  }
  getAllTags()
  {
    this.userPanelService.getAllTags()
      .subscribe(response=>{
        this.allTags = response;
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
    this.userPanelService.uploadImg(this.selectedFile).subscribe(url=>
    {
      this.postToAdd.image = url.data.url;
    }
    );
  }

  onSubmit()
  {
    if(this.toppings.errors || this.place.errors || this.title.errors || this.description.errors || this.date.errors){
      return; 
    }
    for(let i=0;i<this.selectedToppings.length;i++)
    {
      this.tags.push({name:this.selectedToppings[i]});
    }
    this.userPanelService.addNewPost(this.postToAdd).subscribe(response=>{
      if(response==null)
      {
        let snackBarRef = this._snackBar.open("Post utworzony","Zamknij");
        snackBarRef.onAction().subscribe(()=> window.location.reload());
      }
    });
  }
  public handleAddressChange(place: google.maps.places.PlaceResult) {
    if (place.formatted_address != null) {
      this.postToAdd.place = place.formatted_address;
    }
    if (place.url != null) {
      this.postToAdd.location = place.url;
    }
  }

}
