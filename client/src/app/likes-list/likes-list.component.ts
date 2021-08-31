import { Component, OnInit } from '@angular/core';
import { MemberService } from '_services/member.service';
import { Member } from '../_models/member';


@Component({
  selector: 'app-likes-list',
  templateUrl: './likes-list.component.html',
  styleUrls: ['./likes-list.component.css']
})
export class LikesListComponent implements OnInit {
  memberLikes:Partial<Member[]>;
  predicate:string = "source";
  constructor(private memberservice:MemberService) { }

  ngOnInit(): void {
    this.memberservice.GetLikes(this.predicate).subscribe(data => this.memberLikes = data);
  }
  loadLikes(predicate){
    this.predicate = predicate;
    this.memberservice.GetLikes(this.predicate).subscribe(data => this.memberLikes = data);
  }




}
