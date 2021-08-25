import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberService } from '_services/member.service';
import { Member } from '../_models/member';

@Component({
  selector: 'app-member-page',
  templateUrl: './member-page.component.html',
  styleUrls: ['./member-page.component.css']
})
export class MemberPageComponent implements OnInit {
   member:Member;
  constructor(private memberservice: MemberService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.GetMember();
  }

  GetMember(){
    
    this.memberservice.GetMember(this.route.snapshot.paramMap.get("username")).subscribe(
      user=> {this.member = user
      }
    )
  }

}
