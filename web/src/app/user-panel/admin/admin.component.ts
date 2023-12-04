import {Component, OnInit} from '@angular/core';
import {EventPost} from "../../events/model/event.model";
import {AdminService} from "./service/admin.service";
import {DatePipe} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit{
  events: EventPost[] =[];

  constructor(private adminService: AdminService, private datePipe: DatePipe, private router: Router) {
  }
  ngOnInit() {
    this.getAllPostsToAccept();
  }
  getAllPostsToAccept()
  {
    this.adminService.getAllPostsToAccept()
      .subscribe(response=>{
        //console.log(this.events);
        this.events = response;
        this.changeDateFormat();
      })
  }
  changeDateFormat()
  {
    for(let i=0;i<this.events.length;i++)
    {
      this.events[i].eventDate = <string>this.datePipe.transform(this.events[i].eventDate, 'dd.MM.yyyy hh:mm');
    }
  }
  acceptPost(id:number)
  {
    this.adminService.acceptPost(id)
      .subscribe(response=>{
      this.getAllPostsToAccept();
      });
  }
  deletePost(id:number)
  {
    this.adminService.deletePost(id)
      .subscribe(response =>{
        this.getAllPostsToAccept();
      })
  }
  truncateDescription(description: string, maxLength: number): string {
    if (description.length <= maxLength) {
      return description;
    } else {
      return description.slice(0, maxLength) + '...';
    }
  }
}
