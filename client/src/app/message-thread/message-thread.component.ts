import { Component, OnInit } from '@angular/core';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';

@Component({
  selector: 'app-message-thread',
  templateUrl: './message-thread.component.html',
  styleUrls: ['./message-thread.component.css']
})
export class MessageThreadComponent implements OnInit {
  InboxMessages = [];
  pagination;
  Page = 1;
  MessageType:("Inbox"|"Outbox"|"Unread")="Inbox";
  username;
  sendMex={
    Content:"",
    RecipientUsername:""
  };
  constructor(private accountservice:AccountService, private memberservice:MemberService) { }
  
  ngOnInit(): void {
    this.accountservice.currentUser$.subscribe(x => this.username = x.userName )
    this.memberservice.GetMessages({predicate:this.MessageType,page:this.Page}).subscribe(x=> {
      console.log(x);
      this.InboxMessages = x.result;
      this.pagination = x.pagination});
  }
  pageChanged(event){
    this.Page = event.page;
    this.memberservice.GetMessages({predicate:this.MessageType,page:this.Page}).subscribe(x=> {
      console.log(x);
      this.InboxMessages = x.result;
      this.pagination = x.pagination})
  }

  MessageViewChange(event){

    this.MessageType = event.target.id;
    this.memberservice.GetMessages({predicate:this.MessageType,page:this.Page}).subscribe(x=> {

      this.InboxMessages = x.result;
      this.pagination = x.pagination})
  }
  SendMessage(){
    this.memberservice.SendMessage(this.sendMex.RecipientUsername,this.sendMex.Content).subscribe(data =>
    this.InboxMessages.unshift(data))
      //this.InboxMessages = data.result)
  }

  DeleteMessage(inMessage){
       let index =this.InboxMessages.indexOf(inMessage);
    if(index >= 0) this.InboxMessages.splice(index, 1);

    this.memberservice.DeleteMessage(inMessage.id).subscribe(data => console.log(data));
  
  }


}
