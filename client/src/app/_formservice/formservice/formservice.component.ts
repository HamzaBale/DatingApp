import { Component, Input, OnInit, Self } from '@angular/core';
import {ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-formservice',
  templateUrl: './formservice.component.html',
  styleUrls: ['./formservice.component.css']
})
export class FormserviceComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() type:string;
  constructor(@Self() public ngControl: NgControl ) { 
    this.ngControl.valueAccessor = this;
  }
  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
   
  }
  registerOnTouched(fn: any): void {
   
  }


}
