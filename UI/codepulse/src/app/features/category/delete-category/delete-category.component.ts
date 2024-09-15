import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Location } from '@angular/common';

declare var bootstrap: any;

@Component({
  selector: 'app-delete-category',
  templateUrl: './delete-category.component.html',
  styleUrls: ['./delete-category.component.css']
})
export class DeleteCategoryComponent implements OnInit ,OnDestroy{
  @Input()
  categories!: Category[]; //those categories values are get from Parent component (category-list.ts),this component it is the child
  categoryById?: Category;
  private deleteCategorySubscription?:Subscription;
  
  private modalElement: any;

  constructor(private categoryService:CategoryService,
    private location:Location) {}


  ngOnInit(): void {
    this.modalElement = document.getElementById('deleteCategoryModal');
    console.log(this.categories);
  }

  openModal(id :string) {
    this.categoryById=this.categories?.find(c=> c.id== id);
    console.log(this.categoryById);
    const modal = new bootstrap.Modal(this.modalElement);
    modal.show();
  }

  closeModal() {
    const modal = bootstrap.Modal.getInstance(this.modalElement);
    if (modal) {
      modal.hide();
    }
  }

  onDelete() {
    // Implement your delete logic here

    if(this.categoryById?.id){
      this.deleteCategorySubscription=this.categoryService.deleteCategoryById(this.categoryById.id)
      .subscribe({
        next:(response)=>{
          window.location.reload(); // reload only this component and keep on the same Url , with window.location.reload() reload all App
        }
      })

      console.log('Category deleted:', this.categoryById);
      this.closeModal();
    }

  }

  ngOnDestroy(): void {
    this.deleteCategorySubscription?.unsubscribe();
  }
}
