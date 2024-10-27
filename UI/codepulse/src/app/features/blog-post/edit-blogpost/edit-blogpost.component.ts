import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { UpdateBlogPost } from '../models/update-blog-post.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit,OnDestroy {
  
 id:string |null =null;
 model?:BlogPost;

 routeSubscription?:Subscription;
 updateBlogPostSubscription?:Subscription;
 getBlogPostSubscription?:Subscription;
 imageSelectSubscription?:Subscription;

 categories$?:Observable<Category[]>;
 selectedCategories?:string[]
 isImageSelectorVisible:boolean=false;
 
  constructor(private route:ActivatedRoute,
    private blogPostService:BlogPostService,
    private categoryService:CategoryService,
    private imageService:ImageService,
    private router:Router
  ) {
  }

  
  ngOnInit(): void {

    this.categories$=this.categoryService.getAllCtegories();

    this.routeSubscription=this.route.paramMap.subscribe({
      next:(params)=>{
        this.id=params.get('id');

        //get Blogpost From 
        
        if(this.id){
          this.getBlogPostSubscription=this.blogPostService.getBlogPostById(this.id).subscribe({
            next:(response)=>{
              this.model=response;

              // get Ids of selected categories
              this.selectedCategories=response.categories.map(c=>c.id); 
            }
          });
        }

        //Get images url

        this.imageSelectSubscription=this.imageService.onSelectImage()
        .subscribe({
          next:(response)=>{
            if(this.model){
              this.model.featuredImageUrl=response.url;
              this.isImageSelectorVisible=false;
            }
          }
        });


      }
    });
  }

  onFormSubmit():void{
    //convert this model to Request Obejct
    if(this.model && this.id){
      var UpdateBlogPost:UpdateBlogPost={
        author:this.model.author,
        content:this.model.content,
        shortDescription:this.model.shortDescription,
        featuredImageUrl:this.model.featuredImageUrl,
        isVisible:this.model.isVisible,
        publishedDate:this.model.publishedDate,
        title:this.model.title,
        urlHandle:this.model.urlHandle,
        categories:this.selectedCategories??[]
      };

      this.updateBlogPostSubscription=this.blogPostService.updateBlogPost(this.id,UpdateBlogPost)
      .subscribe({
        next:(response)=>{
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }

  }

  openModalImageSelector():void{
    this.isImageSelectorVisible=true;
  }

  closeModalImageSelector():void{
    this.isImageSelectorVisible=false;
  }

  ngOnDestroy(): void {
   this.routeSubscription?.unsubscribe();
   this.updateBlogPostSubscription?.unsubscribe();
   this.getBlogPostSubscription?.unsubscribe();
   this.imageSelectSubscription?.unsubscribe();
  }

}
