import { Component, OnInit, Output } from '@angular/core';
import { AccountService } from '_services/account.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public loggedIn:boolean = false;

  constructor(public accountservice: AccountService) { }

  ngOnInit(): void {

  }

  CancelRegister(event:boolean){

    this.loggedIn = event;
  }


}
