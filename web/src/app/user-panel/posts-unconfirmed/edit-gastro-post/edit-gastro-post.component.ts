import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Tags, gastroPost } from 'src/app/events/model/gastro.model';
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { BlurEvent } from '@ckeditor/ckeditor5-angular';
import { CustomSnackbarComponent } from 'src/app/custom-snackbar/custom-snackbar.component';
import { imgbbUpload } from "imgbb-image-uploader";
import { gastroPostToAdd } from '../../model/user-panel.model';
import { UserPanelService } from '../../service/user-panel.service';
import { UploadImgModel } from '../../model/uploadImg.model';



@Component({
  selector: 'app-edit-gastro-post',
  templateUrl: './edit-gastro-post.component.html',
  styleUrls: ['./edit-gastro-post.component.scss']
})
export class EditGastroPostComponent {
  tags: Tags[] = []
  allTags: Tags[] = []
  userEvents: gastroPost[] = [];
  postToAdd: gastroPost = {
    postId: 5,
    title: '',
    description: '',
    image: '',
    place: '',
    location: '',
    day: '',
    tags: this.tags,
    link: '',
    confirmed: false,
  }

  newPost: gastroPost = {
    postId: 5,
    title: '',
    description: '',
    image: '',
    place: '',
    location: '',
    day: '',
    tags: this.tags,
    link: '',
    confirmed: false,
  }
  public Editor = ClassicEditor
  public config = {
    toolbar: [
      'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote'
    ]
  };
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
    if (this.toppings.root.touched) { return this.toppings.hasError('atLeastOneSelectedValidator') ? '' : 'Wybierz conajmniej 1 kategorię'; }
    return '';
  }

  day = new FormControl('', [Validators.required, this.atLeastOneSelectedValidator.bind(this)]);
  getErrorDayMessage() {
    if (this.day.root.touched) { return this.day.hasError('atLeastOneSelectedValidator') ? '' : 'Wybierz dzien'; }
    return '';
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
    return '';
  }


  ngOnInit() {
    if (localStorage.getItem("Authorization") == null) {
      this.router.navigate(['/']);
    }
    this.postToAdd = this.userPanelService.event;
    this.newPost = {
      postId: this.postToAdd.postId,
      title: this.postToAdd.title,
      description: this.postToAdd.description,
      image: this.postToAdd.image,
      place: this.postToAdd.place,
      location: this.postToAdd.location,
      day: this.postToAdd.day,
      tags: this.postToAdd.tags,
      link: this.postToAdd.link,
      confirmed: false,
    }
    this.getAllTags();
  }
  selectedFile: File = {} as File;
  getAllTags() {
    this.userPanelService.getAllGastroTags()
      .subscribe(response => {
        this.allTags = response;
      })
  }
  onFileSelected(event: any) {
    this.selectedFile = <File>event.target.files[0]
    imgbbUpload({
      key: '0044368c0f15bd2f0120f0819f511ee9',
      image: this.selectedFile,
    })
      .then((data: UploadImgModel) => {
        this.newPost.image = data.data.url;
      })
  }

  onSubmit() {
    if (this.toppings.errors || this.place.errors || this.title.errors || this.description.errors || this.day.errors) {
      return;
    }
    for (let i = 0; i < this.selectedToppings.length; i++) {
      this.tags.push({ name: this.selectedToppings[i] });
    }
    this.newPost.tags = this.tags;
    console.log(this.newPost);
    this.userPanelService.editGastro(this.newPost, this.newPost.postId).subscribe(response => {
      if (response == null) {
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
  changeDay(event: Event) {
  }
  public handleAddressChange(place: google.maps.places.PlaceResult) {
    if (place.formatted_address != null) {
      this.newPost.place = place.formatted_address;
    }
    if (place.url != null) {
      this.newPost.location = place.url;
    }
  }

  public onReady(editor: ClassicEditor) {
  }
  public onChange({ editor }: BlurEvent) {
    this.newPost.description = editor.data.get();
  }
}