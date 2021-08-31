import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppPhotoComponent } from './app-photo/app-photo.component';
import { HomeComponent } from './home/home.component';
import { LikesListComponent } from './likes-list/likes-list.component';
import { MemberPageComponent } from './member-page/member-page.component';
import { MemberlistComponent } from './members/memberlist.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { AuthenticationGuard } from './_guards/authentication.guard';

const routes: Routes = [
    {path:"", component:HomeComponent},
    {path:"matches", component:MemberlistComponent, canActivate:[AuthenticationGuard]},
    {path:"member/:username", component:MemberPageComponent,canActivate:[AuthenticationGuard]},
    {path:"memberedit", component:UpdateUserComponent,canActivate:[AuthenticationGuard]},
    {path:"update-photo/:username", component:AppPhotoComponent,canActivate:[AuthenticationGuard]},
    {path:"likes", component:LikesListComponent, canActivate:[AuthenticationGuard]},
    {path:"**", component:HomeComponent, pathMatch:"full"}
   
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
