import { Component, Input, OnInit } from '@angular/core';
import { SelectModel } from './movie-category-select.model';

@Component({
  selector: 'app-movie-category-select',
  templateUrl: './movie-category-select.component.html',
  styleUrls: ['./movie-category-select.component.css']
})
export class MovieCategorySelectComponent implements OnInit {

  constructor() { }

  @Input()
  selectedItems: SelectModel[] = [];

  @Input()
  noneSelectedItems: SelectModel[] = [];

  ngOnInit(): void {
  }
  select(item: SelectModel, index: number){
    this.selectedItems.push(item);
    this.noneSelectedItems.splice(index,1);
  }

  deSelect(item: SelectModel, index: number){
    this.noneSelectedItems.push(item);
    this.selectedItems.splice(index, item.key);
  }

  selectAll(){
    this.selectedItems.push(...this.noneSelectedItems)
    this.noneSelectedItems = [];
  }
  deSelectAll(){
    this.noneSelectedItems.push(...this.selectedItems)
    this.selectedItems = [];
  }
}
