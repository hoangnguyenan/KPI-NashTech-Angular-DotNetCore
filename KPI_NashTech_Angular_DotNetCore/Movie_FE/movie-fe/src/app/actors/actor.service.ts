import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { formatDate } from '../shares/Utilities';
import { actorCreateDTO, actorDTO, actorMovieDTO } from './actor.model';

@Injectable({
  providedIn: 'root'
})
export class ActorService {

  constructor(private http: HttpClient) { }
  actor: actorMovieDTO[] | any;
  private apiUrl = environment.apiUrl + '/actors'

  getAll(page: number, recordsPerpage: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('page', page.toString());
    params = params.append('recordsPerpage', recordsPerpage.toString());
    return this.http.get<actorDTO[]>(this.apiUrl, { observe: 'response', params });
  }

  getById(id: number): Observable<actorDTO> {
    return this.http.get<actorDTO>(`${this.apiUrl}/${id}`);
  }

  searchByName(name: string): Observable<actorMovieDTO[]> {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this.http.post<actorMovieDTO[]>(`${this.apiUrl}/searchByName`,
      JSON.stringify(name), { headers });

  }

  create(actor: actorCreateDTO) {
    const fdata = this.formData(actor);
    return this.http.post(this.apiUrl, fdata);
  }

  edit(id: number, actor: actorCreateDTO) {
    const formData = this.formData(actor);
    return this.http.put(`${this.apiUrl}/${id}`, formData);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  private formData(actor: actorCreateDTO): FormData {
    const fdata = new FormData();
    fdata.append('name', actor.name);
    if (actor.story) {
      fdata.append('story', actor.story);
    }
    if (actor.dob) {
      fdata.append('dob', formatDate(actor.dob));
    }
    if (actor.picture) {
      fdata.append('picture', actor.picture);
    }
    return fdata;
  }

}
