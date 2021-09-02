import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl:string =  environment.ApiUrl;
  constructor(private http:HttpClient) { }

  public getAllUserWithRoles(){
    
    return this.http.get(this.baseUrl+"roles");

  }



}
