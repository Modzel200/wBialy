import { Component, OnInit } from '@angular/core';
import { EventPost } from "../model/event.model";
import { EventsService } from "../service/events.service";
import { UserPanelService } from 'src/app/user-panel/service/user-panel.service';
import { AdminService } from 'src/app/user-panel/admin/service/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomSnackbarComponent } from 'src/app/custom-snackbar/custom-snackbar.component';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss']
})
export class EventComponent implements OnInit {
  constructor(private eventsService: EventsService, private userPanelService: UserPanelService, private adminService: AdminService, private _snackBar: MatSnackBar) {
  }
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
  }
  isAdmin = false;
  ngOnInit() {
    this.event = this.eventsService.event;
    this.userPanelService.isAdmin()
      .subscribe(response => {
        this.isAdmin = response;
      })
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
