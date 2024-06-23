import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { error } from 'console';
import { UpdateCategoryRequest } from '../models/update-category-request.mode';
import { CategoryModel } from '../models/category.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit, OnDestroy {

  id: string | null = null;
  paramsSubscription? : Subscription;
  editCategorySubscription?: Subscription;
  category? : CategoryModel;

  constructor(private route: ActivatedRoute, 
    private categoryService: CategoryService,
    private router : Router) {

  }
   
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (parms) => {
        this.id = parms.get('id');
        this.getCategoryFromAPI();
      }
    });
  }

  getCategoryFromAPI() {
    if(this.id) {
      this.categoryService.getCategoriyById(this.id).subscribe({
        next: (response) => {
          this.category = response;
        }, 
        error: (error) => {
  
        }
      });   
    }  
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }

  onDeleteBtnClick(): void {
    if(this.id && this.category) {
      this.categoryService.deleteCategory(this.id).subscribe({
        next: (response) => {
          this.router.navigate(['/admin/categories']);
        },
        error: (error) => {

        }
      });
    }
  }

  onFormSubmit(): void {
  
    if(this.id && this.category) {

      const updateCategoryRequest: UpdateCategoryRequest = {
        name: this.category.name ?? '',
        urlHandle: this.category.urlHandle ?? '',
      }

      this.editCategorySubscription = this.categoryService.updateCategory(this.id,updateCategoryRequest).subscribe({
        next: (response) => {
          this.router.navigate(['/admin/categories']);
        },
        error: (error) => {
    
        }
       });      
    }
 
  }

}
