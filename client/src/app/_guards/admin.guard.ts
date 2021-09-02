import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AccountService } from '_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate  {
  roles:string[];
  constructor(private accountService:AccountService){
  }


  canActivate(): Observable<boolean>{
    
    this.accountService.currentUser$.subscribe(user =>{ this.roles = user.Roles
    console.log(user);
    });
    if(this.roles.indexOf("Moderator")>0 ||this.roles.indexOf("Admin")>0 ) return of(true) ;
    console.log("Who you again?");
    throw "Who's you";
  }
  
}
