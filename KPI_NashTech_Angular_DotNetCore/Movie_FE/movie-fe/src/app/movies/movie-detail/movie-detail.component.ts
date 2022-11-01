import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute} from '@angular/router';
import { coordinatesMapWithMessage } from 'src/app/shares/map/coordinate';
import { RatingService } from 'src/app/shares/rating.service';
import Swal from 'sweetalert2';
import { movieDTO } from '../movie.model';
import { MovieService } from '../movie.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {

  constructor(
    private movieService: MovieService,
    private activatedRoute: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private ratingService: RatingService
    ) { }

    movie: movieDTO|any;
    releaseDate: Date|any;
    trailerUrl: SafeResourceUrl|any;
    coordinates: coordinatesMapWithMessage[] = [];

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.movieService.getById(params.id).subscribe((movie) => {
        this.movie = movie;
        this.releaseDate = new Date(movie.releaseDate);
        this.trailerUrl = this.gennerateEmbeddedVideoFromYoutube(movie.trailer);
        this.coordinates = movie.movieTheaters.map(movieTheater => {
          return {
            latitude: movieTheater.latitude,
            longitude: movieTheater.longitude, 
            name: movieTheater.name,
            address: movieTheater.address
          }
        })
      });
    });
    this.reloadData();
  }
  //e.g: https://www.youtube.com/watch?v=6ZfuNTqbHE8
  gennerateEmbeddedVideoFromYoutube(url: any): SafeResourceUrl{
    if(!url){
      return '';
    }
    let videoId = url.split('v=')[1];//get Id of youtube
    const ampersandPosition = videoId.indexOf('&');
    if (ampersandPosition !== -1){
      videoId = videoId.substring(0, ampersandPosition);
    }
    //Security in Angular
     return this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${videoId}`);
  }

  onRating(rate: number){
    this.ratingService.rate(this.movie.id, rate).subscribe(() => {
      Swal.fire("Success", "Your vote has been received", "success");
      this.reloadData();
    });
  }
  reloadData(){
    this.activatedRoute.params.subscribe((params) => {
      this.movieService.getById(params.id).subscribe((movie) => {
        this.movie = movie;
      });
    });
  }

}
