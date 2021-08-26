import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter }  from '@angular/core';
import { AccountService } from '_services/account.service';
import { User } from '_types/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model = {name:"",password:""};
  constructor(private accountservice:AccountService) { }

  ngOnInit(): void {

  }

  onSubmit(){
    this.accountservice.register(this.model).subscribe(resp => console.log(resp));
  }
}
