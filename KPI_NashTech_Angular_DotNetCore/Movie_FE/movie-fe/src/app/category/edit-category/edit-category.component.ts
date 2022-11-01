import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { categoryCreateDTO, categoryDTO } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

  constructor(private activeRoute: ActivatedRoute, 
    private categoryService: CategoryService,
    private router: Router) { }

  model: categoryDTO | any;

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params => {
      this.categoryService.getById(params.id).subscribe(category => {
        this.model = category;
      });
    });
  }
  saveChanges(categoryCreateDTO: categoryCreateDTO) {
    this.categoryService.edit(this.model.id, categoryCreateDTO).subscribe(() =>{
      this.router.navigate(["/categories"]);
    });
    
  }

}
