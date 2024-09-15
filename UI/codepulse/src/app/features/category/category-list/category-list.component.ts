import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';
import { DeleteCategoryComponent } from '../delete-category/delete-category.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],  
/*   template: `
<div *ngIf="categories$ | async as categories">
  <app-delete-category 
    *ngFor="let category of categories"
    [categories]="category">
  </app-delete-category>
</div>
` */
})
export class CategoryListComponent implements OnInit {

  @ViewChild('deleteCategoryModal') deleteCategoryModal!: DeleteCategoryComponent;
  
  //categories?:Category[]; ->form 1
  categories$?:Observable<Category[]>;

  constructor(private categoryService:CategoryService){

  }
  ngOnInit(): void {
    //Form 1
/*     this.categoryService.getAllCtegories()
    .subscribe({
      next:(response)=>{
        this.categories=response;
      }
    }); */

    //Form 2 :Using Async Pipe: Suscribe and ansusbcribe obervables automatically
    this.categories$=this.categoryService.getAllCtegories();



  }
}
