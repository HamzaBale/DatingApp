import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberlistComponent } from './members/memberlist/memberlist.component';
import { AuthenticationGuard } from './_guards/authentication.guard';

const routes: Routes = [
    {path:"", component:HomeComponent},
    {path:"matches", component:MemberlistComponent, canActivate:[AuthenticationGuard]},
    {path:"**", component:HomeComponent, pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
