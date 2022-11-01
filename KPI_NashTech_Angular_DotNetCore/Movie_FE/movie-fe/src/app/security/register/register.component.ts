import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { showAPIErrors } from 'src/app/shares/Utilities';
import { userCredentials } from '../security.model';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private securityService: SecurityService,
    private router: Router) { }

  errors: string[] = [];     
  
  @Input()
  registerView = false;

  ngOnInit(): void {
  }

  register(userCredentials: userCredentials ){
    this.errors = [];
    this.securityService.register(userCredentials).subscribe(authenticationResponse => {
      // console.log(authenticationResponse);
      
      this.securityService.saveToken(authenticationResponse);
      this.router.navigate(['/']);
    }, error => this.errors = showAPIErrors(error));
  }
  
}
