import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { movieTheaterCreateDTO, movieTheaterDTO } from '../movie-theater.model';
import { MovieTheaterService } from '../movie-theater.service';

@Component({
  selector: 'app-edit-movie-theater',
  templateUrl: './edit-movie-theater.component.html',
  styleUrls: ['./edit-movie-theater.component.css']
})
export class EditMovieTheaterComponent implements OnInit {

  constructor(private activeRoute: ActivatedRoute,
    private movieTheaterService: MovieTheaterService,
    private router: Router) { }

  model: movieTheaterDTO|any;
  
  ngOnInit(): void {
    this.activeRoute.params.subscribe(params =>{
      this.movieTheaterService.getById(params.id).subscribe(movieTheater =>
        this.model = movieTheater);
    });
  }
  saveChanges(movieTheater: movieTheaterCreateDTO){
    this.movieTheaterService.edit(this.model.id, movieTheater).subscribe(() =>{
      this.router.navigate(["/movietheaters"]);
    });
  }

}
