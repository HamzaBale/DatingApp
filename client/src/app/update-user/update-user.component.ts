import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';
import { User } from '_types/User';
import { Member } from '../_models/member';

export interface UpdateMember{
  introduction:string,
  username:string,
  City:string,
  Country:string,
  Interests:string,
  KnownAs:string,
  LookingFor:string
}

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  member:Member;
  user: User;
  inputMember:UpdateMember = {
    introduction:"",
    username:"",
    City:"",
    Country:"",
    Interests:"",
    KnownAs:"",
    LookingFor:""
  };
  constructor(private memberservice:MemberService, public accountservice: AccountService) { }
  
  ngOnInit(): void {
 
    this.accountservice.currentUser$.pipe(take(1)).subscribe(x =>{ this.user =  x;});
   this.memberservice.GetMember(this.user.userName).subscribe(x =>  this.member = x);
  }

  onSubmit(){
    this.member.username = this.inputMember.username;
    this.member.introduction = this.inputMember.introduction;
    this.member.knownAs = this.inputMember.KnownAs;
      this.memberservice.UpdateData(this.member).subscribe(data=> console.log("buon fine"));
      this.user.userName = this.member.username;
  }
}
