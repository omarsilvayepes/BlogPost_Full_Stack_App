import { Component, OnInit, ViewChild } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { DeleteBlogpostComponent } from '../delete-blogpost/delete-blogpost.component';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {

  @ViewChild('deleteBlogPostModal') deleteBlogPostModal!: DeleteBlogpostComponent;

  blogPost$?:Observable<BlogPost[]>;

constructor(private blogPostService:BlogPostService) {}

  ngOnInit(): void {
    //get  all blog post
  this.blogPost$=this.blogPostService.getAllBlogPosts();
  this.blogPost$.subscribe({
    next:(response)=>{
      console.log(response);
    }
  })
  }

}
