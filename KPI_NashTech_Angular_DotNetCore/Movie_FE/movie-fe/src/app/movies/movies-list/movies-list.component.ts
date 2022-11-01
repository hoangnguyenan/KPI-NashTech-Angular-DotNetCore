import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MovieService } from '../movie.service';

@Component({
  selector: 'app-movies-list',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.css']
})
export class MoviesListComponent implements OnInit {
  
  @Input()
  movies: any;

  @Output()
  onDelete = new EventEmitter<void>();

  constructor(private movieService: MovieService){}

  ngOnInit(): void { 
  }
  remove(id: number){
    this.movieService.delete(id).subscribe(() =>{
      this.onDelete.emit();
    });
  }
}
