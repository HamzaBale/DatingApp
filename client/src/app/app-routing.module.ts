import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberPageComponent } from './member-page/member-page.component';
import { MemberlistComponent } from './members/memberlist.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { AuthenticationGuard } from './_guards/authentication.guard';

const routes: Routes = [
    {path:"", component:HomeComponent},
    {path:"matches", component:MemberlistComponent, canActivate:[AuthenticationGuard]},
    {path:"home/:username", component:MemberPageComponent,canActivate:[AuthenticationGuard]},
    {path:"memberedit", component:UpdateUserComponent,canActivate:[AuthenticationGuard]},
    {path:"**", component:HomeComponent, pathMatch:"full"}
   
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
