import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { showAPIErrors } from 'src/app/shares/Utilities';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';
import { checkNonAlphanumericCharacter, IsValidName } from 'src/app/validators/IsValidName';
import { userCredentials } from '../security.model';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private securityService: SecurityService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  errors: string[] = [];
  action: string = 'Login'; 
  form: FormGroup|any;


  ngOnInit(): void {
    this.form = this.formBuilder.group({     
      username:['', {
        validators: [
          Validators.required,
          firstLetterUppercase(),
          Validators.maxLength(50)
        ]
      }],
      password: ['', {
        validators: [
          Validators.required,
          firstLetterUppercase(),
          checkNonAlphanumericCharacter(),
          Validators.minLength(8)
        ]
      }]
    });
  }

  login(userCredentials: userCredentials) {
    this.securityService.login(userCredentials).subscribe(authenticationResponse => {
        this.securityService.saveToken(authenticationResponse);
        this.router.navigate(['/']);
      }, error => this.errors = showAPIErrors(error));
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
