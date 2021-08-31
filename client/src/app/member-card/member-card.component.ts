import { Component, Input, OnInit } from '@angular/core';
import { MemberService } from '_services/member.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member;
  constructor(private memberservice: MemberService) { }

  ngOnInit(): void {
  }

  LikeUser(username){
      this.memberservice.LikeUser(username).subscribe(
        ()=> console.log("you liked that user")
      ,error =>{if(error.status != 200) console.log(error)
      else console.log("you liked that user");}
      );
  }
}
