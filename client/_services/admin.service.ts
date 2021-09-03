import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { RoleResponse } from 'src/app/_interfaces/RoleResponse';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl:string =  environment.ApiUrl;
  user;
  constructor(private http:HttpClient) { }

  public getAllUserWithRoles(){
    
    return this.http.get(this.baseUrl+"roles").pipe(
      map(x => this.user = x));

  }

  public getUserRolesByUsername(username){
      return this.user.find(x=> x.username === username);
  }


  public setNewRole(role,edit,username){
    let roles = [];
    roles.push(role);
    return this.http.post<RoleResponse>(this.baseUrl+"roles/edit/"+username,{role:roles,Edit:edit});
  }



}
