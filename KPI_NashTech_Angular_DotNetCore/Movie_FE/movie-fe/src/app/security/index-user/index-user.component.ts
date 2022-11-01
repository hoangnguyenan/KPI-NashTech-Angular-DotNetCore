import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { showAPIErrors } from 'src/app/shares/Utilities';
import Swal from 'sweetalert2';
import { userDTO } from '../security.model';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-index-user',
  templateUrl: './index-user.component.html',
  styleUrls: ['./index-user.component.css']
})
export class IndexUserComponent implements OnInit {

  constructor(private securityService: SecurityService) { }

  errors: string[] = [];

  users: userDTO[]|any;
  page: number = 1;
  pageSize: number = 5;
  totalAmountOfRecords:any;
  columnsToDisplay = ["No.","username","email", "role","actions"];

  Admin:string|any;
  Manager:string|any;
  User:string|any;

  ngOnInit(): void {
     this.loadData();
  }

  loadData(){
    this.securityService.getUsers(this.page, this.pageSize).subscribe((httpResponse: HttpResponse<userDTO[]>) => {
      this.users = httpResponse.body;
      this.totalAmountOfRecords = httpResponse.headers.get("totalAmountOfRecords");
    });   
  }

  makeAdmin(userId: string){
    this.securityService.makeAdmin(userId).subscribe((check) => { 
      if(check === 0)
      {
        Swal.fire("Warning", "You are already Admin.", "warning");
      }
      if(check === 1)
      {
        Swal.fire("Success", "Successfully granted manager permission.", "success");
        this.loadData();
      }
      if(check === 2){
        Swal.fire("Warning", "User is already Manager.", "warning");
      }
      if(check === 3){
        Swal.fire("Warning", "User is already Admin.", "warning");
      }
    }, error => this.errors = showAPIErrors(error))
  }

  removeAdmin(userId: string){
    this.securityService.removeAdmin(userId).subscribe((check) => {
      if(check == true){
        Swal.fire("Success", "Abort manager permission successfully.", "success");
        this.loadData();
      }else
      {
        Swal.fire("Warning", "The same level can not be deleted.", "warning");
      }
    },error => this.errors = showAPIErrors(error))
  }

  updatePagination(event: PageEvent){
    this.page = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }  

}
