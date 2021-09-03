
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AdminService } from '_services/admin.service';
import { RoleResponse } from '../_interfaces/RoleResponse';


@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css']
})
export class EditRoleComponent implements OnInit {
  username:string;
  user;
  selectedRoleRemove:string = "";
  selectedRoleAdd:string = "";

  constructor(private route:ActivatedRoute, private adminservice:AdminService) { }

  ngOnInit(): void {
    this.username = this.route.snapshot.paramMap.get("username");
      this.user =this.adminservice.getUserRolesByUsername(this.username);
  }
  ThisRoleAvailable(value){
    return this.user.role.indexOf(value) < 0;
  } 
  SelectChangedAdd(val){
      console.log(val);
     this.selectedRoleAdd = val;
  }
  SelectChangedRemove(val){
    console.log(val);
    this.selectedRoleRemove = val;
}
  ChangeRole(edit){

    if(edit ==="add" && this.selectedRoleAdd!="" && this.selectedRoleAdd!="Choose Role")this.adminservice.setNewRole(this.selectedRoleAdd,edit,this.username)
    .subscribe(data => {if(data.succeeded) this.user.role.push(this.selectedRoleAdd)});
    if(this.selectedRoleRemove !== "" && this.selectedRoleRemove !== "Choose Role" && edit ==="remove")this.adminservice.setNewRole(this.selectedRoleRemove,edit,this.username)
    .subscribe(data =>  {if(data.succeeded) {
      let index = this.user.role.indexOf(this.selectedRoleRemove);
      this.user.role.splice(index, 1);
    }});
  }
} 
