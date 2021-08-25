import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpdateMember } from 'src/app/update-user/update-user.component';
import { Member } from 'src/app/_models/member';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}



@Injectable({
  providedIn: 'root'
})
export class MemberService {

  BaseUrl = environment.ApiUrl;

  constructor(private http:HttpClient) { }

  public GetMembers(){
   return this.http.get<Member[]>(this.BaseUrl+"users");
  }

  public GetMember(username:string){
    return this.http.get<Member>(this.BaseUrl+"users/"+username);
  }

  public UpdateData(member: UpdateMember){

      return this.http.post<Member>(this.BaseUrl+"users/update",member);
  }

}
