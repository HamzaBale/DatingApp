import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { UpdateMember } from 'src/app/update-user/update-user.component';
import { PaginatedResult, pagination } from 'src/app/_interfaces/PaginationI';
import { Member } from 'src/app/_models/member';
import { environment } from 'src/environments/environment';
import { AccountService } from './account.service';


const httpOptions = { //jwtinterceptor
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
  PaginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http:HttpClient) { }

  public GetMembers(page?:number,pageSize?:number, Gender?:any,FromAge?:number,ToAge?:number){
    let params = new HttpParams();
  
  
    if(page) params = params.append("pageNumber",page.toString());
    
    if(pageSize) params= params.append("pageSize",pageSize.toString());

    if(Gender) params= params.append("Gender",Gender);

    if(FromAge) params= params.append("FromAge",FromAge);
    if(ToAge) params= params.append("ToAge",ToAge);

   return this.http.get<Member[]>(this.BaseUrl+"users",{observe:'response',params}).pipe(
     map(response => {

          this.PaginatedResult.result = response.body;
          if(response.headers.get("pagination") !== null) {this.PaginatedResult.pagination = JSON.parse(response.headers.get("pagination"));}
          return this.PaginatedResult;
        }
      )
   );
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
    this.members.find(x => x.photoUrl = x.photos.find(s => s.id === photoId).url);
    return this.http.put(this.BaseUrl+"users/main-photo/"+photoId,photoId);
      
  }
  public DeleteLocalMembers(){
    this.members = [];
  }

  
}
