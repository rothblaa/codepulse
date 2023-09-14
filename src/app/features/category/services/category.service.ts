import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  //takes model and returns observable
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>('https://localhost:7196/api/categories', model);
  }
}
