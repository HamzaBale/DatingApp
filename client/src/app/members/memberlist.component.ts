import { Component, OnInit, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MemberService } from '_services/member.service';
import { PaginatedResult, pagination,  } from '../_interfaces/PaginationI';

@Component({
  selector: 'app-memberlist',
  templateUrl: './memberlist.component.html',
  styleUrls: ['./memberlist.component.css']
})
export class MemberlistComponent implements OnInit {
  users: Member[];
  pagination: pagination;
  pageNumber:number = 1;
  pageSize:number = 5;
  filters:any = {};
  constructor(private memberservice:MemberService, private route:ActivatedRoute ) { }

  ngOnInit(): void {
    let user = JSON.parse(localStorage.getItem("user"));

    /*this.memberservice.GetMember(user.userName).subscribe(us => 
      this.Male = us.gender == "Male" ? true : false
      );*/

    this.GetMembers(this.pageNumber, this.pageSize);

  }

  public ChangeFilter(event){
  
    console.log(this.filters);
    this.GetMembers(this.pageNumber, this.pageSize,this.filters);
    }
    

  public GetMembers(page?:number,pageSize?:number, filters?:any){
    
    this.memberservice.GetMembers(page,pageSize,filters?.Gender,filters?.FromAge,filters?.ToAge).subscribe(
      users => {this.users = users.result;
        this.pagination = users.pagination;
      console.log( this.pagination);}
    )

  }

  public pageChanged(event){
    
    this.pageNumber = event.page;
  
    this.GetMembers(this.pageNumber,this.pageSize);

  }

}
