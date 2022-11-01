import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { formatDate } from '../shares/Utilities';
import { homeDTO, movieCreateDTO, movieDTO, MoviePostGetDTO, MoviePutGetDTO } from './movie.model';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }
  private apiUrl = environment.apiUrl + '/movies';

  public filter(values: any): Observable<any>{
    const params = new HttpParams({fromObject: values});
    return this.http.get<movieDTO[]>(`${this.apiUrl}/filter`, {params, observe: 'response'});
  }

  public getHomePageMovies(page : number, recordsPerpage: number):Observable<any>{
    let params = new HttpParams();
    params = params.append('page', page.toString());
    params = params.append('recordsPerpage', recordsPerpage.toString());

    return this.http.get<movieDTO[]>(this.apiUrl, {observe: 'response', params}); 
  }

  public getById(id: number): Observable<movieDTO>{
    return this.http.get<movieDTO>(`${this.apiUrl}/${id}`);
  }

  public GetCinemaAndCategoryPost(): Observable<MoviePostGetDTO>{
    return this.http.get<MoviePostGetDTO>(`${this.apiUrl}/getpost`);
  }

  public create(movieCreate: movieCreateDTO):Observable<number>{
    const formData = this.FormMovieData(movieCreate);
    return this.http.post<number>(this.apiUrl, formData);
  } 

  public GetCinemaAndCategoryPut(id: number): Observable<MoviePutGetDTO>{
    return this.http.get<MoviePutGetDTO>(`${this.apiUrl}/getput/${id}`);
  }

  public edit(id: number, movieEdit: movieCreateDTO){
    const formData = this.FormMovieData(movieEdit);
    return this.http.put<number>(`${this.apiUrl}/${id}`, formData);
  }
  
  public delete(id: number){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  private FormMovieData(movie: movieCreateDTO): FormData{
    const formData = new FormData();

    formData.append('title', movie.title);
    formData.append('summary', movie.summary);
    formData.append('trailer', movie.trailer);
    formData.append('inCinemas', String(movie.inCinemas));
    if (movie.releaseDate){
      formData.append('releaseDate', formatDate(movie.releaseDate));
    }

    if (movie.poster){
      formData.append('poster', movie.poster);
    }

    formData.append('categoriesId', JSON.stringify(movie.categoriesId));
    formData.append('cinemasId', JSON.stringify(movie.cinemasId));
    formData.append('actors', JSON.stringify(movie.actors));
    
    return formData;
  }
}
