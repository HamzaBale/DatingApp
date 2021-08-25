import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';
import { User } from '_types/User';
import { Member } from '../_models/member';

export interface UpdateMember{
  introduction:string,
  username:string
}

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  member:Member;
  user: User;

  constructor(private memberservice:MemberService, private accountservice: AccountService) { }
  
  ngOnInit(): void {
 
    this.accountservice.currentUser$.pipe(take(1)).subscribe(x =>{ this.user =  x;});
   this.memberservice.GetMember(this.user.userName).subscribe(x =>  this.member = x);
  }

  onSubmit(){
      
      this.memberservice.UpdateData(this.member).subscribe(data=> console.log("buon fine"));
      this.user.userName = this.member.username;
  }
}
