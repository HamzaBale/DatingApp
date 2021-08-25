import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';
import { Member } from '../_models/member';

export interface UpdateMember{
  OldUsername:string,
  introduction:string,
  username:string
}

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  member:UpdateMember = {
    OldUsername:"",
    introduction:"",
    username:""
  };

  constructor(private memberservice:MemberService, private accountservice: AccountService) { }
  
  ngOnInit(): void {
    console.log("on init")
    this.accountservice.currentUser$.pipe(take(1)).subscribe(x =>{this.member.OldUsername =  x.userName;});

  }

  onSubmit(){
      
      this.memberservice.UpdateData(this.member).subscribe(data=> console.log(data));
    
  }
}
