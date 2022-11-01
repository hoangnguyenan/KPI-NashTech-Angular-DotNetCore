import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { icon } from 'leaflet';
import { movieDTO } from 'src/app/movies/movie.model';
import { MovieService } from 'src/app/movies/movie.service';
import { coordinatesMapWithMessage } from 'src/app/shares/map/coordinate';
import { movieTheaterDTO } from '../movie-theater.model';
import { MovieTheaterService } from '../movie-theater.service';

@Component({
  selector: 'app-index-movie-theater',
  templateUrl: './index-movie-theater.component.html',
  styleUrls: ['./index-movie-theater.component.css']
})
export class IndexMovieTheaterComponent implements OnInit {

  constructor(private movieTheaterService: MovieTheaterService) { }

  movieTheaters: movieTheaterDTO[]|any;
  columnToDisplay = ['No.','name','address', 'actions'];
  totalAmountOfRecords : any;
  currentPage = 1;
  pageSize = 5;

  ngOnInit(): void {    
    this.loadData();
    
  }

  loadData(){
    this.movieTheaterService.getAll(this.currentPage, this.pageSize).subscribe((response: HttpResponse<movieTheaterDTO[]>) => {
      this.movieTheaters = response.body;
      this.totalAmountOfRecords = response.headers.get("totalAmountOfRecords");
    });
  }
  updatePagination(event: PageEvent){
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }  
  delete(id: number){
    this.movieTheaterService.delete(id).subscribe(() =>{
      this.loadData();
    });
  }


}
