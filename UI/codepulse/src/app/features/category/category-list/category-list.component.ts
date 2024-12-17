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
  totalCount?: number;
  list:number[]=[];
  pageNumber=1;
  pageSize=5;
  resultsPerPage?:number=5;

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
    
    this.categoryService.getCategoriesCount()
    .subscribe({
      next:(value)=>{
        this.totalCount=value;

        //find the number of itererations

        this.list=new Array(Math.ceil(value/this.pageSize))

        //Form 2 :Using Async Pipe: Suscribe and ansusbcribe obervables automatically
        this.categories$=this.categoryService.getAllCategories(
          undefined,
          undefined,
          undefined,
          this.pageNumber,
          this.pageSize
        );
      }
    })

  }

  onSearch(query:string){
    //This filter it is posible implement only in FE. I.e client angular app using Angular material
    this.categories$=this.categoryService.getAllCategories(query);
  }

  sort(sortBy:string,sortDirection:string){
    this.categories$=this.categoryService.getAllCategories(undefined,sortBy,sortDirection);
  }

  getPage(pageNumber:number){
    this.pageNumber=pageNumber;

    this.categories$=this.categoryService.getAllCategories(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  getNextPage(){
    //validate if last page

    if(this.pageNumber+1>this.list.length){
      return;
    }
    this.pageNumber+=1;
    this.categories$=this.categoryService.getAllCategories(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );

  }

  UpdateResultsPerPage(){
    if(this.resultsPerPage){
      this.pageSize=this.resultsPerPage;
      this.categories$=this.categoryService.getAllCategories(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize
      );
    }
    
  }

  getPrevPage(){
    //validate if first page

    if(this.pageNumber-1<1){
      return;
    }
    this.pageNumber-=1;
    this.categories$=this.categoryService.getAllCategories(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );

  }
}
