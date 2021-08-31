import { Component, OnInit } from '@angular/core';
import { MemberService } from '_services/member.service';
import { UserParams } from '../_interfaces/Userparams';
import { Member } from '../_models/member';


@Component({
  selector: 'app-likes-list',
  templateUrl: './likes-list.component.html',
  styleUrls: ['./likes-list.component.css']
})
export class LikesListComponent implements OnInit {
  memberLikes:Partial<Member[]>;
  predicate:string = "source";
  pagination;
  pageSize=2;
  pageNumber;
  constructor(private memberservice:MemberService) { }

  ngOnInit(): void {
    this.memberservice.GetLikes(this.predicate).subscribe(data => {
      console.log(data);
      this.memberLikes = data.result
      this.pagination = data.pagination;
      });
  }
  loadLikes(predicate){
    this.predicate = predicate;
    this.memberservice.GetLikes(this.predicate).subscribe(data =>{
      
      this.memberLikes = data.result
    this.pagination = data.pagination;
    });
  }
  pageChanged(event){
    this.pageNumber = event.page;
    var userparams = new UserParams();
    userparams.page = this.pageNumber;
    userparams.pageSize = this.pageSize;


    this.memberservice.GetMembers(userparams).subscribe(data=>{
      this.memberLikes = data.result
      this.pagination = data.pagination;});
  }



}
