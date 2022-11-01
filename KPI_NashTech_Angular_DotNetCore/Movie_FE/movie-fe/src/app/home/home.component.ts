import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { homeDTO } from '../movies/movie.model';
import { MovieService } from '../movies/movie.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  inCinemas: any;
  upComingReleases: any;
  title = 'movie-fe';
  
  currentPage = 1;
  recordsPerPage = 6;
  totalAmountOfRecordsUpComing: any;
  totalAmountOfRecordsInCinema: any;

  constructor(private movieService: MovieService){}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(){
    this.movieService.getHomePageMovies(this.currentPage, this.recordsPerPage).subscribe((homeDTO: HttpResponse<any>) =>{
      this.inCinemas = homeDTO.body.inCinemas;
      this.upComingReleases = homeDTO.body.upComingReleases;
      this.totalAmountOfRecordsInCinema = homeDTO.headers.get("totalAmountOfRecordsInCinema");
      this.totalAmountOfRecordsUpComing = homeDTO.headers.get("totalAmountOfRecordsUpComing");
    });
  }

  paginatorUpdate(event: PageEvent){
    this.currentPage = event.pageIndex + 1;
    this.recordsPerPage = event.pageSize;
    this.loadData();
  }
  
  onDelete(){
    this.loadData();
  }

}
