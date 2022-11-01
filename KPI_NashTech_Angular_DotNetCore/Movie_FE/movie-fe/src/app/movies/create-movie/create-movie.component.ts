 import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SelectModel } from 'src/app/shares/movie-category-select/movie-category-select.model';
import { movieCreateDTO } from '../movie.model';
import { MovieService } from '../movie.service';

@Component({
  selector: 'app-create-movie',
  templateUrl: './create-movie.component.html',
  styleUrls: ['./create-movie.component.css']
})
export class CreateMovieComponent implements OnInit {

  constructor(private router: Router, 
    private movieService: MovieService) { }

    nonSelectedCategories: SelectModel[]|any;
    nonSelectedMovieTheaters: SelectModel[]|any;

  ngOnInit(): void {
    this.movieService.GetCinemaAndCategoryPost().subscribe(response => {
      this.nonSelectedCategories = response.categories.map(category => {
        return <SelectModel>{key: category.id, value: category.name}
      });

      this.nonSelectedMovieTheaters = response.movieTheaters.map(movieTheater => {
        return <SelectModel>{key: movieTheater.id, value: movieTheater.name}
      });
    });
  }
  saveChanges(movieCreateDTO: movieCreateDTO){
    this.movieService.create(movieCreateDTO).subscribe(id => {
      this.router.navigate(['/movies/' + id]);
    });
  }
}
