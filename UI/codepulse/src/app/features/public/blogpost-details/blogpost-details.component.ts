import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { BlogPostService } from '../../blog-post/services/blog-post.service';

@Component({
  selector: 'app-blogpost-details',
  templateUrl: './blogpost-details.component.html',
  styleUrls: ['./blogpost-details.component.css']
})
export class BlogpostDetailsComponent implements OnInit{

  url:string | null=null;
  blogPost$?:Observable<BlogPost>;

  constructor(private route:ActivatedRoute,
    private blogPostService:BlogPostService
  ) { 
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        this.url=params.get('url');
      }
    });

    //fecth blog Details from Url Handle
    if(this.url){
      this.blogPost$=this.blogPostService.getBlogPostByUrlHandle(this.url);

      this.blogPostService.getBlogPostByUrlHandle(this.url).subscribe({
        next:(response)=>{
          console.log("test date:"+response.publishedDate);
        }
      });
    }
    


  }

}
