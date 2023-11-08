import {Component, OnInit, ViewChild} from '@angular/core';
import { PostToAdd, Tags } from '../model/user-panel.model';
import { EventPost } from 'src/app/events/model/event.model';
import { UserPanelService } from '../service/user-panel.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import {NgxGpAutocompleteDirective} from "@angular-magic/ngx-gp-autocomplete";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-add-post-form',
  templateUrl: './add-post-form.component.html',
  styleUrls: ['./add-post-form.component.scss']
})
export class AddPostFormComponent implements OnInit {
  selectedToggleValue: string = '';
  ngOnInit(): void {
    this.selectedToggleValue= 'Post';
  }

}
