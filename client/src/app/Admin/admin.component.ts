import { Component, OnInit } from '@angular/core';
import { AccountService } from '_services/account.service';
import { AdminService } from '_services/admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  selected;
  users;
  constructor(private AdminService:AdminService) { }

  ngOnInit(): void {
      this.AdminService.getAllUserWithRoles().subscribe(x=>this.users = x);
    
  }
  ChangeRole(id,event){
 
        console.log(id,event);
  }


}
