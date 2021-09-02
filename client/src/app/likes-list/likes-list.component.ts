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
  predicate:any = {
    text:"source",
    PageNumber:1,
    PageSize:2
  };
  pagination;
  pageSize=2;
  pageNumber;
  filters : {
    page?:number;
    pageSize?:number,
    Gender?:string,
    FromAge?:number,
    ToAge?:number
  } = {};
  constructor(private memberservice:MemberService) { }

  ngOnInit(): void {

    this.memberservice.GetLikes(this.predicate).subscribe(data => {
      console.log(data);
      this.memberLikes = data.result
      this.pagination = data.pagination;
      });
  }
  loadLikes(predicate){
    this.predicate.text = predicate;
    this.memberservice.GetLikes(this.predicate).subscribe(data =>{
      
      this.memberLikes = data.result
    this.pagination = data.pagination;
    });
  }
  pageChanged(event){
    this.pageNumber = event.page;
    this.predicate.PageNumber = this.pageNumber;
    this.predicate.PageSize = this.pageSize;


    this.memberservice.GetLikes(this.predicate).subscribe(data=>{
      this.memberLikes = data.result
      this.pagination = data.pagination;});
  }



}
