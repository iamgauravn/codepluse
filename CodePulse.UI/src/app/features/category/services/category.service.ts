import { Injectable, model } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { UpdateCategoryRequest } from '../models/update-category-request.mode';
import { CategoryModel } from '../models/category.model';
import { AddCategoryRequest } from '../models/add-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http : HttpClient) { }
  
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`, model, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  getCategories(): Observable<CategoryModel[]> {
    return this.http.get<CategoryModel[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategoriyById(id : string): Observable<CategoryModel> {
    return this.http.get<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

  updateCategory(id: string, category : UpdateCategoryRequest) : Observable<CategoryModel> {
    return this.http.put<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`,category);
  }

  deleteCategory(id:string) : Observable<CategoryModel> {
    return this.http.delete<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }
   
}
