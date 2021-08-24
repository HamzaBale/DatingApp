import { Component, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public loggedIn:boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  CancelRegister(event:boolean){
    console.log(this.loggedIn)
    this.loggedIn = event;
  }


}
