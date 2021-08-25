import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MemberService } from '_services/member.service';

@Component({
  selector: 'app-memberlist',
  templateUrl: './memberlist.component.html',
  styleUrls: ['./memberlist.component.css']
})
export class MemberlistComponent implements OnInit {
  users: Member[];

  constructor(private member:MemberService ) { }

  ngOnInit(): void {
    this.GetMembers();
  }

  public GetMembers(){
    
    this.member.GetMembers().subscribe(
      users => {this.users = users;
      console.log(users);}
    )

  }

}
