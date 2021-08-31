import { createOfflineCompileUrlResolver } from '@angular/compiler';
import { Component, OnInit, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
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
  filters:UserParams = {
    Gender : "male",
    FromAge : 18,
    ToAge: 150,

  };
  constructor(private memberservice:MemberService, private route:ActivatedRoute ) { }

  ngOnInit(): void {
    let user = JSON.parse(localStorage.getItem("user"));

    /*this.memberservice.GetMember(user.userName).subscribe(us => 
      this.Male = us.gender == "Male" ? true : false
      );*/

    this.GetMembers(this.pageNumber, this.pageSize);

  }

  public ChangeFilter(event){
  

    this.GetMembers(this.pageNumber, this.pageSize,this.filters);
    }
    

  public GetMembers(page?:number,pageSize?:number, filters?:any){
    console.log(filters);
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
        Gender : "male",
        FromAge : 18,
        ToAge: 150,
      }
    }


    this.memberservice.GetMembers(this.filters).subscribe(
      users => {
        console.log(users);
        this.users = users.result;
        this.pagination = users.pagination;
      }
    )

  }

  public pageChanged(event){
    
    this.pageNumber = event.page;
  
    this.GetMembers(this.pageNumber,this.pageSize);

  }

}
