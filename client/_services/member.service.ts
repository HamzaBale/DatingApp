import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
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

  members:Member[] = [];

  constructor(private http:HttpClient) { }

  public GetMembers(){
    console.log(this.members);
    if(this.members.length > 0) return of(this.members);
   return this.http.get<Member[]>(this.BaseUrl+"users").pipe(
     map(users => 
                {this.members = users;
                  console.log(users);
                 return users;
                }));
  }

  public GetMember(username:string){
    const member = this.members.find(x => x.username === username);
    if(member !== undefined) return of(member);

    return this.http.get<Member>(this.BaseUrl+"users/"+username).pipe(
      map(user =>{ 
                    return user;
      })
    );
  }

  public UpdateData(member: Member){
      return this.http.put<Member>(this.BaseUrl+"users",member).pipe();
  }

  public SetMainPhoto(photoId){
    console.log(this.members.find(x => x.photoUrl = x.photos.find(s => s.id === photoId).url));
    return this.http.put(this.BaseUrl+"users/main-photo/"+photoId,photoId);
      
  }
  public DeleteLocalMembers(){
    this.members = [];
  }

  
}
