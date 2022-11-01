import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { showAPIErrors } from 'src/app/shares/Utilities';
import { categoryCreateDTO } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {

  errors: string[] = [];
  constructor(private router: Router, private categoryService: CategoryService) { }
  ngOnInit(): void {

  }
  saveChanges(categoryCreateDTO: categoryCreateDTO) {
    this.categoryService.create(categoryCreateDTO).subscribe(() => {
      this.router.navigate(['/categories']);
    }, error => this.errors = showAPIErrors(error));
  
  }
  
}
