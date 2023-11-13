import { Component } from '@angular/core';
import { PostToAdd, Tags, lfPostToAdd } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { lfPost } from 'src/app/events/model/lostfound.model';
import {AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";

@Component({
  selector: 'app-add-lf-post',
  templateUrl: './add-lf-post.component.html',
  styleUrls: ['./add-lf-post.component.scss']
})
export class AddLfPostComponent {
  tags: Tags[] =[]
  allTags: Tags[] = []
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
    this.userPanelService.getAllLFTags()
      .subscribe(response=>{
        this.allTags = response;
      })
  }
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
    if(this.toppings.errors || this.place.errors || this.title.errors || this.description.errors){
      return; 
    }
    this.userPanelService.addNewLfPost(this.postToAdd).subscribe(response=>{
    });
    window.location.reload();
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
