import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { categoryDTO } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-index-category',
  templateUrl: './index-category.component.html',
  styleUrls: ['./index-category.component.css']
})
export class IndexCategoryComponent implements OnInit {

  category: categoryDTO[] | any;
  columnToDisplay = ['No.','name', 'actions'];
  totalAmountOfRecords : any;
  currentPage = 1;
  pageSize = 5;

  constructor(private categorySerice: CategoryService) { }

  ngOnInit(): void {
    this.loadCategory();
  }

  loadCategory(){
    this.categorySerice.getAll(this.currentPage, this.pageSize).subscribe((response: HttpResponse<categoryDTO[]>) => {
      this.category = response.body;
      this.totalAmountOfRecords = response.headers.get("totalAmountOfRecords");
    });
  }
  updatePagination(event: PageEvent){
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadCategory();
  } 
  
  delete(id: number){
    this.categorySerice.delete(id).subscribe(() =>{
      this.loadCategory();
    },error => {
      console.log(error);
    });
  }

}
