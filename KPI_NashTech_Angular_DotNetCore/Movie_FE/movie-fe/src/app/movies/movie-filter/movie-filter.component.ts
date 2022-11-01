import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { categoryDTO } from 'src/app/category/category.model';
import { CategoryService } from 'src/app/category/category.service';
import { movieDTO } from '../movie.model';
import { MovieService } from '../movie.service';
import { Location } from '@angular/common'
import { ActivatedRoute } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { showAPIErrors } from 'src/app/shares/Utilities';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-movie-filter',
  templateUrl: './movie-filter.component.html',
  styleUrls: ['./movie-filter.component.css']
})
export class MovieFilterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private movieService: MovieService,
    private categoryService: CategoryService,
    private location: Location,
    private activatedRoute: ActivatedRoute
    ) { }

  form: any = FormGroup;
  categories : categoryDTO[]|any;
  movies: movieDTO[]|any;

  currentPage = 1;
  recordsPerPage = 6;
  initialFormValues: any;
  totalAmountOfRecords: any;


  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: '',
      categoryId: 0,
      upComingReleases: false,
      inCinemas: false
    });

    this.initialFormValues = this.form.value;
    //keep the record if the is reloaded again
    this.readParametersFromURL();

    this.categoryService.getAllWithoutPagination().subscribe(categories => {
      this.categories = categories;
  
      this.filterMovies(this.form.value);
  
      this.form.valueChanges
      .subscribe((values: any) => {
        this.filterMovies(values);
        this.writeParametersInURL();
      });
  
    })
  }
  filterMovies(values: any){
    values.page = this.currentPage;
    values.recordsPerPage = this.recordsPerPage;
    this.movieService.filter(values).subscribe((response: HttpResponse<movieDTO[]>)=>{
      // if(response.body?.length == 0){
      //   Swal.fire("Warning","Can not find the movie.","warning");
      // }else{
        this.movies = response.body;
        this.totalAmountOfRecords = response.headers.get("totalAmountOfRecords");
      // }
    })
  }

  private readParametersFromURL(){
    this.activatedRoute.queryParams.subscribe(params => {
      var obj: any = {};

      if (params.title){
        obj.title = params.title;
      }

      if (params.categoryId){
        obj.categoryId = Number(params.categoryId);
      }

      if (params.upComingReleases){
        obj.upComingReleases = params.upComingReleases
      }

      if (params.inCinemas){
        obj.inCinemas = params.inCinemas;
      }

      if (params.page){
        this.currentPage = params.page;
      }

      if (params.recordsPerPage){
        this.recordsPerPage = params.recordsPerPage;
      }

      this.form.patchValue(obj);
    });
  }

  private writeParametersInURL(){
    const queryStrings = [];

    const formValues = this.form.value;

    if (formValues.title){
      queryStrings.push(`title=${formValues.title}`);
    }

    if (formValues.categoryId != '0'){
      queryStrings.push(`categoryId=${formValues.categoryId}`);
    }

    if (formValues.upComingReleases){
      queryStrings.push(`upComingReleases=${formValues.upComingReleases}`);
    }

    if (formValues.inCinamas){
      queryStrings.push(`inCinamas=${formValues.inCinamas}`);
    }

    queryStrings.push(`page=${this.currentPage}`);
    queryStrings.push(`recordsPerPage=${this.recordsPerPage}`);

    this.location.replaceState('movies/filter', queryStrings.join('&'));
  }

  paginatorUpdate(event: PageEvent){
    this.currentPage = event.pageIndex + 1;
    this.recordsPerPage = event.pageSize;
    this.writeParametersInURL();
    this.filterMovies(this.form.value);
  }

  clearForm(){
    this.form.patchValue(this.initialFormValues);
  }

  onDelete(){
    this.filterMovies(this.form.value);
  }
}
