import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter }  from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() CancelRegistration: boolean;
  constructor() { }

  ngOnInit(): void {
  }

}
