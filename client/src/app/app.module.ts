import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberlistComponent } from './members/memberlist.component';
import { MemberCardComponent } from './member-card/member-card.component';
import { JwtInterceptor } from 'interceptors/jwt.interceptor';
import { MemberPageComponent } from './member-page/member-page.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { AppPhotoComponent } from './app-photo/app-photo.component';
import { FileUploadModule } from 'ng2-file-upload';
import { FormserviceComponent } from './_formservice/formservice/formservice.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { LikesListComponent } from './likes-list/likes-list.component';
import { MessageThreadComponent } from './message-thread/message-thread.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberlistComponent,
    MemberCardComponent,
    MemberPageComponent,
    UpdateUserComponent,
    AppPhotoComponent,
    FormserviceComponent,
    LikesListComponent,
    MessageThreadComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploadModule ,
    PaginationModule.forRoot()
  ],
  exports:[
    PaginationModule
  ],
  providers: [{provide:HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule {

}
