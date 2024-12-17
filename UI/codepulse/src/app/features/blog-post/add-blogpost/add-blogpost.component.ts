import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit,OnDestroy {

  model:AddBlogPost;
  categories$?:Observable<Category[]>;
  isImageSelectorVisible:boolean=false;

  imageSelectorSubscription?:Subscription;

  constructor(private blogPostService:BlogPostService,
    private imageService:ImageService,
    private router:Router,
    private categoryService:CategoryService
  ){
    this.model={
      title:'',
      shortDescription:'',
      urlHandle:'',
      content:'',
      featuredImageUrl:'',
      author:'',
      isVisible:true,
      publishedDate:new Date(),
      categories:[]
    }
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }
  
  ngOnInit(): void {
    //get Categories
    this.categories$=this.categoryService.getAllCategories();

    //get images selected
    this.imageSelectorSubscription=this.imageService.onSelectImage().subscribe({
      next:(selectedImage)=>{
        this.model.featuredImageUrl=selectedImage.url;
        this.closeModalImageSelector();
      }
    })
  }

  onFormSubmit(){
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model)
    .subscribe({
      next:(response)=>{
        this.router.navigateByUrl('/admin/blogposts');
      }
    })

  }

  openModalImageSelector():void{
    this.isImageSelectorVisible=true;
  }

  closeModalImageSelector():void{
    this.isImageSelectorVisible=false;
  }

}
