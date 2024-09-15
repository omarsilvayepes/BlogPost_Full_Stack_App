import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy {
  
  id:string|null=null;
  paramsSuscription?:Subscription;
  editCategorySuscription?:Subscription;
  category?:Category

  constructor(private route:ActivatedRoute,// for get the id from url
    private categoryService:CategoryService,
    private router:Router){

    }

  
  ngOnInit(): void {
    this.paramsSuscription=this.route.paramMap.subscribe({
      next:(params)=>{
        this.id=params.get('id');
        if(this.id){
          //get the data from API for this category Id
          this.categoryService.getCategoryById(this.id)
          .subscribe({
            next:(response)=>{
              this.category=response;
            }
          })
        }

      }
    })
  }

  onFormSumit(){
    //Update on Database
    const updateCategoryRequest:UpdateCategoryRequest={
      name:this.category?.name??'',
      urlHandle:this.category?.urlHandle??''
    };

    //Call to the service
    if(this.id){
      this.editCategorySuscription=this.categoryService.updateCategory(this.id,updateCategoryRequest)
      .subscribe({
        next:(response)=>{
          this.router.navigateByUrl('admin/categories');
        }
      })
    }
  }

  ngOnDestroy(): void {
    this.paramsSuscription?.unsubscribe();
    this.editCategorySuscription?.unsubscribe();
  }
 
}
