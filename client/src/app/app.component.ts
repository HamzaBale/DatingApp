import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '_services/account.service';
import { User } from '_types/User';

/*Type AppUser = {
  name:String
}*/

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App';
   users: any;
  
  ngOnInit(): void {
   this.setCurrentUser();
  }

  constructor(private accountService:AccountService){

  }

  setCurrentUser(){
    const user:User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  private getUsers(){
   //this.accountService.login({username:"hamza",password:"password"});
  }
}
