import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';


import { of } from 'rxjs';
import { map } from 'rxjs/operators';

import { PaginatedResult } from 'src/app/_interfaces/PaginationI';
import { UserParams } from 'src/app/_interfaces/Userparams';
import { Member } from 'src/app/_models/member';
import { environment } from 'src/environments/environment';
import { Message, MessageParams } from '_types/Message';



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
  MessagesResult:PaginatedResult<Message[]> = new PaginatedResult<Message[]>();
  //PaginatedResultLikes
  counter:number[] =[];
  memberCache = new Map();

  messageCache = new Map();
  messages:Message[];


  constructor(private http:HttpClient) { }

  public GetMembers(userParams:UserParams){
   
    let params = new HttpParams();
    var response = this.memberCache.get(Object.values(userParams).join("-"))

    if(response) {
      
      return of(response);
    }

    if(userParams.page) params = params.append("pageNumber",userParams.page.toString());
    
    if(userParams.pageSize) params= params.append("pageSize",userParams.pageSize.toString());

    if(userParams.Gender) params= params.append("Gender",userParams.Gender);

    if(userParams.FromAge) params= params.append("FromAge",userParams.FromAge);

    if(userParams.ToAge) params= params.append("ToAge",userParams.ToAge);

    
   return this.http.get<Member[]>(this.BaseUrl+"users",{observe:'response',params}).pipe(
     map(response => {
          //console.log(response.body);

          this.PaginatedResult.result = response.body;
  
         
          if(response.headers.get("pagination") !== null) {
            this.PaginatedResult.pagination = JSON.parse(response.headers.get("pagination"));
          }
          this.memberCache.set((Object.values(userParams).join("-")),{result:response.body,pagination:this.PaginatedResult.pagination});
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

  public LikeUser(username){
      return this.http.post(this.BaseUrl+"likes/"+username,{});
  }

  public DislikeUser(username){
    return this.http.post(this.BaseUrl+"likes/dislike/"+username,{});
  }

  public GetLikes(predicate){
    return this.http.get<Partial<Member[]>>(this.BaseUrl+"likes/?predicate="+predicate,{observe:'response'}).pipe(
      map(response=> {
       
        this.PaginatedResult.result = response.body;
        this.PaginatedResult.pagination = JSON.parse(response.headers.get("pagination"));
        return this.PaginatedResult;
      }
        )
    );
  }

  public GetMessages(messageParams:MessageParams){
    let params = new HttpParams();
    var test = this.memberCache.get(Object.values(messageParams).join("-"));
    if(test){
      console.log("cached");
      return of(test);
    } 
    
    
    if(messageParams.page) params = params.append("pageNumber",messageParams.page.toString());
    if(messageParams.pageSize) params = params.append("pageSize",messageParams.pageSize.toString());
  


    return this.http.get<Message[]>(this.BaseUrl+"messages/?container="+messageParams.predicate,{observe:'response',params}).pipe(
      map(response =>{
        this.MessagesResult.result = response.body;
        this.MessagesResult.pagination = JSON.parse(response.headers.get("pagination"));
        this.memberCache.set(Object.values(messageParams).join("-"),{result:response.body,pagination:this.MessagesResult.pagination});
        return this.MessagesResult;
      })
    );
  
  }



  
}
