import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '_types/User';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl:string = "https://localhost:5001/api/";

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  login(user:User){
    console.log(user);
    return this.http.post(this.baseUrl+"Account/login",user).pipe(
      map((response:User)=>{ 
        if(response!= null) {
          localStorage.setItem('user',JSON.stringify(response));
          this.currentUserSource.next(response);
      }
      }
      )
    );
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
