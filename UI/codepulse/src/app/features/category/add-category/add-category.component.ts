import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {

 model:AddCategoryRequest;
 private addCategorySubscription?:Subscription;

 constructor(private categoryService:CategoryService,private router:Router){ // inject service in constructor 
  this.model={name:'',urlHandle:''}
 }


  onFormSubmit(){
    console.log(this.model);

    this.addCategorySubscription=this.categoryService.addCategory(this.model)
    .subscribe({
      next:(response)=>{
        // do something when success
        console.log('This was succesful!!');
        this.router.navigateByUrl('/admin/categories');

      },
      error:(error)=>{
       //do something when error
       console.log('There is an error!!');
      }

    });
  }

  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
  }


}
