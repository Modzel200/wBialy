import { Component } from '@angular/core';
import { PageResultModel } from '../events/model/pageResult.model';
import { DatePipe, ViewportScroller } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { lfPost } from '../events/model/lostfound.model';
import { lfService } from '../events/service/lf.service';
import { faFilter } from "@fortawesome/free-solid-svg-icons";
import { FormControl } from "@angular/forms";
import { Tags } from "../user-panel/model/user-panel.model";
import { MatSnackBar } from '@angular/material/snack-bar';
import { AdminService } from '../user-panel/admin/service/admin.service';
import { UserPanelService } from '../user-panel/service/user-panel.service';
import { CustomSnackbarComponent } from '../custom-snackbar/custom-snackbar.component';

@Component({
  selector: 'app-lostfound',
  templateUrl: './lostfound.component.html',
  styleUrls: ['./lostfound.component.scss']
})
export class LostfoundComponent {
  number = 1;
  events: lfPost[] = [];
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
  selectedValue: string = 'zgubione';
  constructor(private eventsService: lfService, private datePipe: DatePipe, private router: Router, public dialog: MatDialog, private scroller: ViewportScroller, private userPanelService: UserPanelService, private adminService: AdminService, private _snackBar: MatSnackBar) {
  }
  isAdmin = false;
  ngOnInit() {
    this.getAllLFPosts();
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

  truncateDescription(description: string, maxLength: number): string {
    if (description.length <= maxLength) {
      return description;
    } else {
      return description.slice(0, maxLength) + '...';
    }
  }

  getAllLFPosts() {
    this.eventsService.getAllLfPosts(this.number, this.selectedValue, this.selectedToppingsString)
      .subscribe(response => {
        this.pageResult = response;
        this.events = this.pageResult.items;
      });

  }
  getAllTags() {
    this.eventsService.getAllGastroTags()
      .subscribe(response => {
        this.allTags = response;
      })
  }
  sendFilters() {
    this.selectedToppingsString = this.selectedToppings;
    this.getAllLFPosts();
  }
  clearTags() {
    this.selectedToppings = [];
    this.getAllLFPosts();
  }
  changeType() {
    this.getAllLFPosts();
  }
  protected readonly faFilter = faFilter;
}
