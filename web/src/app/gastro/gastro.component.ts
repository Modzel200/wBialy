import { Component } from '@angular/core';
import { PageResultModel } from '../events/model/pageResult.model';
import { gastroPost } from '../events/model/gastro.model';
import { gastroService } from '../events/service/gastro.service';
import { DatePipe, ViewportScroller } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormControl } from "@angular/forms";
import { Tags } from "../user-panel/model/user-panel.model";
import { faFilter } from "@fortawesome/free-solid-svg-icons";
import { UserPanelService } from "../user-panel/service/user-panel.service";
import { AdminService } from '../user-panel/admin/service/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomSnackbarComponent } from '../custom-snackbar/custom-snackbar.component';

@Component({
  selector: 'app-gastro',
  templateUrl: './gastro.component.html',
  styleUrls: ['./gastro.component.scss']
})
export class GastroComponent {
  number = 1;
  events: gastroPost[] = [];
  pageResult: PageResultModel = {
    items: [],
    totalPages: 0,
    itemFrom: 0,
    itemTo: 0,
    totalItemsCount: 0
  };
  toppings = new FormControl();
  selectedToppings = [];
  selectedToppingsString: string[] = [];
  allTags: Tags[] = []
  constructor(private eventsService: gastroService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog, private scroller: ViewportScroller, private userPanelService: UserPanelService, private adminService: AdminService, private _snackBar: MatSnackBar) {
  }
  isAdmin = false;
  ngOnInit() {
    //this.getAllGastroPosts();
    this.getGastroPosts(this.day);
    this.getAllTags();
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

  getDayName(dateStr: string | number | Date, locale: Intl.LocalesArgument) {
    var date = new Date(dateStr);
    return date.toLocaleDateString(locale, { weekday: 'long' });
  }

  dateStr = new Date();
  day = this.getDayName(this.dateStr, "pl-PL");

  selectedToggleValue: string = this.day;

  getAllGastroPosts() {
    this.eventsService.getAllGastroPosts(this.number)
      .subscribe(response => {
        this.pageResult = response;
        if (this.pageResult.items.length > 0) {
          this.events = this.pageResult.items;
          //this.changeDateFormat();
        }
      });

  }
  getGastroPosts(day: string) {
    this.eventsService.getDayPosts(day, this.number, this.selectedToppingsString)
      .subscribe(response => {
        this.pageResult = response;
        this.events = this.pageResult.items;
      })
  }
  getAllTags() {
    this.eventsService.getAllGastroTags()
      .subscribe(response => {
        this.allTags = response;
      })
  }
  sendFilters() {
    this.selectedToppingsString = this.selectedToppings;
    this.getGastroPosts(this.selectedToggleValue);
  }
  clearTags() {
    this.selectedToppings = [];
    this.getGastroPosts(this.day);
  }

  truncateDescription(description: string, maxLength: number): string {
    if (description.length <= maxLength) {
      return description;
    } else {
      return description.slice(0, maxLength) + '...';
    }
  }

  protected readonly faFilter = faFilter;
}

