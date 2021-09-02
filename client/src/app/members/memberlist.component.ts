import { createOfflineCompileUrlResolver } from '@angular/compiler';
import { Component, OnInit, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';
import { PaginatedResult, pagination,  } from '../_interfaces/PaginationI';
import { UserParams } from '../_interfaces/Userparams';

@Component({
  selector: 'app-memberlist',
  templateUrl: './memberlist.component.html',
  styleUrls: ['./memberlist.component.css']
})
export class MemberlistComponent implements OnInit {
  users: Member[];
  pagination: pagination;
  pageNumber:number = 1;
  pageSize:number = 2;
  filters : {
    page?:number;
    pageSize?:number,
    Gender?:string,
    FromAge?:number,
    ToAge?:number
  } = {};
  error;
  gender;
  loaded:boolean = true;
  constructor(private memberservice:MemberService, private route:ActivatedRoute,private accountService:AccountService ) { }

  ngOnInit(): void {
    let user = JSON.parse(localStorage.getItem("user"));
    console.log(user);

      this.gender = user.Gender == "male" ? "female" : "male";
      this.filters.Gender = this.gender;
      this.filters.FromAge = 18;
      this.filters.ToAge = 140;
      this.GetMembers(this.pageNumber, this.pageSize,this.filters);

  }

  public ChangeFilter(event){
  

    this.GetMembers(this.pageNumber, this.pageSize,this.filters);
    }
    

  public GetMembers(page?:number,pageSize?:number, filters?:any){

         if(filters) this.filters = {
        page:page,
        pageSize:pageSize,
        Gender:filters?.Gender,
        FromAge:filters?.FromAge,
        ToAge:filters?.ToAge
      }
    else {
      this.filters = {
        page:page,
        pageSize:pageSize,
      }
    }


    this.memberservice.GetMembers(this.filters).subscribe(
      users => {
        
        this.users = users.result;
        this.pagination = users.pagination;
        this.loaded = true;
      }
    ,error => this.error = error);
  }

  public pageChanged(event){
    this.loaded = false;
    this.pageNumber = event.page;
  
    this.GetMembers(this.pageNumber,this.pageSize,this.filters);

  }

}
