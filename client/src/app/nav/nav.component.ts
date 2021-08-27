import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AccountService } from '_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  @Output() cancelRegistration = new EventEmitter<boolean>();
  model: any = {};

  loggedIn:boolean = false;

  constructor(public accountService:AccountService, private route: Router){

  }


  ngOnInit(): void {
    this.getCurrentUser();
  }

  login(){
    this.accountService.login(this.model).subscribe(res => {
      
      this.loggedIn = true;
      this.route.navigate(['matches']);
    }
    );    
    this.cancelRegistration.emit(true);
  }
  
  logout(){
    
   this.accountService.logout();
    this.loggedIn = false ;
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe(user => this.loggedIn = !!user);
  }
}
