import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { movieTheaterCreateDTO, movieTheaterDTO } from './movie-theater.model';

@Injectable({
  providedIn: 'root'
})
export class MovieTheaterService {

  constructor(private http: HttpClient) { }

  private apiUrl = environment.apiUrl + '/movietheaters';

  public getAll(page : number, recordsPerpage: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('page', page.toString());
    params = params.append('recordsPerpage', recordsPerpage.toString());
    return this.http.get<movieTheaterDTO[]>(this.apiUrl, {observe: 'response',params});
  }
  
  public getById(id: number): Observable<movieTheaterDTO>{
    return this.http.get<movieTheaterDTO>(`${this.apiUrl}/${id}`);
  }

  public create(movieTheaterDTO: movieTheaterCreateDTO){
    return this.http.post(this.apiUrl, movieTheaterDTO);
  }

  public edit(id: number, movieTheaterDTO: movieTheaterCreateDTO){
    return this.http.put(`${this.apiUrl}/${id}`,movieTheaterDTO);
  }

  public delete(id: number){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
