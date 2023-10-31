import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import {FormsModule} from "@angular/forms";
import { EventsComponent } from './events/events.component';
import { EventComponent } from './events/event/event.component';
import { HomeComponent } from './home/home.component';
import { SignupFormComponent } from './signup-form/signup-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule, Routes} from "@angular/router";
import {DatePipe} from "@angular/common";
import { UserPanelComponent } from './user-panel/user-panel.component';
import {MatDialog, MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import { AddPostFormComponent } from './user-panel/add-post-form/add-post-form.component';
import { PostsConfirmedComponent } from './user-panel/posts-confirmed/posts-confirmed.component';
import { PostsUnconfirmedComponent } from './user-panel/posts-unconfirmed/posts-unconfirmed.component';
import { FooterComponent } from './footer/footer.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { GastroComponent } from './gastro/gastro.component';
import { EditPostFormComponent } from './user-panel/posts-unconfirmed/edit-post-form/edit-post-form.component';

const appRoutes: Routes = [
  {path: '', component: EventsComponent},
  {path: 'login', component: LoginFormComponent},
  {path: 'signup', component: SignupFormComponent},
  {path: 'account',component:UserPanelComponent},
  {path: 'event',component:EventComponent},
  {path: 'gastro',component:GastroComponent},
];
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EventsComponent,
    EventComponent,
    HomeComponent,
    SignupFormComponent,
    LoginFormComponent,
    UserPanelComponent,
    AddPostFormComponent,
    PostsConfirmedComponent,
    PostsUnconfirmedComponent,
    FooterComponent,
    GastroComponent,
    EditPostFormComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    MatDialogModule,
    MatButtonModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatButtonToggleModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
