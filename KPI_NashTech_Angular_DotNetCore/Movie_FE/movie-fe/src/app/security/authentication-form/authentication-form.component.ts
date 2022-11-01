import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';
import { checkNonAlphanumericCharacter, IsValidName} from 'src/app/validators/IsValidName';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { userCredentials } from '../security.model';

@Component({
  selector: 'app-authentication-form',
  templateUrl: './authentication-form.component.html',
  styleUrls: ['./authentication-form.component.css']
})
export class AuthenticationFormComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  form: FormGroup|any;

  @Input()
  action: string = 'Register'; 

  @Output()
  onSubmit = new EventEmitter<userCredentials>();

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username:['', {
        validators: [
          Validators.required,
          firstLetterUppercase(),
          Validators.maxLength(50)
        ]
      }],
      email: ['', {
        validators: [
          Validators.required,
          Validators.email,
          Validators.maxLength(50)
        ]
      }],
      password: ['', {
        validators: [
          Validators.required,
          firstLetterUppercase(),
          checkNonAlphanumericCharacter(),
          Validators.minLength(8),
          Validators.maxLength(50)
        ]
      }]
    });
  }

  getNameErrorMessage(){
    var name = this.form.get('username');
    if (name.hasError('required')){
      return "The name field is required";
    }
    if (name.hasError('firstLetterUppercase')){
      return name.getError('firstLetterUppercase').message;
    }
    if (name.hasError('maxlength')) {
      return 'The name must not exceed 50 characters.';
    }
    return '';
  }  

  getEmailErrorMessage(){
    var email = this.form.get('email');
    if (email.hasError('required')){
      return "The email field is required";
    }
    if (email.hasError('email')){
      return "The email is invalid";
    }
    if (email.hasError('maxlength')) {
      return 'Email must not exceed 50 characters.';
    }

    return '';
  }  

  getPasswordErrorMessage(){
    var password = this.form.get('password');
    if (password.hasError('required')){
      return "The password field is required";
    }
    if (password.hasError('firstLetterUppercase')){
      return password.getError('firstLetterUppercase').message;
    }
    if (password.hasError('checkNonAlphanumericCharacter')){
      return password.getError('checkNonAlphanumericCharacter').message;
    }
    if (password.hasError('minlength')) {
      return 'Password must be less than 8 character.';
    }
    if (password.hasError('maxlength')) {
      return 'Password must not exceed 50 characters.';
    }
    return '';
  }  
 
}

