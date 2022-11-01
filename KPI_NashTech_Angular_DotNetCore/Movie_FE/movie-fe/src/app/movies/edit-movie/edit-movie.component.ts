import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { actorMovieDTO } from 'src/app/actors/actor.model';
import { SelectModel } from 'src/app/shares/movie-category-select/movie-category-select.model';
import { movieCreateDTO, movieDTO } from '../movie.model';
import { MovieService } from '../movie.service';

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css']
})
export class EditMovieComponent implements OnInit {

  constructor(private activeRoute: ActivatedRoute,
    private movieService: MovieService,
    private router: Router) { }

  model: movieDTO|any;
  selectedCategories : SelectModel[]|any;
  nonSelectedCategories: SelectModel[]|any;
  selectedMovieTheaters: SelectModel[]|any;
  nonSelectedMovieTheaters: SelectModel[]|any;
  selectedActors: actorMovieDTO[]|any;

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params =>{
      this.movieService.GetCinemaAndCategoryPut(params.id).subscribe(data =>{
        this.model = data.movie;

        this.selectedCategories = data.selectedCategories.map(category => {
          return <SelectModel>{key: category.id, value: category.name}
        });

        this.nonSelectedCategories = data.nonSelectedCategories.map(category => {
          return <SelectModel>{key: category.id, value: category.name}
        });
  
        this.selectedMovieTheaters = data.selectedMovieTheaters.map(cinema => {
          return <SelectModel>{key: cinema.id, value: cinema.name}
        });

        this.nonSelectedMovieTheaters = data.nonSelectedMovieTheaters.map(cinema => {
          return <SelectModel>{key: cinema.id, value: cinema.name}
        });

        this.selectedActors = data.actors;
      });
    });
  }
  saveChanges(movieEdit: movieCreateDTO){
    this.movieService.edit(this.model.id, movieEdit).subscribe(() =>{
      this.router.navigate(['/movies/' + this.model.id]);
      console.log(movieEdit);
      
    });
    
  }
}
