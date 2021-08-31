import { Component, EventEmitter, Input, OnInit, Output, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { FileLikeObject, FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';
import { Member } from '../_models/member';
import { Photo } from '../_models/photo';

@Component({
  selector: 'app-app-photo',
  templateUrl: './app-photo.component.html',
  styleUrls: ['./app-photo.component.css']
})
export class AppPhotoComponent implements OnInit {
  @Output() picChanged = new EventEmitter<boolean>();
  @Output() PhotoUploaded = new EventEmitter<FileLikeObject>();
  @Input() member:Member;
  baseUrl = environment.ApiUrl;
  Uploader: FileUploader;
  hasBaseDropzoneOver: any;
  constructor(private memberservice:MemberService, private accountservice:AccountService) { }

  ngOnInit(): void {
    let token;
     this.accountservice.currentUser$.subscribe(x => token = x?.token);

        this.Uploader = new FileUploader({
          url:this.baseUrl+"users/add-photo",
          authToken:"Bearer " + token,
          isHTML5:true,
          allowedFileType:["image"],
          removeAfterUpload:true,
          autoUpload:false,
          maxFileSize: 10*1024*1024
        });
        this.Uploader.onAfterAddingFile = (file) =>{
          file.withCredentials = false;
        }
        this.Uploader.onSuccessItem = (item,response,status,headers)=>{
            if(response){
              const photo = JSON.parse(response);
              this.member.photos.push(photo);

              if(photo.isMain) this.setMainPhoto(photo);
              this.PhotoUploaded.emit(photo.url);
            }
        }
  }


  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  setMainPhoto(photo:Photo){

    this.memberservice.SetMainPhoto(photo.id).subscribe(e => console.log(e));
    this.picChanged.emit(true);
  }

  deletePhoto(photo){}

}
