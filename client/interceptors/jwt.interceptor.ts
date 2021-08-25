import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '_services/account.service';
import { User } from '_types/User';
import { take } from 'rxjs/operators';
/**
 * INTERCETTA LE RICHIESTE HTTP LE CLONA E CI ATTACCA TOKEN AUTHORIZATION
 */

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountservice: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let currentUser:User;


      this.accountservice.currentUser$.pipe(take(1)).subscribe(
       
        user =>{currentUser = user;
          console.log(user);}
    );
    if(currentUser){
      console.log(currentUser);
     request = request.clone({
      setHeaders:{
        Authorization:"Bearer "+currentUser.token
      }
    })

  }



    return next.handle(request);
  }
}
