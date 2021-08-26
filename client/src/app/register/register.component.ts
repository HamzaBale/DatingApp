import { Component, Input, OnInit, Output, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { EventEmitter }  from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '_services/account.service';
import { User } from '_types/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model = {name:"",password:"",confirmPassword:""};
  registerForm:FormGroup;
  constructor(private accountservice:AccountService) { }

  ngOnInit(): void {

    this.InizializeForm();

  }
  InizializeForm(){ 
    this.registerForm = new FormGroup(
      {
        username: new FormControl("",[Validators.required]),
        password: new FormControl("",[Validators.required]),
        confirmPassword:new FormControl("",[Validators.required,  this.matchValues("password")]),
        City:new FormControl("",[Validators.required]),
        knownAs:new FormControl("",[Validators.required]),
        country:new FormControl("",[Validators.required]),
        dateOfBirth:new FormControl("",[Validators.required]),
      }
    )
    this.registerForm.controls.password.valueChanges.subscribe( ()=> this.registerForm.controls.confirmPassword.updateValueAndValidity())


  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching: true}
    }
  }


  onSubmit(){
    console.log(this.registerForm.value);
    this.accountservice.register(this.registerForm.value).subscribe(resp => console.log(resp));
  }
}
