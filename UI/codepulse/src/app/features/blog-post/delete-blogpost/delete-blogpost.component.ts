import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BlogPost } from '../models/blog-post.model';
import { Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { Location } from '@angular/common';

declare var bootstrap: any;

@Component({
  selector: 'app-delete-blogpost',
  templateUrl: './delete-blogpost.component.html',
  styleUrls: ['./delete-blogpost.component.css']
})
export class DeleteBlogpostComponent implements OnInit ,OnDestroy {

  @Input()
  blogPost!: BlogPost[]; //this blog are get from Parent component (blogpost-list.ts),this component it is the child
  blogPostById?: BlogPost;
  private deletePostSubscription?:Subscription;
  
  private modalElement: any;

  constructor(private blogPostService:BlogPostService,
    private location:Location) {}


  ngOnInit(): void {
    this.modalElement = document.getElementById('deleteBlogPostModal');
    console.log(this.blogPost);
  }

  openModal(id :string) {
    this.blogPostById=this.blogPost?.find(c=> c.id== id);
    console.log(this.blogPostById);
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

    if(this.blogPostById?.id){
      this.deletePostSubscription=this.blogPostService.deletePostById(this.blogPostById.id)
      .subscribe({
        next:(response)=>{
          window.location.reload(); // reload only this component and keep on the same Url , with window.location.reload() reload all App
        }
      })

      console.log('BlogPost deleted:', this.blogPostById);
      this.closeModal();
    }

  }

  ngOnDestroy(): void {
    this.deletePostSubscription?.unsubscribe();
  }

}
