import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { SecurityService } from './security/security.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  
  constructor(private securityService: SecurityService, private router: Router) {  }
  //go to app-routing to modify
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
      if (this.securityService.getRole() === 'admin' || this.securityService.getRole() === 'manager'){
        return true;
      }      
      Swal.fire("Warning", "You don't have permission to access. Please contact admin for authentication !!!", "warning");

      this.router.navigate(['/']);      
      return false;      
  }

  
}
