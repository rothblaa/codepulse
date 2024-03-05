import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  // returns obs of type categoriesList
  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategoryById(id: string): Observable<Category> {
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`)
  }

  //takes model and returns observable
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`, model, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

  updateCategory(id: string, UpdateCategoryRequest: UpdateCategoryRequest):
  Observable<Category> {
    return this.http.put<Category>(`${environment.apiBaseUrl}/api/categories/${id}`,
    UpdateCategoryRequest,{
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

  deleteCategory(id: string): Observable<Category> {
    return this.http.delete<Category>(`${environment.apiBaseUrl}/api/categories/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }
}
