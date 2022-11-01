import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { categoryCreateDTO, categoryDTO } from './category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  private apiUrl = environment.apiUrl + '/categories'

  getAll(page : number, recordsPerpage: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('page', page.toString());
    params = params.append('recordsPerpage', recordsPerpage.toString());
    return this.http.get<categoryDTO[]>(this.apiUrl, {observe: 'response',params});
  }

  getAllWithoutPagination(): Observable<any>{
    return this.http.get<categoryDTO[]>(this.apiUrl);
  }
 
  getById(id: number): Observable<categoryDTO>{
    return this.http.get<categoryDTO>(`${this.apiUrl}/${id}`);
  }
  create(category: categoryCreateDTO){
    return this.http.post(this.apiUrl, category);
  }
  edit(id: number, category: categoryCreateDTO){
    return this.http.put(`${this.apiUrl}/${id}`, category);
    
  }
  delete(id:number){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
