import { Component, OnInit } from '@angular/core';
import { EventPost } from "../model/event.model";
import { EventsService } from "../service/events.service";
import { UserPanelService } from 'src/app/user-panel/service/user-panel.service';
import { AdminService } from 'src/app/user-panel/admin/service/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomSnackbarComponent } from 'src/app/custom-snackbar/custom-snackbar.component';
import { faHeart } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss']
})
export class EventComponent implements OnInit {
  constructor(private eventsService: EventsService, private userPanelService: UserPanelService, private adminService: AdminService, private _snackBar: MatSnackBar) {
  }
  faHeart = faHeart;

  event: EventPost = {
    postId: 0,
    title: '',
    description: '',
    found: false,
    image: '',
    place: '',
    location: '',
    confirmed: false,
    eventDate: '',
    day: '',
    tags: [],
    link: '',
    isLiked: false,
    likeCount: 0
  }
  tempEvent = this.event;
  likeCount = 0;
  isAdmin = false;
  isLogged = false;
  isLiked = false;
  hour = '';
  date = '';
  ngOnInit() {
    this.event = this.eventsService.event;
    this.likeCount = this.event.likeCount;
    if (localStorage.getItem("Authorization") != null) {
      this.isLogged = true;
    }
    if (this.isLogged) {
      this.userPanelService.isAdmin()
        .subscribe(response => {
          this.isAdmin = response;
        })
      this.userPanelService.isLikedPost(this.event.postId).subscribe(response => {
        this.tempEvent = response as EventPost;
        this.isLiked = this.tempEvent.isLiked;
        const [date, time] = this.event.eventDate.split(' ')
        console.log([date, time])
        this.date = date;
        this.hour = time;
      });
    }
  }

  toggleLike() {
    this.isLiked = !this.isLiked;
    this.isLiked ? this.likeCount = this.likeCount + 1 : this.likeCount = this.likeCount - 1;
    this.userPanelService.likePost(this.event.postId).subscribe(response => {
      console.log(response);
    });
  }

  deletePost(id: number) {
    this.adminService.deletePost(id).subscribe(response => {
      console.log(response);
    })
    console.log(id);
    this.openCustomSnackbar("Post usunięty");
  }

  openCustomSnackbar(message: string): void {
    this._snackBar.openFromComponent(CustomSnackbarComponent, {
      panelClass: ['snackbar'],
      data: { message },
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }
  //event: EventPost = new EventPost(1,'tytul','opis','https://static.android.com.pl/uploads/2022/11/Shrek-animacja-bajka.jpg',new Date(),'Bialy',1,true,'link',new Date(),['bialy','test'], new Date())
}
