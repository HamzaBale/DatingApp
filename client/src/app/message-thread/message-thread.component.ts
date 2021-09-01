import { Component, OnInit } from '@angular/core';
import { AccountService } from '_services/account.service';
import { MemberService } from '_services/member.service';

@Component({
  selector: 'app-message-thread',
  templateUrl: './message-thread.component.html',
  styleUrls: ['./message-thread.component.css']
})
export class MessageThreadComponent implements OnInit {
  InboxMessages;
  pagination;
  Page = 1;
  MessageType:("Inbox"|"Outbox"|"Unread")="Inbox";
  username;
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
    console.log(event);
    this.MessageType = event.target.id;
    this.memberservice.GetMessages({predicate:this.MessageType,page:this.Page}).subscribe(x=> {
      console.log(x);
      this.InboxMessages = x.result;
      this.pagination = x.pagination})
  }



}
