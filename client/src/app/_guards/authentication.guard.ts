import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private accountService: AccountService){}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(res => {
        if(res) return true;
        console.error("You can't go here");
        return false;
      })
    )
  }
  
}
